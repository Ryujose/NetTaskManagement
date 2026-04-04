using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Logging.Abstractions;
using NetFramework.Tasks.Management.Abstractions.Enums;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace NetFramework.Tasks.Management.Benchmarks
{
    /// <summary>
    /// Measures CancelAllTasks() as the number of running tasks scales.
    ///
    /// [InvocationCount(1)] is required: CancelAllTasks mutates CTS state so a second
    /// call in the same iteration would see already-cancelled tokens and skew results.
    /// IterationSetup re-registers and re-starts all tasks before each measurement.
    /// </summary>
    [MemoryDiagnoser]
    [InvocationCount(1)]
    public class CancelAllTasksBenchmarks
    {
        private static readonly Action<object> SpinUntilCancelled = state =>
        {
            var cts = (CancellationTokenSource)state;
            while (!cts.IsCancellationRequested)
                Thread.SpinWait(1);
        };

        private TasksManagement _tasks = null!;
        private int _counter;

        [Params(1, 10, 50)]
        public int TaskCount { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
            => _tasks = new TasksManagement(NullLogger.Instance);

        [IterationSetup]
        public void IterationSetup()
        {
            _tasks.ClearConcurrentLists();

            for (int i = 0; i < TaskCount; i++)
            {
                var cts = new CancellationTokenSource();
                var name = $"task-{Interlocked.Increment(ref _counter)}-{i}";
                _tasks.RegisterTask(name, SpinUntilCancelled, cts);
                _tasks.StartTask(name);
            }
        }

        [IterationCleanup]
        public void IterationCleanup()
            => _tasks.ClearConcurrentLists();

        [GlobalCleanup]
        public void GlobalCleanup()
            => _tasks.ClearConcurrentLists();

        /// <summary>
        /// Benchmarks the Parallel.ForEach cancellation fan-out across TaskCount running tasks.
        /// </summary>
        [Benchmark]
        public TaskManagementStatus CancelAllTasks()
        {
            var failed = new ConcurrentDictionary<string, TaskManagementStatus>();
            return _tasks.CancelAllTasks(null, ref failed);
        }
    }
}
