using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Logging.Abstractions;
using NetFramework.Tasks.Management.Abstractions.Enums;
using System;
using System.Collections.Concurrent;
using System.Linq;
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
    /// Threading model — this is the core distinction:
    ///   NTM uses TaskCreationOptions.LongRunning throughout, which instructs the runtime
    ///   to create a dedicated OS thread per task rather than borrowing one from the pool.
    ///   This is the intended usage: named, long-running workers (daemons, background jobs,
    ///   pipeline stages) that must not starve the thread pool used by the rest of the app.
    ///
    ///   Dataflow's ActionBlock uses thread-pool threads. That is the correct model for
    ///   high-throughput anonymous item processing where each work unit is short-lived.
    ///
    ///   NetTaskManagement is the right choice when you need:
    ///     – Named task handles (look up / cancel / delete by string key)
    ///     – Dedicated OS thread per worker — zero pool pressure for long-running work
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
    /// Each scenario has two NTM variants:
    ///   _LongRunning — TaskCreationOptions.LongRunning — dedicated OS thread per task.
    ///                  Right choice for daemons, pipeline stages, long-duration workers.
    ///   _Pool        — TaskCreationOptions.None — thread-pool thread per task.
    ///                  Right choice when tasks complete quickly and pool availability
    ///                  is not a concern; avoids the OS thread spawn cost of LongRunning.
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

        /// <summary>
        /// Duration of blocking work per task in Benchmark 4.
        /// Short enough to keep benchmark runs fast; long enough that the thread-pool
        /// injection interval (~500 ms) cannot hide the batching cost when N &gt; min-threads.
        /// </summary>
        private const int WorkMs = 20;

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
            => CancelWaitDeleteAll();

        /// <summary>
        /// Properly tears down all running NTM workers:
        ///   1. Signal every CTS via CancelAllTasks (idempotent if already cancelled).
        ///   2. Wait for each dedicated OS thread to actually exit via CheckTaskStatusCompleted.
        ///   3. Delete each task (releases dict entry, disposes CTS, reclaims memory).
        ///   4. ClearConcurrentLists as a safety sweep.
        ///
        /// Without the wait step, ClearConcurrentLists returns while threads are still
        /// spinning, and each subsequent iteration spawns more — leading to hundreds of
        /// unbound OS threads that saturate all CPUs.
        /// </summary>
        private void CancelWaitDeleteAll()
        {
            var failed = new ConcurrentDictionary<string, TaskManagementStatus>();
            _tasks.CancelAllTasks(null, ref failed);

            var taskNames = _tasks.GetTasksStatus().Keys.ToArray();
            foreach (var name in taskNames)
            {
                _tasks.CheckTaskStatusCompleted(name, retry: 10, millisecondsCancellationWait: 10_000);
                _tasks.DeleteTask(name);
            }

            _tasks.ClearConcurrentLists();
        }

        // ─────────────────────────────────────────────────────────────────────
        // Benchmark 1 — Start N concurrent workers
        //
        // NTM_LongRunning: RegisterTask(LongRunning) × N  +  StartTask × N
        //           Each worker gets its own OS thread via TaskCreationOptions.LongRunning.
        //           Thread spawn is expensive (~1 ms/thread on Windows) but the thread
        //           is dedicated — zero pool pressure for the lifetime of the worker.
        //           Right choice for daemons and pipeline stages that run for minutes/hours.
        //
        // NTM_Pool: RegisterTask(None) × N  +  StartTask × N
        //           Workers borrow thread-pool threads. Startup is ~10× faster than
        //           LongRunning because no new OS thread is created. Pool threads are
        //           shared, so long-running spin loops will starve other pool work —
        //           use only when tasks are genuinely short-lived or pool depth is ample.
        //
        // Dataflow: new ActionBlock(MaxDegreeOfParallelism = N)  +  Post × N
        //           Workers are anonymous pool threads — no per-worker handle or key.
        //
        // Trade-off: LongRunning → isolated, addressable, expensive to start.
        //            Pool        → shared, cheaper to start, pool-pressure risk.
        //            Dataflow    → pooled, anonymous, cheapest for high-volume streams.
        // ─────────────────────────────────────────────────────────────────────

        [IterationSetup(Targets = new[] { nameof(NTM_StartNWorkers_LongRunning), nameof(NTM_StartNWorkers_Pool) })]
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
        public void NTM_StartNWorkers_LongRunning()
        {
            for (int i = 0; i < N; i++)
            {
                _tasks.RegisterTask(_taskNames[i], SpinUntilCancelled, _ctsList[i], TaskCreationOptions.LongRunning);
                _tasks.StartTask(_taskNames[i]);
            }
        }

        [Benchmark]
        public void NTM_StartNWorkers_Pool()
        {
            for (int i = 0; i < N; i++)
            {
                _tasks.RegisterTask(_taskNames[i], SpinUntilCancelled, _ctsList[i], TaskCreationOptions.None);
                _tasks.StartTask(_taskNames[i]);
            }
        }

        [IterationCleanup(Targets = new[] { nameof(NTM_StartNWorkers_LongRunning), nameof(NTM_StartNWorkers_Pool) })]
        public void Cleanup_NTM_StartNWorkers()
            => CancelWaitDeleteAll();

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
        // NTM:      CancelAllTasks() — sequential foreach (N ≤ 500) across the
        //           dictionary, calling CancellationTokenSource.Cancel() per task.
        //           Returns a status dict of any that could not be cancelled.
        //           Both LongRunning and Pool variants measure the same cancel path —
        //           the CTS signalling cost does not depend on thread origin.
        //
        // Dataflow: CancellationTokenSource.Cancel() — one call signals all N workers
        //           via the shared token passed to ExecutionDataflowBlockOptions.
        //           Workers run on thread-pool threads.
        //
        // Trade-off: NTM's per-task cancel lets you exempt specific tasks and collect
        //            per-task failure status; Dataflow's shared-token cancel is O(1)
        //            but all-or-nothing.
        //
        // Note: Thread.Sleep gives workers time to enter their spin loops before
        //       measurement. LongRunning threads are scheduled immediately by the OS,
        //       so the sleep is the same for both variants.
        // ─────────────────────────────────────────────────────────────────────

        [IterationSetup(Targets = new[] { nameof(NTM_CancelNWorkers_LongRunning) })]
        public void Setup_NTM_CancelNWorkers_LongRunning()
        {
            _tasks.ClearConcurrentLists();
            for (int i = 0; i < N; i++)
            {
                var cts = new CancellationTokenSource();
                var name = $"worker-{Interlocked.Increment(ref _counter)}";
                _tasks.RegisterTask(name, SpinUntilCancelled, cts, TaskCreationOptions.LongRunning);
                _tasks.StartTask(name);
            }
            Thread.Sleep(50);
        }

        [Benchmark]
        public TaskManagementStatus NTM_CancelNWorkers_LongRunning()
        {
            var failed = new ConcurrentDictionary<string, TaskManagementStatus>();
            return _tasks.CancelAllTasks(null, ref failed);
        }

        [IterationCleanup(Targets = new[] { nameof(NTM_CancelNWorkers_LongRunning) })]
        public void Cleanup_NTM_CancelNWorkers_LongRunning()
            => CancelWaitDeleteAll();

        // ──

        [IterationSetup(Targets = new[] { nameof(NTM_CancelNWorkers_Pool) })]
        public void Setup_NTM_CancelNWorkers_Pool()
        {
            _tasks.ClearConcurrentLists();
            for (int i = 0; i < N; i++)
            {
                var cts = new CancellationTokenSource();
                var name = $"worker-{Interlocked.Increment(ref _counter)}";
                _tasks.RegisterTask(name, SpinUntilCancelled, cts, TaskCreationOptions.None);
                _tasks.StartTask(name);
            }
            Thread.Sleep(50);
        }

        [Benchmark]
        public TaskManagementStatus NTM_CancelNWorkers_Pool()
        {
            var failed = new ConcurrentDictionary<string, TaskManagementStatus>();
            return _tasks.CancelAllTasks(null, ref failed);
        }

        [IterationCleanup(Targets = new[] { nameof(NTM_CancelNWorkers_Pool) })]
        public void Cleanup_NTM_CancelNWorkers_Pool()
            => CancelWaitDeleteAll();

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
        // NTM_LongRunning: Measured: StartTask × N  →  CheckTaskStatusCompleted × N
        //                  →  DeleteTask × N  (includes GC.Collect per delete)
        //                  Each item gets a dedicated OS thread. Expensive for trivial
        //                  work — intentionally shows the cost of the wrong tool.
        //
        // NTM_Pool:        Same pipeline with TaskCreationOptions.None.
        //                  Thread-pool reuse avoids the OS thread spawn overhead, so
        //                  start is faster. Still pays GC.Collect per DeleteTask.
        //                  Right choice when items complete quickly and pool depth is
        //                  not under pressure from the rest of the application.
        //
        // Dataflow:        new ActionBlock  +  Post × N  +  Complete  +  Wait
        //                  Thread-pool threads, no per-item lifecycle cost — this IS
        //                  its intended use case. Use NTM when you need named handles
        //                  and per-task cancel/status; use Dataflow for anonymous items.
        //
        // Read the LongRunning gap as the cost of spawning OS threads for trivial work.
        // Read the Pool gap as the NTM lifecycle overhead (dict, GC) vs raw Dataflow.
        // ─────────────────────────────────────────────────────────────────────

        [IterationSetup(Targets = new[] { nameof(NTM_ProcessNItems_LongRunning), nameof(NTM_ProcessNItems_Pool) })]
        public void Setup_NTM_ProcessNItems()
        {
            _tasks.ClearConcurrentLists();
            _ctsList = new CancellationTokenSource[N];
            _taskNames = new string[N];
            for (int i = 0; i < N; i++)
            {
                _ctsList[i] = new CancellationTokenSource();
                _taskNames[i] = $"item-{Interlocked.Increment(ref _counter)}";
            }
        }

        [Benchmark]
        public void NTM_ProcessNItems_LongRunning()
        {
            for (int i = 0; i < N; i++)
                _tasks.RegisterTask(_taskNames[i], _ => { /* trivial work unit */ }, _ctsList[i], TaskCreationOptions.LongRunning);

            for (int i = 0; i < N; i++)
                _tasks.StartTask(_taskNames[i]);

            for (int i = 0; i < N; i++)
                _tasks.CheckTaskStatusCompleted(_taskNames[i], retry: 3, millisecondsCancellationWait: 5000);

            for (int i = 0; i < N; i++)
                _tasks.DeleteTask(_taskNames[i]);
        }

        [Benchmark]
        public void NTM_ProcessNItems_Pool()
        {
            for (int i = 0; i < N; i++)
                _tasks.RegisterTask(_taskNames[i], _ => { /* trivial work unit */ }, _ctsList[i], TaskCreationOptions.None);

            for (int i = 0; i < N; i++)
                _tasks.StartTask(_taskNames[i]);

            for (int i = 0; i < N; i++)
                _tasks.CheckTaskStatusCompleted(_taskNames[i], retry: 3, millisecondsCancellationWait: 5000);

            for (int i = 0; i < N; i++)
                _tasks.DeleteTask(_taskNames[i]);
        }

        [IterationCleanup(Targets = new[] { nameof(NTM_ProcessNItems_LongRunning), nameof(NTM_ProcessNItems_Pool) })]
        public void Cleanup_NTM_ProcessNItems()
            => CancelWaitDeleteAll();

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

        // ─────────────────────────────────────────────────────────────────────
        // Benchmark 4 — Parallel blocking-work throughput
        //
        // Each of N workers blocks for WorkMs milliseconds (simulating I/O,
        // a database call, an HTTP request, etc.).  The total wall-clock time
        // is determined by how quickly all workers can start and run in parallel.
        //
        // NTM_LongRunning: N dedicated OS threads are created before the first
        //   worker starts. All N workers begin blocking simultaneously.
        //   Total elapsed ≈ WorkMs — no queuing, no injection delay.
        //
        // NTM_Pool / Dataflow_Unbounded: Workers run on thread-pool threads.
        //   The pool starts with min = Environment.ProcessorCount threads.
        //   When all pool threads are blocked, the pool injects one new thread
        //   roughly every 500 ms. For N >> ProcessorCount the work is batched:
        //     total ≈ ceil(N / minThreads) × WorkMs   (ignoring injection delay)
        //   plus potential 500 ms stalls while the pool decides to inject.
        //
        // This is the definitive LongRunning use case: blocking workers that
        // must all start within a latency budget, or workers that must not
        // starve the pool that the rest of the application depends on.
        //
        // Note: tasks are pre-registered in IterationSetup so that thread
        //   creation (LongRunning) and pool dispatch (Pool) are NOT included
        //   in the measurement — only the actual parallel execution is timed.
        // ─────────────────────────────────────────────────────────────────────

        [IterationSetup(Targets = new[]
        {
            nameof(NTM_ParallelBlockingWork_LongRunning),
            nameof(NTM_ParallelBlockingWork_Pool)
        })]
        public void Setup_NTM_ParallelBlockingWork()
        {
            _tasks.ClearConcurrentLists();
            _ctsList = new CancellationTokenSource[N];
            _taskNames = new string[N];
            for (int i = 0; i < N; i++)
            {
                _ctsList[i] = new CancellationTokenSource();
                _taskNames[i] = $"blocking-{Interlocked.Increment(ref _counter)}";
            }
        }

        [Benchmark]
        public void NTM_ParallelBlockingWork_LongRunning()
        {
            for (int i = 0; i < N; i++)
                _tasks.RegisterTask(_taskNames[i], _ => Thread.Sleep(WorkMs), _ctsList[i], TaskCreationOptions.LongRunning);

            for (int i = 0; i < N; i++)
                _tasks.StartTask(_taskNames[i]);

            for (int i = 0; i < N; i++)
                _tasks.CheckTaskStatusCompleted(_taskNames[i], retry: 5, millisecondsCancellationWait: 30_000);

            for (int i = 0; i < N; i++)
                _tasks.DeleteTask(_taskNames[i]);
        }

        [Benchmark]
        public void NTM_ParallelBlockingWork_Pool()
        {
            for (int i = 0; i < N; i++)
                _tasks.RegisterTask(_taskNames[i], _ => Thread.Sleep(WorkMs), _ctsList[i], TaskCreationOptions.None);

            for (int i = 0; i < N; i++)
                _tasks.StartTask(_taskNames[i]);

            for (int i = 0; i < N; i++)
                _tasks.CheckTaskStatusCompleted(_taskNames[i], retry: 5, millisecondsCancellationWait: 30_000);

            for (int i = 0; i < N; i++)
                _tasks.DeleteTask(_taskNames[i]);
        }

        [IterationCleanup(Targets = new[]
        {
            nameof(NTM_ParallelBlockingWork_LongRunning),
            nameof(NTM_ParallelBlockingWork_Pool)
        })]
        public void Cleanup_NTM_ParallelBlockingWork()
            => CancelWaitDeleteAll();

        // ──

        [Benchmark]
        public void Dataflow_ParallelBlockingWork()
        {
            // Unbounded degree — Dataflow imposes no artificial cap, so any gap
            // between this and LongRunning is purely the thread-pool injection cost,
            // not a Dataflow configuration choice.
            var block = new ActionBlock<int>(
                _ => Thread.Sleep(WorkMs),
                new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = DataflowBlockOptions.Unbounded });

            for (int i = 0; i < N; i++)
                block.Post(i);

            block.Complete();
            block.Completion.Wait();
        }
    }
}
