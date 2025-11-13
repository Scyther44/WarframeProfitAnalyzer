namespace WarframeProfitAnalyzer.Models
{
    public class OrdersResponse
    {
        public string? ApiVersion { get; set; }
        public OrdersData? Data { get; set; }
        public object? Error { get; set; }
    }

    public class OrdersData
    {
        public List<Order>? Buy { get; set; }
        public List<Order>? Sell { get; set; }
    }

    public class Order
    {
        public string? Id { get; set; }
        public string? OrderType { get; set; }  // "buy" or "sell"
        public int Platinum { get; set; }
        public int Quantity { get; set; }
        public string? Platform { get; set; }
        public User? User { get; set; }
    }

    public class User
    {
        public string? Id { get; set; }
        public string? Status { get; set; }  // "ingame", "online", "offline"
        public int? Reputation { get; set; }
    }
}
