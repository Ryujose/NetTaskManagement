# NetFramework.Tasks.Management — Benchmarks

BenchmarkDotNet suite that measures the cost of every public operation exposed by `ITaskManagement`, targeting all three supported runtimes: **net8.0**, **net9.0**, and **net10.0**.

## Benchmark classes

### `LifecycleBenchmarks`

Isolates each individual stage of the task lifecycle. `[InvocationCount(1)]` is applied at the class level because every method mutates the shared static `ConcurrentDictionary` — each iteration must start from a clean slate. Targeted `IterationSetup` / `IterationCleanup` drive the task to the required state before timing begins, then reset afterwards.

| Benchmark | What is measured |
|---|---|
| `RegisterTask` | `ConcurrentDictionary.TryAdd` + `Task` constructor |
| `StartTask` | `TryGetValue` + `Task.Start` (thread-pool enqueue) |
| `CancelTask` | `TryGetValue` + `CancellationTokenSource.Cancel` |
| `DeleteTask` | `TryRemove` + `Task.Dispose` + `GC.Collect` (see ordering invariant in CLAUDE.md) |
| `FullLifecycle` | All four stages end-to-end, including the forced GC |

### `GetTasksStatusBenchmarks`

Measures `GetTasksStatus()` (a `ConcurrentDictionary → Dictionary` snapshot) as the number of registered tasks grows. Tasks stay alive across all iterations — no `InvocationCount(1)` needed because the operation is purely read-only.

| Parameter | Values |
|---|---|
| `TaskCount` | 1, 10, 50 |

### `CancelAllTasksBenchmarks`

Measures the `Parallel.ForEach` fan-out inside `CancelAllTasks` as the number of running tasks grows. `[InvocationCount(1)]` + `IterationSetup` re-registers and re-starts all tasks before each measurement so cancellation state is always fresh.

| Parameter | Values |
|---|---|
| `TaskCount` | 1, 10, 50 |

### `DataflowComparisonBenchmarks`

Head-to-head comparison of NetTaskManagement vs TPL Dataflow for the scenarios where both libraries can solve the same problem. The goal is to help developers choose the right tool, not declare a winner — each library has distinct strengths.

| Benchmark pair | What is measured |
|---|---|
| `NTM_StartNWorkers` / `Dataflow_StartNWorkers` | Cost of getting N concurrent workers running. NTM: `RegisterTask × N` + `StartTask × N` (ConcurrentDictionary inserts + `Task.Start`). Dataflow: `new ActionBlock` + `Post × N`. |
| `NTM_CancelNWorkers` / `Dataflow_CancelNWorkers` | Cancel N running workers. NTM: `CancelAllTasks()` uses `Parallel.ForEach` and returns per-task status. Dataflow: single `CancellationTokenSource.Cancel()` shared across all workers. |
| `NTM_ProcessNItems` / `Dataflow_ProcessNItems` | Process N short-lived items end-to-end. NTM: `StartTask × N` → `CheckTaskStatusCompleted × N` → `DeleteTask × N` (includes `GC.Collect` per delete). Dataflow: `Post × N` → `Complete` → `Wait`. |

`[InvocationCount(1)]` applies to all pairs because every method mutates shared state. A `Thread.Sleep(50)` in the cancellation setup ensures workers are actively spinning before the timed cancellation call — without it, some items may not have been dequeued yet and would understate the true cancellation cost.

| Parameter | Values |
|---|---|
| `N` | 10, 50, 100 |

## Running

> Benchmarks must be run in **Release** configuration. Debug builds produce meaningless results.

### Single framework

```bash
# net8.0
dotnet run -c Release -f net8.0 --project benchmarks/NetFramework.Tasks.Management.Benchmarks/

# net9.0
dotnet run -c Release -f net9.0 --project benchmarks/NetFramework.Tasks.Management.Benchmarks/

# net10.0
dotnet run -c Release -f net10.0 --project benchmarks/NetFramework.Tasks.Management.Benchmarks/
```

### Filter to a specific class

```bash
dotnet run -c Release -f net8.0 --project benchmarks/NetFramework.Tasks.Management.Benchmarks/ -- --filter *Lifecycle*
dotnet run -c Release -f net8.0 --project benchmarks/NetFramework.Tasks.Management.Benchmarks/ -- --filter *GetTasksStatus*
dotnet run -c Release -f net8.0 --project benchmarks/NetFramework.Tasks.Management.Benchmarks/ -- --filter *CancelAll*
dotnet run -c Release -f net8.0 --project benchmarks/NetFramework.Tasks.Management.Benchmarks/ -- --filter *DataflowComparison*
```

### All benchmarks

```bash
dotnet run -c Release -f net8.0 --project benchmarks/NetFramework.Tasks.Management.Benchmarks/ -- --filter *
```

### Interactive menu (no filter)

```bash
dotnet run -c Release -f net8.0 --project benchmarks/NetFramework.Tasks.Management.Benchmarks/
```

BenchmarkDotNet will list all available benchmark classes and let you choose interactively.

## Output

Results are written to `BenchmarkDotNet.Artifacts/` in the project directory:

```
BenchmarkDotNet.Artifacts/
└── results/
    ├── NetFramework.Tasks.Management.Benchmarks.LifecycleBenchmarks-report.md
    ├── NetFramework.Tasks.Management.Benchmarks.GetTasksStatusBenchmarks-report.md
    ├── NetFramework.Tasks.Management.Benchmarks.CancelAllTasksBenchmarks-report.md
    └── NetFramework.Tasks.Management.Benchmarks.DataflowComparisonBenchmarks-report.md
```

`[MemoryDiagnoser]` is enabled on all classes so each report includes **allocated bytes per operation** in addition to execution time.

## Notes

- `DeleteTask` and `FullLifecycle` will be slower than the other operations because `DeleteTask` calls `GC.Collect()` + `GC.WaitForPendingFinalizers()` by design (see the ordering invariant in [CLAUDE.md](../../CLAUDE.md)).
- `CancelAllTasks` uses `Parallel.ForEach` internally, so its cost scales sub-linearly with `TaskCount` on machines with multiple cores.
- `GetTasksStatus` allocates a new `Dictionary<string, TaskStatus>` on every call — the allocation column in the report reflects this.
