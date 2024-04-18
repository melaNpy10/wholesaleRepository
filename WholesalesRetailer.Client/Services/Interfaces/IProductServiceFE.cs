using WholesalesRetailer.Shared.Models;

namespace WholesalesRetailer.Client.Services.Interfaces
{
    public interface IProductServiceFE
    {
        Task<List<Product?>> GetProducts();
        Task<Product?> AddProduct(ProductFE request);
    }
}
