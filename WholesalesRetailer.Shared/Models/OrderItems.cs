namespace WholesalesRetailer.Shared.Models
{
    public class OrderItems
    {
        public int Id { get; set; }
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }
        public double? UnitPrice { get; set; }
    }
}
