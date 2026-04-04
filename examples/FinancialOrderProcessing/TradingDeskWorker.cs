using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;

namespace FinancialOrderProcessing
{
    /// <summary>
    /// Simulates a trading desk that dequeues and executes market orders.
    ///
    /// Execution latency model (realistic):
    ///   base      5 ms  — network round-trip to exchange
    ///   jitter    0–40 ms — exchange queue depth / matching engine variance
    ///   size     +20 ms  — large orders (> 5 000 shares) need partial fills
    ///   price    +10 ms  — high-price securities route through a slower ECN
    ///
    /// Total worst case: ~75 ms per order.
    /// </summary>
    public static class TradingDeskWorker
    {
        public static Action<object> CreateAction(
            string deskName,
            ConcurrentQueue<MarketOrder> queue,
            WorkerMetrics metrics)
        {
            return (state) =>
            {
                var cts = (CancellationTokenSource)state;
                var rng = new Random();

                while (!cts.IsCancellationRequested)
                {
                    if (!queue.TryDequeue(out MarketOrder? order))
                    {
                        // Queue is empty — back off 1 ms before retrying
                        Thread.Sleep(1);
                        continue;
                    }

                    var sw = Stopwatch.StartNew();

                    int latencyMs =
                        5                               // base: network RTT
                        + rng.Next(0, 41)               // jitter: exchange variance
                        + (order.Quantity > 5_000 ? 20 : 0)   // size factor
                        + (order.Price    > 500m  ? 10 : 0);  // price/ECN factor

                    Thread.Sleep(latencyMs);

                    sw.Stop();
                    metrics.RecordExecution(sw.ElapsedMilliseconds);
                }
            };
        }
    }
}
