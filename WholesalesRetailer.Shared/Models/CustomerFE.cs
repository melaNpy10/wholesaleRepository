using System.ComponentModel.DataAnnotations;

namespace WholesalesRetailer.Shared.Models
{
    public class CustomerFE
    {
        [Required]
        public string? CustomerName { get; set; }
        [Required]
        public string? Email { get; set; }
        public string? Adress { get; set; }
        public int? RebateId { get; set; }
    }
}
