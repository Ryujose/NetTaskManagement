using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Logging.Abstractions;
using NetFramework.Tasks.Management.Abstractions.Enums;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace NetFramework.Tasks.Management.Benchmarks
{
    /// <summary>
    /// Head-to-head: NetTaskManagement vs TPL Dataflow for concurrent-worker scenarios.
    ///
    /// Purpose: help developers choose the right tool, not declare a winner.
    ///
    ///   NetTaskManagement is the right choice when you need:
    ///     – Named task handles (look up / cancel / delete by string key)
    ///     – Status-based lifecycle API (no raw Task objects exposed to callers)
    ///     – DI-friendly registration and an observability queue for disposed tasks
    ///     – Explicit per-task cancel + wait + delete with memory reclaim guarantees
    ///
    ///   TPL Dataflow is the right choice when you need:
    ///     – High-throughput anonymous item processing (pipeline graphs, fan-out/fan-in)
    ///     – Built-in backpressure (BoundedCapacity) and block linking
    ///     – Minimal per-item overhead for high-volume streams
    ///     – Single shared cancellation token across all workers
    ///
    /// Overlap measured here: starting N concurrent workers, cancelling them in bulk,
    /// and processing N short-lived work items end-to-end.
    ///
    /// [InvocationCount(1)] is required for all benchmarks — every method mutates
    /// shared state (ConcurrentDictionary, ActionBlock, CancellationTokenSource).
    /// </summary>
    [MemoryDiagnoser]
    [InvocationCount(1)]
    public class DataflowComparisonBenchmarks
    {
        private static readonly Action<object> SpinUntilCancelled = state =>
        {
            var cts = (CancellationTokenSource)state;
            while (!cts.IsCancellationRequested)
                Thread.SpinWait(1);
        };

        [Params(10, 50, 100)]
        public int N { get; set; }

        // ── NTM state ─────────────────────────────────────────────────────────
        private TasksManagement _tasks = null!;
        private int _counter;
        private string[] _taskNames = null!;
        private CancellationTokenSource[] _ctsList = null!;

        // ── Dataflow state ────────────────────────────────────────────────────
        private ActionBlock<int> _block = null!;
        private CancellationTokenSource _blockCts = null!;

        [GlobalSetup]
        public void GlobalSetup()
            => _tasks = new TasksManagement(NullLogger.Instance);

        [GlobalCleanup]
        public void GlobalCleanup()
            => _tasks.ClearConcurrentLists();

        // ─────────────────────────────────────────────────────────────────────
        // Benchmark 1 — Start N concurrent workers
        //
        // NTM:      RegisterTask × N  +  StartTask × N
        //           Each worker gets a unique string key, a CancellationTokenSource,
        //           and a slot in the ConcurrentDictionary.
        //
        // Dataflow: new ActionBlock(MaxDegreeOfParallelism = N)  +  Post × N
        //           Workers are anonymous — no per-worker handle or key.
        //
        // Trade-off: NTM gives you addressable handles at the cost of per-task
        //            dictionary overhead; Dataflow is leaner but you lose individual control.
        // ─────────────────────────────────────────────────────────────────────

        [IterationSetup(Targets = new[] { nameof(NTM_StartNWorkers) })]
        public void Setup_NTM_StartNWorkers()
        {
            _ctsList = new CancellationTokenSource[N];
            _taskNames = new string[N];
            for (int i = 0; i < N; i++)
            {
                _ctsList[i] = new CancellationTokenSource();
                _taskNames[i] = $"worker-{Interlocked.Increment(ref _counter)}";
            }
        }

        [Benchmark]
        public void NTM_StartNWorkers()
        {
            for (int i = 0; i < N; i++)
            {
                _tasks.RegisterTask(_taskNames[i], SpinUntilCancelled, _ctsList[i]);
                _tasks.StartTask(_taskNames[i]);
            }
        }

        [IterationCleanup(Targets = new[] { nameof(NTM_StartNWorkers) })]
        public void Cleanup_NTM_StartNWorkers()
            => _tasks.ClearConcurrentLists();

        // ──

        [IterationSetup(Targets = new[] { nameof(Dataflow_StartNWorkers) })]
        public void Setup_Dataflow_StartNWorkers()
            => _blockCts = new CancellationTokenSource();

        [Benchmark]
        public void Dataflow_StartNWorkers()
        {
            _block = new ActionBlock<int>(_ =>
            {
                while (!_blockCts.IsCancellationRequested)
                    Thread.SpinWait(1);
            }, new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = N });

            for (int i = 0; i < N; i++)
                _block.Post(i);
        }

        [IterationCleanup(Targets = new[] { nameof(Dataflow_StartNWorkers) })]
        public void Cleanup_Dataflow_StartNWorkers()
        {
            _blockCts.Cancel();
            _block.Complete();
            _block.Completion.Wait();
            _blockCts.Dispose();
        }

        // ─────────────────────────────────────────────────────────────────────
        // Benchmark 2 — Cancel N running workers
        //
        // NTM:      CancelAllTasks() — Parallel.ForEach across the dictionary,
        //           calling CancellationTokenSource.Cancel() on each task individually.
        //           Returns a status dict of any that could not be cancelled.
        //
        // Dataflow: CancellationTokenSource.Cancel() — one call signals all N workers
        //           via the shared token passed to ExecutionDataflowBlockOptions.
        //
        // Trade-off: NTM's per-task cancel lets you exempt specific tasks and collect
        //            per-task failure status; Dataflow's shared-token cancel is O(1)
        //            but all-or-nothing.
        //
        // Note: Thread.Sleep in Dataflow setup gives workers time to enter their
        //       spin loops before measurement — without it some items may not have
        //       been dequeued yet, understating the cancellation cost.
        // ─────────────────────────────────────────────────────────────────────

        [IterationSetup(Targets = new[] { nameof(NTM_CancelNWorkers) })]
        public void Setup_NTM_CancelNWorkers()
        {
            _tasks.ClearConcurrentLists();
            for (int i = 0; i < N; i++)
            {
                var cts = new CancellationTokenSource();
                var name = $"worker-{Interlocked.Increment(ref _counter)}";
                _tasks.RegisterTask(name, SpinUntilCancelled, cts);
                _tasks.StartTask(name);
            }
            // Give thread-pool workers time to enter their spin loops.
            Thread.Sleep(50);
        }

        [Benchmark]
        public TaskManagementStatus NTM_CancelNWorkers()
        {
            var failed = new ConcurrentDictionary<string, TaskManagementStatus>();
            return _tasks.CancelAllTasks(null, ref failed);
        }

        [IterationCleanup(Targets = new[] { nameof(NTM_CancelNWorkers) })]
        public void Cleanup_NTM_CancelNWorkers()
            => _tasks.ClearConcurrentLists();

        // ──

        [IterationSetup(Targets = new[] { nameof(Dataflow_CancelNWorkers) })]
        public void Setup_Dataflow_CancelNWorkers()
        {
            _blockCts = new CancellationTokenSource();
            _block = new ActionBlock<int>(_ =>
            {
                while (!_blockCts.IsCancellationRequested)
                    Thread.SpinWait(1);
            }, new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = N });

            for (int i = 0; i < N; i++)
                _block.Post(i);

            // Give thread-pool workers time to enter their spin loops.
            Thread.Sleep(50);
        }

        [Benchmark]
        public void Dataflow_CancelNWorkers()
            => _blockCts.Cancel();

        [IterationCleanup(Targets = new[] { nameof(Dataflow_CancelNWorkers) })]
        public void Cleanup_Dataflow_CancelNWorkers()
        {
            _block.Complete();
            _block.Completion.Wait();
            _blockCts.Dispose();
        }

        // ─────────────────────────────────────────────────────────────────────
        // Benchmark 3 — Process N short-lived work items end-to-end
        //
        // NTM:      Tasks are pre-registered in IterationSetup.
        //           Measured: StartTask × N  →  CheckTaskStatusCompleted × N
        //                     →  DeleteTask × N  (includes GC.Collect per delete)
        //           This is the correct NTM idiom for item-scoped work; it exposes
        //           the per-task lifecycle cost (dictionary TryRemove + GC).
        //
        // Dataflow: Measured: new ActionBlock  +  Post × N  +  Complete  +  Wait
        //           No per-item lifecycle cost — items are dispatched and forgotten.
        //
        // Trade-off: NTM carries O(N) lifecycle overhead (GC.Collect × N) in exchange
        //            for named handles and status tracking per item. Dataflow has
        //            near-zero overhead per item but you get no individual item status.
        // ─────────────────────────────────────────────────────────────────────

        [IterationSetup(Targets = new[] { nameof(NTM_ProcessNItems) })]
        public void Setup_NTM_ProcessNItems()
        {
            _tasks.ClearConcurrentLists();
            _ctsList = new CancellationTokenSource[N];
            _taskNames = new string[N];
            for (int i = 0; i < N; i++)
            {
                _ctsList[i] = new CancellationTokenSource();
                _taskNames[i] = $"item-{Interlocked.Increment(ref _counter)}";
                _tasks.RegisterTask(_taskNames[i], _ => { /* trivial work unit */ }, _ctsList[i]);
            }
        }

        [Benchmark]
        public void NTM_ProcessNItems()
        {
            for (int i = 0; i < N; i++)
                _tasks.StartTask(_taskNames[i]);

            for (int i = 0; i < N; i++)
                _tasks.CheckTaskStatusCompleted(_taskNames[i], retry: 3, millisecondsCancellationWait: 5000);

            for (int i = 0; i < N; i++)
                _tasks.DeleteTask(_taskNames[i]);
        }

        [IterationCleanup(Targets = new[] { nameof(NTM_ProcessNItems) })]
        public void Cleanup_NTM_ProcessNItems()
            => _tasks.ClearConcurrentLists();

        // ──

        [Benchmark]
        public void Dataflow_ProcessNItems()
        {
            var block = new ActionBlock<int>(
                _ => { /* trivial work unit */ },
                new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = N });

            for (int i = 0; i < N; i++)
                block.Post(i);

            block.Complete();
            block.Completion.Wait();
        }
    }
}
