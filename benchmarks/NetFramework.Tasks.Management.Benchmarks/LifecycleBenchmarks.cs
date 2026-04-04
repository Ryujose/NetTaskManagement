using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Logging.Abstractions;
using NetFramework.Tasks.Management.Abstractions.Enums;
using System;
using System.Threading;

namespace NetFramework.Tasks.Management.Benchmarks
{
    /// <summary>
    /// Measures the cost of each individual stage in the task lifecycle:
    ///   Register → Start → Cancel → Delete
    /// and the end-to-end full lifecycle in a single benchmark.
    ///
    /// [InvocationCount(1)] is required because every method mutates the shared
    /// static ConcurrentDictionary — each iteration must start from a clean slate.
    /// IterationSetup / IterationCleanup are targeted per benchmark method so that
    /// only the stage under measurement is timed.
    /// </summary>
    [MemoryDiagnoser]
    [InvocationCount(1)]
    public class LifecycleBenchmarks
    {
        // A lightweight action that spins until cancelled — exits within microseconds
        // of CancellationTokenSource.Cancel() so setup times stay minimal.
        private static readonly Action<object> SpinUntilCancelled = state =>
        {
            var cts = (CancellationTokenSource)state;
            while (!cts.IsCancellationRequested)
                Thread.SpinWait(1);
        };

        private TasksManagement _tasks = null!;
        private CancellationTokenSource _cts = null!;
        private string _taskName = null!;
        private int _counter;

        [GlobalSetup]
        public void GlobalSetup()
            => _tasks = new TasksManagement(NullLogger.Instance);

        [GlobalCleanup]
        public void GlobalCleanup()
            => _tasks.ClearConcurrentLists();

        // ── RegisterTask ──────────────────────────────────────────────────────
        // Measures: ConcurrentDictionary.TryAdd + Task constructor

        [IterationSetup(Targets = new[] { nameof(RegisterTask) })]
        public void SetupRegister()
        {
            _taskName = $"bench-{Interlocked.Increment(ref _counter)}";
            _cts = new CancellationTokenSource();
        }

        [Benchmark]
        public TaskManagementStatus RegisterTask()
            => _tasks.RegisterTask(_taskName, SpinUntilCancelled, _cts);

        [IterationCleanup(Targets = new[] { nameof(RegisterTask) })]
        public void CleanupRegister()
            => _tasks.ClearConcurrentLists();

        // ── StartTask ─────────────────────────────────────────────────────────
        // Measures: ConcurrentDictionary.TryGetValue + Task.Start (thread pool enqueue)

        [IterationSetup(Targets = new[] { nameof(StartTask) })]
        public void SetupStart()
        {
            _taskName = $"bench-{Interlocked.Increment(ref _counter)}";
            _cts = new CancellationTokenSource();
            _tasks.RegisterTask(_taskName, SpinUntilCancelled, _cts);
        }

        [Benchmark]
        public TaskManagementStatus StartTask()
            => _tasks.StartTask(_taskName);

        [IterationCleanup(Targets = new[] { nameof(StartTask) })]
        public void CleanupStart()
            => _tasks.ClearConcurrentLists();

        // ── CancelTask ────────────────────────────────────────────────────────
        // Measures: ConcurrentDictionary.TryGetValue + CancellationTokenSource.Cancel

        [IterationSetup(Targets = new[] { nameof(CancelTask) })]
        public void SetupCancel()
        {
            _taskName = $"bench-{Interlocked.Increment(ref _counter)}";
            _cts = new CancellationTokenSource();
            _tasks.RegisterTask(_taskName, SpinUntilCancelled, _cts);
            _tasks.StartTask(_taskName);
        }

        [Benchmark]
        public TaskManagementStatus CancelTask()
            => _tasks.CancelTask(_taskName);

        [IterationCleanup(Targets = new[] { nameof(CancelTask) })]
        public void CleanupCancel()
            => _tasks.ClearConcurrentLists();

        // ── DeleteTask ────────────────────────────────────────────────────────
        // Measures: TryRemove + Task.Dispose + GC.Collect (see DeleteTask ordering invariant)
        // Setup drives the task to completion before timing begins.

        [IterationSetup(Targets = new[] { nameof(DeleteTask) })]
        public void SetupDelete()
        {
            _taskName = $"bench-{Interlocked.Increment(ref _counter)}";
            _cts = new CancellationTokenSource();
            _tasks.RegisterTask(_taskName, SpinUntilCancelled, _cts);
            _tasks.StartTask(_taskName);
            _tasks.CancelTask(_taskName);
            // Drive to completion before we start timing DeleteTask
            _tasks.CheckTaskStatusCompleted(_taskName, retry: 5, millisecondsCancellationWait: 500);
        }

        [Benchmark]
        public TaskManagementStatus DeleteTask()
            => _tasks.DeleteTask(_taskName, sendDisposedDataToInternalQueue: false);

        [IterationCleanup(Targets = new[] { nameof(DeleteTask) })]
        public void CleanupDelete()
            => _tasks.ClearConcurrentLists();

        // ── FullLifecycle ─────────────────────────────────────────────────────
        // Measures the complete Register → Start → Cancel → CheckCompleted → Delete
        // pipeline end-to-end, including the GC.Collect inside DeleteTask.

        [IterationSetup(Targets = new[] { nameof(FullLifecycle) })]
        public void SetupFullLifecycle()
        {
            _taskName = $"bench-{Interlocked.Increment(ref _counter)}";
            _cts = new CancellationTokenSource();
        }

        [Benchmark]
        public TaskManagementStatus FullLifecycle()
        {
            _tasks.RegisterTask(_taskName, SpinUntilCancelled, _cts);
            _tasks.StartTask(_taskName);
            _tasks.CancelTask(_taskName);
            _tasks.CheckTaskStatusCompleted(_taskName, retry: 5, millisecondsCancellationWait: 500);
            return _tasks.DeleteTask(_taskName, sendDisposedDataToInternalQueue: false);
        }

        [IterationCleanup(Targets = new[] { nameof(FullLifecycle) })]
        public void CleanupFullLifecycle()
            => _tasks.ClearConcurrentLists();
    }
}
