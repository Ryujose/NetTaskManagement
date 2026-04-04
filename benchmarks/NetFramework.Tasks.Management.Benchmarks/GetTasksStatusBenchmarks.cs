using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NetFramework.Tasks.Management.Benchmarks
{
    /// <summary>
    /// Measures GetTasksStatus() throughput as the number of registered tasks scales.
    /// GetTasksStatus is read-only (ConcurrentDictionary.ToDictionary snapshot), so
    /// multiple invocations per iteration are safe — no InvocationCount(1) needed.
    ///
    /// Tasks are pre-registered in GlobalSetup and left alive for the entire benchmark run.
    /// </summary>
    [MemoryDiagnoser]
    public class GetTasksStatusBenchmarks
    {
        private TasksManagement _tasks = null!;

        [Params(1, 10, 50)]
        public int TaskCount { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            _tasks = new TasksManagement(NullLogger.Instance);
            _tasks.ClearConcurrentLists();

            for (int i = 0; i < TaskCount; i++)
            {
                var cts = new CancellationTokenSource();
                _tasks.RegisterTask($"task-{i}", _ => { }, cts);
                // Kept in Created state — status snapshot is identical regardless
            }
        }

        [GlobalCleanup]
        public void GlobalCleanup()
            => _tasks.ClearConcurrentLists();

        /// <summary>
        /// Benchmarks ConcurrentDictionary → Dictionary snapshot cost at each task count.
        /// </summary>
        [Benchmark]
        public Dictionary<string, TaskStatus> GetTasksStatus()
            => _tasks.GetTasksStatus();
    }
}
