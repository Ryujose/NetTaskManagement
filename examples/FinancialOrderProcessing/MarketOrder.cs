using System;

namespace FinancialOrderProcessing
{
    public enum OrderType { Buy, Sell }

    public class MarketOrder
    {
        public int      Id        { get; }
        public string   Symbol    { get; }
        public OrderType Type     { get; }
        public int      Quantity  { get; }
        public decimal  Price     { get; }
        public DateTime CreatedAt { get; }

        public MarketOrder(int id, string symbol, OrderType type, int quantity, decimal price, DateTime createdAt)
        {
            Id        = id;
            Symbol    = symbol;
            Type      = type;
            Quantity  = quantity;
            Price     = price;
            CreatedAt = createdAt;
        }

        public decimal NotionalValue => Quantity * Price;
    }
}
