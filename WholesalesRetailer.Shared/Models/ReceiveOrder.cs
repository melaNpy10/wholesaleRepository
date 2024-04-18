namespace WholesalesRetailer.Shared.Models
{
    public class ReceiveOrder
    {
        public int CustomerId { get; set; }
        public List<ProductData> ProductData { get; set; }
    }
}
