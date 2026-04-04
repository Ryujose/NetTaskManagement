# NetTaskManagement

A .NET Standard 2.0 library for Task Parallel Library (TPL) orchestration with Dependency Injection support.

Tasks are identified by **string key** and managed through a **status-based API** — no raw `Task` objects exposed to callers. The library handles the full task lifecycle: register → start → cancel → check completion → delete, and exposes an append-only observability queue that captures disposal metadata for external monitoring.

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

**[View benchmark charts](https://ryujose.github.io/NetTaskManagement/dev/bench/)**

Benchmarks cover:
- `LifecycleBenchmarks` — individual lifecycle stages: Register, Start, Cancel, Delete, and full end-to-end
- `GetTasksStatusBenchmarks` — dictionary snapshot cost at 1, 10, and 50 tasks
- `CancelAllTasksBenchmarks` — `Parallel.ForEach` cancellation fan-out at 1, 10, and 50 tasks

All benchmarks run on **net8.0** and **net9.0** with `[MemoryDiagnoser]` enabled (reports allocated bytes per operation).

To run locally (Release mode required):

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
