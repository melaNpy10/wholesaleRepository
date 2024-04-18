namespace WholesalesRetailer.Shared.Models
{
    public class OrderRequest
    {
        public int? CustomerId { get; set; }
        public string? Code { get; set; }
        public int? Quantity { get; set; }
    }
}
