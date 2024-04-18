namespace WholesalesRetailer.Shared.Models
{
    public class OrderFE : Order
    {

        public string CustomerName { get; set; }
        public string OrderStatus { get; set; }
        public double? ProdValue { get; set; }   
        public double? ProcentRabate { get; set; }
        public double? CashBack { get; set; }
        public double? FutureCashBack {  get; set; } 
    }
}
