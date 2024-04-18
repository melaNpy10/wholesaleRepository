namespace WholesalesRetailer.Shared.Models
{
    public class ProductFE
    {
        public string? ProductName { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public double? Standard_Cost { get; set; }
        public double? List_Price { get; set; }
        public int? Quantity { get; set; }
        public int? CategoryId { get; set; }
    }
}
