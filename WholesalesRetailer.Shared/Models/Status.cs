namespace WholesalesRetailer.Shared.Models
{
    public class Status
    {
        public int StatusId { get; set; }
        public StatusDescription StatusDescription { get; set; } 
    }

    public enum StatusDescription
    {
        Unknown = 1,
        Pending = 2,
        Shipped = 3,
        Completed = 4,
        Cancelled = 5,
        Declined = 6,
        Refunded = 7
    }
}
