# NetTaskManagement

A .NET Standard 2.0 C# library for Task Parallel Library (TPL) orchestration with Dependency Injection support. Callers identify tasks by string key and interact through a status-based API instead of raw `Task` objects.

## Published NuGet Packages

- `NetFramework.Tasks.Management` — core implementation
- `NetFramework.Tasks.Management.Abstractions` — interfaces, enums, models

Registry: `nuget.pkg.github.com/ryujose`

## Solution Structure

```
src/
├── NetFramework.Task.Management/               # Core implementation
├── NetFramework.Task.Management.Abstractions/  # ITaskManagement, enums, models
└── NetFramework.Tasks.Management.xUnit.Tests/  # xUnit test suite
examples/
└── FinancialOrderProcessing/                   # Multi-threaded market order pipeline demo
benchmarks/
└── NetFramework.Tasks.Management.Benchmarks/   # BenchmarkDotNet suite (net8.0;net9.0;net10.0)
```

## Common Commands

```bash
# Restore
dotnet restore src/NetFramework.Task.Management.sln

# Build
dotnet build src/NetFramework.Task.Management.sln --configuration Release

# Test (all TFMs — do NOT pass --framework, all targets must pass)
dotnet test src/NetFramework.Tasks.Management.xUnit.Tests/

# Test (single filter)
dotnet test --filter "FullyQualifiedName~RegisterTaskTests"

# Pack
dotnet pack src/NetFramework.Task.Management/NetFramework.Tasks.Management.csproj --configuration Release
dotnet pack src/NetFramework.Task.Management.Abstractions/NetFramework.Tasks.Management.Abstractions.csproj --configuration Release

# Benchmarks (Release mode required — Debug produces meaningless results)
dotnet run -c Release -f net8.0  --project benchmarks/NetFramework.Tasks.Management.Benchmarks/ -- --filter '*'
dotnet run -c Release -f net9.0  --project benchmarks/NetFramework.Tasks.Management.Benchmarks/ -- --filter '*'
dotnet run -c Release -f net10.0 --project benchmarks/NetFramework.Tasks.Management.Benchmarks/ -- --filter '*'
```

## Architecture

- **`ITaskManagement`** is the primary service contract (defined in Abstractions)
- **`AddTaskManagement()`** registers the service via `IServiceCollection`
- Methods return **`TaskManagementStatus`** enum values — not exceptions — for all expected outcomes
- Logging via injected `ILogger` is present at every public method boundary

### In-memory task registry (intentional static fields)

`TasksDataModel` (`static ConcurrentDictionary<string, TaskDataModel>`) and `TaskDisposedDataModel` (`static ConcurrentQueue<TaskDisposedDataModel>`) are **deliberately static**. This makes the in-memory registry shared across all DI scopes and `TasksManagement` instances for the lifetime of the process — do not convert them to instance fields.

- **`TasksDataModel`** — single source of truth for all live tasks. Used to register, start, cancel, and delete tasks by string key.
- **`TaskDisposedDataModel`** — append-only observability queue. When a task is deleted, its metadata is enqueued here. External systems (logging platforms, monitoring tools) dequeue and consume the metadata however they want. The library itself does not process this queue.

### DeleteTask() ordering invariant

`DeleteTask()` guarantees that when it returns, the `TaskDataModel` wrapper is fully gone from memory. The steps must remain in this exact order:

```
1. Capture taskId and taskStatus as local primitives   ← before any removal
2. TryRemove from TasksDataModel                       ← release dict reference
3. task.Dispose() + CancellationTokenSource.Dispose()
4. taskDataModel = null                                ← release local reference
5. GC.Collect() + GC.WaitForPendingFinalizers()        ← now actually collects
6. Build TaskDisposedDataModel from captured primitives
7. Enqueue to TaskDisposedDataModel if requested
```

GC must run **after** both references (dict and local variable) are released, otherwise it fires with live references and collects nothing. Metadata must be captured **before** removal so it is available for the observability queue after GC runs.

### Task lifecycle

```
RegisterTask → StartTask → [running in TasksDataModel]
                                    ↓
                           CancelTask / completes naturally
                                    ↓
                           CheckTaskStatusCompleted
                                    ↓
                           DeleteTask
                             → capture metadata
                             → TryRemove from TasksDataModel
                             → Dispose Task + CTS
                             → null local ref
                             → GC.Collect (block — guarantees memory freed)
                             → Enqueue to TaskDisposedDataModel (if requested)
```

## CI/CD

| Workflow | Trigger | Purpose |
|---|---|---|
| `nugetpackage.yml` | Push tag `v*.*.*` | Build, pack, publish both NuGet packages to GitHub Packages and nuget.org |
| `ci.yml` | Push / PR | Run full xUnit test suite across all TFMs |
| `benchmarks.yml` | Push to `main` / manual | Run BenchmarkDotNet on net8.0, net9.0, net10.0 and publish charts to GitHub Pages |

Benchmark results are committed to `benchmarks/results/` on `main` and served via GitHub Pages at `https://ryujose.github.io/NetTaskManagement/benchmarks/results/`.

**To release a new version:** bump the version in both `.csproj` files, commit, then push a tag: `git tag v1.2.3 && git push origin v1.2.3`
