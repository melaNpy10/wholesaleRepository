namespace WholesalesRetailer.Shared.Models
{
    public class CashBack
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public double? Value { get; set; }
    }
}
