using WholesalesRetailer.Shared.Models;

namespace WholesalesRetailer.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetProductById(int id);
        List<Product>? GetProducts();
        int GetProductIdByCode(string code);
        double? GetUnitPriceByCode(string code);
        void UpdateProduct(string code, int quantity);
        int GetQuantity(string code);
        Product? AddProduct(Product request);
    }
}