using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetFramework.Tasks.Management;
using NetFramework.Tasks.Management.Abstractions.Enums;
using NetFramework.Tasks.Management.Abstractions.Interfaces;

namespace FinancialOrderProcessing
{
    internal class Program
    {
        private const int SimulationSeconds = 15;

        private static readonly string[] DeskNames =
        {
            "desk-equities",
            "desk-bonds",
            "desk-fx",
            "desk-derivatives",
            "desk-commodities",
        };

        static void Main()
        {
            // ── Dependency Injection ──────────────────────────────────────────────
            // AddTaskManagement() needs a plain ILogger (non-generic) from the container.
            var services = new ServiceCollection();
            services.AddLogging(b => b
                .AddConsole()
                .SetMinimumLevel(LogLevel.Error));     // suppress library warn/info noise during demo
            services.AddSingleton<ILogger>(sp =>
                sp.GetRequiredService<ILoggerFactory>()
                  .CreateLogger("NetFramework.Tasks.Management"));
            services.AddTaskManagement();

            var provider  = services.BuildServiceProvider();
            var tasks     = provider.GetRequiredService<ITaskManagement>();

            // ── Shared state ──────────────────────────────────────────────────────
            var orderQueue = new ConcurrentQueue<MarketOrder>();
            var metrics    = new ConcurrentDictionary<string, WorkerMetrics>();

            // ── Register & start: Producer ────────────────────────────────────────
            var producerMetrics = new WorkerMetrics { StartedAt = DateTime.UtcNow };
            metrics["producer"] = producerMetrics;

            var producerCts = new CancellationTokenSource();
            tasks.RegisterTask("producer",
                OrderProducer.CreateAction(orderQueue, producerMetrics),
                producerCts);
            tasks.StartTask("producer");

            // ── Register & start: Trading desk workers ────────────────────────────
            foreach (var name in DeskNames)
            {
                var workerMetrics = new WorkerMetrics { StartedAt = DateTime.UtcNow };
                metrics[name] = workerMetrics;

                var cts = new CancellationTokenSource();
                tasks.RegisterTask(name,
                    TradingDeskWorker.CreateAction(name, orderQueue, workerMetrics),
                    cts);
                tasks.StartTask(name);
            }

            // ── Register & start: Market monitor ─────────────────────────────────
            var monitorCts = new CancellationTokenSource();
            tasks.RegisterTask("monitor",
                MarketMonitor.CreateAction(tasks, metrics, orderQueue),
                monitorCts);
            tasks.StartTask("monitor");

            // ── Simulation run ────────────────────────────────────────────────────
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║         Financial Order Processing — Simulation                      ║");
            Console.WriteLine($"║         Duration : {SimulationSeconds}s  |  Desks : {DeskNames.Length}  |  Press any key to stop      ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine($"  Tasks registered and started:");
            Console.WriteLine($"    producer  — generating ~1 000 orders/sec");
            foreach (var n in DeskNames)
                Console.WriteLine($"    {n}  — executing orders (5–75 ms latency model)");
            Console.WriteLine($"    monitor   — live status table every 2 s");

            var simulationClock = Stopwatch.StartNew();

            while (simulationClock.Elapsed.TotalSeconds < SimulationSeconds)
            {
                try
                {
                    if (Console.KeyAvailable)
                    {
                        Console.ReadKey(intercept: true);
                        break;
                    }
                }
                catch (InvalidOperationException) { /* stdin redirected — ignore */ }

                Thread.Sleep(200);
            }

            simulationClock.Stop();

            // ── Graceful shutdown ─────────────────────────────────────────────────
            Console.WriteLine();
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Shutdown initiated — cancelling all tasks...");

            var cancelFailures = new ConcurrentDictionary<string, TaskManagementStatus>();
            tasks.CancelAllTasks(except: null, tasksCancelPetitionFailedRef: ref cancelFailures);

            var completionStatuses = new ConcurrentDictionary<string, TaskManagementStatus>();
            tasks.CheckAllTaskStatusCompleted(
                except: null,
                tasksCompletedStatusRef: ref completionStatuses,
                retry: 3,
                millisecondsCancellationWait: 8_000);

            foreach (var m in metrics.Values)
                m.FinishedAt = DateTime.UtcNow;

            // Delete every task and push its disposal record onto the observability queue
            foreach (var name in DeskNames.Concat(new[] { "producer", "monitor" }))
                tasks.DeleteTask(name, sendDisposedDataToInternalQueue: true);

            // ── Final report ──────────────────────────────────────────────────────
            long totalProcessed   = DeskNames.Sum(n => metrics[n].OrdersHandled);
            long totalGenerated   = metrics["producer"].OrdersHandled;
            int  remainingInQueue = orderQueue.Count;

            Console.WriteLine();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                         FINAL REPORT                                ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine($"  Simulation wall time  : {simulationClock.Elapsed:mm\\:ss\\.fff}");
            Console.WriteLine($"  Orders generated      : {totalGenerated,10:N0}");
            Console.WriteLine($"  Orders processed      : {totalProcessed,10:N0}");
            Console.WriteLine($"  Orders remaining      : {remainingInQueue,10:N0}  ({remainingInQueue * 100.0 / Math.Max(totalGenerated, 1):F1}% unfilled)");
            Console.WriteLine();
            Console.WriteLine($"  {"Desk",-22} {"Orders",9} {"Avg ms",8} {"Min ms",8} {"Max ms",8} {"Throughput",13}");
            Console.WriteLine($"  {new string('─', 78)}");

            foreach (var name in DeskNames)
            {
                var m        = metrics[name];
                var elapsed  = (m.FinishedAt == default ? DateTime.UtcNow : m.FinishedAt) - m.StartedAt;
                var throughput = elapsed.TotalSeconds > 0
                    ? m.OrdersHandled / elapsed.TotalSeconds
                    : 0;

                Console.WriteLine(
                    $"  {name,-22} {m.OrdersHandled,9:N0} {m.AverageMs,8:F1} {m.MinMs,8} {m.MaxMs,8} {throughput,12:F1}/s");
            }

            // ── Observability queue drain ─────────────────────────────────────────
            Console.WriteLine();
            Console.WriteLine($"  {"─── Observability queue",78}");
            int dequeued = 0;
            while (tasks.DequeueTaskDisposedDataModel(out var record) == TaskManagementStatus.ObjectInfoDequeued)
            {
                Console.WriteLine(
                    $"  Task '{record.TaskName,-20}' id={record.TaskId,5}" +
                    $"  final status = {record.TaskStatus,-18}  disposed = {record.IsDisposed}");
                dequeued++;
            }
            Console.WriteLine($"  Total records dequeued: {dequeued}");
            Console.WriteLine();
        }
    }
}
