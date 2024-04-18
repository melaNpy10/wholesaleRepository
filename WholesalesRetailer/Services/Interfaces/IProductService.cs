using Microsoft.AspNetCore.Mvc;
using WholesalesRetailer.Shared.Models;

namespace WholesalesRetailer.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product?> GetProductById(int id);
        List<Product>? GetProducts();
        int GetProductIdByCode(string code);
        void UpdateProduct(string code, int quantity);
        Product? AddProduct(Product request);
    }
}