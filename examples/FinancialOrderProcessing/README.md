# Financial Order Processing — Example

A runnable simulation that demonstrates every major feature of `NetFramework.Tasks.Management` under realistic multi-threaded load, modelled on a financial market order processing pipeline.

## What it simulates

A trading platform receives a continuous stream of market orders (Buy/Sell across 15 symbols) and routes them through five specialised trading desks running in parallel. A market monitor reports live progress every two seconds. After 15 seconds the coordinator initiates a graceful shutdown and prints a full performance report.

```
┌─────────────┐        ConcurrentQueue<MarketOrder>
│  Producer   │ ──────────────────────────────────────────────┐
│  ~1 000/sec │                                               │
└─────────────┘                                               ▼
                                              ┌───────────────────────────┐
                                              │  desk-equities            │
                                              │  desk-bonds               │
                                              │  desk-fx          workers │
                                              │  desk-derivatives  (~20/s │
                                              │  desk-commodities  each)  │
                                              └───────────────────────────┘
┌─────────────┐
│  Monitor    │  GetTasksStatus() + WorkerMetrics → live table every 2 s
└─────────────┘

┌─────────────┐
│ Coordinator │  CancelAllTasks → CheckAllTaskStatusCompleted
│  (Main)     │  → DeleteTask (sendToQueue: true) × 7
│             │  → DequeueTaskDisposedDataModel → final report
└─────────────┘
```

## Execution latency model

Each worker simulates realistic exchange round-trip times:

| Factor | Cost |
|---|---|
| Base network RTT | 5 ms |
| Exchange queue jitter | 0 – 40 ms |
| Large order (> 5 000 shares) | +20 ms |
| High-price security (> $500) | +10 ms |
| **Worst case** | **75 ms** |

At ~47 ms average, five desks process roughly **100 orders/sec combined** against a producer rate of **~1 000 orders/sec**, so the queue grows steadily — intentional, to demonstrate backpressure.

## Library features exercised

| Feature | Where |
|---|---|
| `RegisterTask` / `StartTask` | All 7 tasks (1 producer, 5 desks, 1 monitor) |
| `GetTasksStatus()` | `MarketMonitor` — live TPL state per task |
| `CancelAllTasks` | Coordinator graceful shutdown |
| `CheckAllTaskStatusCompleted` | Coordinator waits for all tasks to drain |
| `DeleteTask(sendToQueue: true)` | Coordinator cleanup — feeds observability queue |
| `DequeueTaskDisposedDataModel` | Final report — drains disposal records |

## Project structure

```
FinancialOrderProcessing/
├── Program.cs                  DI setup, lifecycle coordination, final report
├── MarketOrder.cs              Order record and OrderType enum
├── WorkerMetrics.cs            Lock-free thread-safe stats (Interlocked / CAS)
├── OrderProducer.cs            Burst-generates market orders into the shared queue
├── TradingDeskWorker.cs        Dequeues and executes orders with latency model
└── MarketMonitor.cs            Prints live status table via GetTasksStatus()
```

## Running

From the repository root:

```bash
dotnet run --project examples/FinancialOrderProcessing/FinancialOrderProcessing.csproj
```

Press any key to stop early. The default duration is 15 seconds.

## Sample output

```
╔══════════════════════════════════════════════════════════════════════╗
║         Financial Order Processing — Simulation                      ║
║         Duration : 15s  |  Desks : 5  |  Press any key to stop      ║
╚══════════════════════════════════════════════════════════════════════╝

[02:16:11] ── LIVE STATUS ───────────────────────────────────────────────────────
  Queue depth :    1.102 orders pending

  Task                   TPL State         Orders   Avg ms   Min ms   Max ms
  ────────────────────────────────────────────────────────────────────────────
  desk-derivatives       Running               43     45,5       15       78
  desk-equities          Running               43     45,6       14       77
  producer               Running            1.320      0,0        0        0
  desk-bonds             Running               41     48,6       14       78
  desk-commodities       Running               45     43,8       16       79
  desk-fx                Running               41     48,2       15       78
  ────────────────────────────────────────────────────────────────────────────

[02:16:24] Shutdown initiated — cancelling all tasks...

╔══════════════════════════════════════════════════════════════════════╗
║                         FINAL REPORT                                ║
╚══════════════════════════════════════════════════════════════════════╝

  Simulation wall time  : 00:15.020
  Orders generated      :      9.660
  Orders processed      :      1.579
  Orders remaining      :      8.081  (83,7% unfilled)

  Desk                      Orders   Avg ms   Min ms   Max ms    Throughput
  ──────────────────────────────────────────────────────────────────────────────
  desk-equities                317     47,1       14       79         19,7/s
  desk-bonds                   328     45,4       14       79         20,4/s
  desk-fx                      314     47,4       14       79         19,5/s
  desk-derivatives             309     48,2       14       79         19,2/s
  desk-commodities             311     48,0       15       79         19,3/s

  ─── Observability queue
  Task 'desk-equities       ' id=1  final status = RanToCompletion  disposed = True
  Task 'desk-bonds          ' id=2  final status = RanToCompletion  disposed = True
  Task 'desk-fx             ' id=3  final status = RanToCompletion  disposed = True
  Task 'desk-derivatives    ' id=4  final status = RanToCompletion  disposed = True
  Task 'desk-commodities    ' id=5  final status = RanToCompletion  disposed = True
  Task 'producer            ' id=6  final status = RanToCompletion  disposed = True
  Task 'monitor             ' id=7  final status = RanToCompletion  disposed = True
  Total records dequeued: 7
```

## Dependencies

| Package | Purpose |
|---|---|
| `NetFramework.Tasks.Management` | Task lifecycle management (project reference) |
| `Microsoft.Extensions.DependencyInjection` | DI container setup |
| `Microsoft.Extensions.Logging.Console` | Console log sink (set to Error level to suppress library noise) |
