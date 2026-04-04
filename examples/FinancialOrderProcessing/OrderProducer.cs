using System;
using System.Collections.Concurrent;
using System.Threading;

namespace FinancialOrderProcessing
{
    /// <summary>
    /// Generates market orders at ~1 000 orders/sec into the shared queue.
    /// Works in bursts of 20 orders followed by a 20 ms pause so the OS
    /// scheduler can give CPU time to the worker desks.
    /// </summary>
    public static class OrderProducer
    {
        private static readonly string[] Symbols =
        {
            "AAPL", "MSFT", "GOOGL", "TSLA", "AMZN",
            "META", "NVDA", "JPM",  "BAC",  "GS",
            "BRK.B", "V",   "MA",   "UNH",  "HD"
        };

        public static Action<object> CreateAction(
            ConcurrentQueue<MarketOrder> queue,
            WorkerMetrics metrics)
        {
            return (state) =>
            {
                var cts = (CancellationTokenSource)state;
                var rng = new Random();
                int orderId = 0;

                while (!cts.IsCancellationRequested)
                {
                    // Burst: enqueue 20 orders then yield briefly
                    for (int i = 0; i < 20 && !cts.IsCancellationRequested; i++)
                    {
                        var order = new MarketOrder(
                            id:        ++orderId,
                            symbol:    Symbols[rng.Next(Symbols.Length)],
                            type:      rng.Next(2) == 0 ? OrderType.Buy : OrderType.Sell,
                            quantity:  rng.Next(100, 10_001),
                            price:     Math.Round((decimal)(rng.NextDouble() * 999 + 1), 2),
                            createdAt: DateTime.UtcNow
                        );

                        queue.Enqueue(order);
                        metrics.IncrementCount();
                    }

                    Thread.Sleep(20); // ~1 000 orders/sec sustained
                }
            };
        }
    }
}
