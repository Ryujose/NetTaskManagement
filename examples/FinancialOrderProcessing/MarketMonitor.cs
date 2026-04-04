using System;
using System.Collections.Concurrent;
using System.Threading;
using NetFramework.Tasks.Management.Abstractions.Interfaces;

namespace FinancialOrderProcessing
{
    /// <summary>
    /// Runs every 2 seconds and prints a live status table by combining:
    ///   - GetTasksStatus()  → actual TPL task state from the library
    ///   - WorkerMetrics     → application-level order statistics
    /// </summary>
    public static class MarketMonitor
    {
        public static Action<object> CreateAction(
            ITaskManagement taskManagement,
            ConcurrentDictionary<string, WorkerMetrics> metrics,
            ConcurrentQueue<MarketOrder> queue)
        {
            return (state) =>
            {
                var cts = (CancellationTokenSource)state;

                while (!cts.IsCancellationRequested)
                {
                    Thread.Sleep(2_000);

                    if (cts.IsCancellationRequested) break;

                    var tplStatuses = taskManagement.GetTasksStatus();
                    var now = DateTime.Now;

                    Console.WriteLine();
                    Console.WriteLine($"[{now:HH:mm:ss}] ── LIVE STATUS ───────────────────────────────────────────────────────");
                    Console.WriteLine($"  Queue depth : {queue.Count,8:N0} orders pending");
                    Console.WriteLine();
                    Console.WriteLine($"  {"Task",-22} {"TPL State",-14} {"Orders",9} {"Avg ms",8} {"Min ms",8} {"Max ms",8}");
                    Console.WriteLine($"  {new string('─', 76)}");

                    foreach (var (name, m) in metrics)
                    {
                        tplStatuses.TryGetValue(name, out var tplState);
                        string avg = m.OrdersHandled > 0 ? $"{m.AverageMs:F1}" : "─";
                        string min = m.OrdersHandled > 0 ? m.MinMs.ToString() : "─";
                        string max = m.OrdersHandled > 0 ? m.MaxMs.ToString() : "─";

                        Console.WriteLine(
                            $"  {name,-22} {tplState,-14} {m.OrdersHandled,9:N0} {avg,8} {min,8} {max,8}");
                    }

                    Console.WriteLine($"  {new string('─', 76)}");
                }
            };
        }
    }
}
