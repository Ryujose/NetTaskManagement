# NetTaskManagement

[![CI](https://github.com/Ryujose/NetTaskManagement/actions/workflows/ci.yml/badge.svg)](https://github.com/Ryujose/NetTaskManagement/actions/workflows/ci.yml)
[![codecov](https://codecov.io/gh/Ryujose/NetTaskManagement/branch/main/graph/badge.svg)](https://codecov.io/gh/Ryujose/NetTaskManagement)

A .NET Standard 2.0 library for Task Parallel Library (TPL) orchestration with Dependency Injection support.

Tasks are identified by **string key** and managed through a **status-based API** — no raw `Task` objects exposed to callers. The library handles the full task lifecycle: register → start → cancel → check completion → delete, and exposes an append-only observability queue that captures disposal metadata for external monitoring.

## NetTaskManagement vs TPL Dataflow — which one do you need?

These are complementary tools, not competing ones. The benchmarks below are meant to help you choose.

| | **NetTaskManagement** | **TPL Dataflow** |
|---|---|---|
| Task identity | Named string key — look up, cancel, or delete any task by name at any time | Anonymous — items are messages, no individual handles |
| Lifecycle control | Explicit Register → Start → Cancel → CheckCompleted → Delete with status at every step | Complete the block and await `Completion`; no per-item lifecycle |
| DI integration | `AddTaskManagement()` registers `ITaskManagement` as a scoped service | Manual construction |
| Cancellation granularity | Per-task: cancel one, a filtered subset, or all — with per-task failure reporting | Shared token: cancel the whole block at once |
| Throughput focus | Long-running named workers (background jobs, daemons, named pipelines) | High-volume anonymous item streams (fan-out, fan-in, transform chains) |
| Observability | Built-in disposal queue — capture task name, id, and final status when a task is removed | No built-in disposal queue |
| Memory reclaim | `DeleteTask` forces `GC.Collect` after disposal — guarantees memory is freed | GC-managed by the runtime |

**Choose NetTaskManagement when** your tasks have identity, need individual control, and live for seconds to hours.<br>
**Choose TPL Dataflow when** you are processing a high-volume stream of anonymous items through a pipeline graph.

**[View live benchmark charts — NTM vs Dataflow](https://ryujose.github.io/NetTaskManagement/benchmarks/results)**

## Packages

| Package | NuGet |
|---|---|
| `NetFramework.Tasks.Management` | Core implementation |
| `NetFramework.Tasks.Management.Abstractions` | Interfaces, enums, and models only |

## Installation

```bash
dotnet add package NetFramework.Tasks.Management
```

If you only need the `ITaskManagement` contract (e.g. in a project that wires up DI but doesn't consume the implementation directly):

```bash
dotnet add package NetFramework.Tasks.Management.Abstractions
```

## Quick start

### Register the service

```csharp
services.AddTaskManagement();
```

### Inject and use `ITaskManagement`

```csharp
public class MyService
{
    private readonly ITaskManagement _tasks;

    public MyService(ITaskManagement tasks) => _tasks = tasks;

    public void RunJob(string jobName)
    {
        var cts = new CancellationTokenSource();

        _tasks.RegisterTask(jobName, state =>
        {
            var token = ((CancellationTokenSource)state).Token;
            while (!token.IsCancellationRequested)
            {
                // do work
            }
        }, cts);

        _tasks.StartTask(jobName);
    }

    public void CancelJob(string jobName) => _tasks.CancelTask(jobName);
}
```

## API reference

All methods return `TaskManagementStatus` — an enum value instead of throwing exceptions for expected outcomes.

| Method | Description |
|---|---|
| `RegisterTask(name, action, cts)` | Registers a new task by name |
| `StartTask(name)` | Starts a previously registered task |
| `CancelTask(name)` | Requests cancellation via the task's `CancellationTokenSource` |
| `CheckTaskStatusCompleted(name, retry, timeoutMs)` | Polls until the task completes or the timeout elapses |
| `DeleteTask(name, sendToQueue)` | Disposes the task and removes it from the registry; optionally enqueues disposal metadata |
| `DequeueTaskDisposedDataModel(out model)` | Dequeues one disposal record for external monitoring |
| `GetTasksStatus()` | Returns a snapshot `Dictionary<string, TaskStatus>` of all live tasks |
| `CancelAllTasks(except, ref failed)` | Cancels all tasks, optionally skipping a list of names |
| `CheckAllTaskStatusCompleted(except, ref statuses, retry, timeoutMs)` | Polls completion for all tasks, optionally skipping a list of names |
| `GetCancellationTokenSource(name, ref cts)` | Retrieves the `CancellationTokenSource` for a registered task |

### Status enum highlights

| Status | Meaning |
|---|---|
| `Added` | Task registered successfully |
| `Started` | Task started successfully |
| `Canceled` | Cancellation requested |
| `Completed` | Task reached a completion state |
| `NotCompleted` | Polling timed out before completion |
| `Deleted` | Task disposed and removed |
| `TaskNotFound` | No task registered under that name |
| `AlreadyRegistered` | A task with that name already exists |

## Observability queue

When `DeleteTask(name, sendToQueue: true)` is called, a `TaskDisposedDataModel` record (task name, id, final status, disposed flag) is appended to an internal `ConcurrentQueue`. Dequeue and forward it to your logging or monitoring platform however you like:

```csharp
if (_tasks.DequeueTaskDisposedDataModel(out var record) == TaskManagementStatus.ObjectInfoDequeued)
{
    logger.LogInformation("Task {Name} (id={Id}) finished with status {Status}", 
        record.TaskName, record.TaskId, record.TaskStatus);
}
```

## Benchmarks

Performance is tracked automatically on every push to `main` and published as interactive charts:

**[View benchmark charts](https://ryujose.github.io/NetTaskManagement/benchmarks/results)**

| Benchmark class | What it measures |
|---|---|
| `LifecycleBenchmarks` | Each lifecycle stage in isolation: Register, Start, Cancel, Delete, and full end-to-end |
| `GetTasksStatusBenchmarks` | Dictionary snapshot cost at 1, 10, and 50 tasks |
| `CancelAllTasksBenchmarks` | `Parallel.ForEach` cancellation fan-out at 1, 10, and 50 tasks |
| `DataflowComparisonBenchmarks` | Head-to-head vs TPL Dataflow: start N workers, cancel N workers, process N items — at N = 10, 50, 100 |

All benchmarks run on **net8.0**, **net9.0**, and **net10.0** with `[MemoryDiagnoser]` enabled (reports allocated bytes per operation).

To run the comparison benchmarks locally (Release mode required):

```bash
dotnet run -c Release -f net9.0 --project benchmarks/NetFramework.Tasks.Management.Benchmarks/ -- --filter '*DataflowComparison*'
```

To run everything:

```bash
dotnet run -c Release -f net8.0 --project benchmarks/NetFramework.Tasks.Management.Benchmarks/ -- --filter '*'
```

See [benchmarks/README.md](benchmarks/NetFramework.Tasks.Management.Benchmarks/README.md) for full run instructions and filter examples.

## Examples

| Example | Description |
|---|---|
| [Financial Order Processing](examples/FinancialOrderProcessing/README.md) | Multi-threaded market order pipeline — producer, 5 trading desk workers, live monitor, graceful shutdown, and observability queue drain |

## License

MIT © Jose Luis Guerra Infante
