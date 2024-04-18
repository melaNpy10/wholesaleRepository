namespace WholesalesRetailer.Shared.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int? StatusId { get; set; }
        public DateTime? OrderDate { get; set; }
        public double? OrderValue { get; set; }
    }
}
