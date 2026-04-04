using System;
using System.Threading;

namespace FinancialOrderProcessing
{
    /// <summary>
    /// Lock-free, thread-safe statistics for a single task.
    /// Uses Interlocked and CAS loops so multiple worker threads can update
    /// without taking a lock.
    /// </summary>
    public class WorkerMetrics
    {
        private long _ordersHandled;
        private long _totalProcessingMs;
        private long _minMs = long.MaxValue;
        private long _maxMs;

        public DateTime StartedAt  { get; set; }
        public DateTime FinishedAt { get; set; }

        // ── Read-only snapshots ───────────────────────────────────────────────

        public long OrdersHandled    => Volatile.Read(ref _ordersHandled);
        public long TotalProcessingMs => Volatile.Read(ref _totalProcessingMs);
        public long MaxMs            => Volatile.Read(ref _maxMs);

        public long MinMs
        {
            get
            {
                long v = Volatile.Read(ref _minMs);
                return v == long.MaxValue ? 0 : v;
            }
        }

        public double AverageMs =>
            OrdersHandled > 0 ? TotalProcessingMs / (double)OrdersHandled : 0;

        // ── Writers (called from worker threads) ──────────────────────────────

        /// <summary>Counts one order without recording latency (used by the producer).</summary>
        public void IncrementCount() => Interlocked.Increment(ref _ordersHandled);

        /// <summary>Records one completed order with its processing time.</summary>
        public void RecordExecution(long processingMs)
        {
            Interlocked.Increment(ref _ordersHandled);
            Interlocked.Add(ref _totalProcessingMs, processingMs);

            // CAS loop for min
            long current;
            do { current = Volatile.Read(ref _minMs); }
            while (processingMs < current &&
                   Interlocked.CompareExchange(ref _minMs, processingMs, current) != current);

            // CAS loop for max
            do { current = Volatile.Read(ref _maxMs); }
            while (processingMs > current &&
                   Interlocked.CompareExchange(ref _maxMs, processingMs, current) != current);
        }
    }
}
