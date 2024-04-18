using Microsoft.AspNetCore.Mvc;
using WholesalesRetailer.Repositories.Interfaces;
using WholesalesRetailer.Services.Interfaces;
using WholesalesRetailer.Shared.Models;

namespace WholesalesRetailer.Services
{
    public class ProductService : IProductService
    {
        readonly ILogger<ProductService> _logger;
        private readonly IProductRepository _productRepository;
        public ProductService(ILogger<ProductService> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }
        public List<Product>? GetProducts()
        {
            try
            {
                return _productRepository.GetProducts();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetProducts)} - Error while getting Products. Message={ex.Message}");
                return null;
            }

        }
        public Task<Product?> GetProductById(int id)
        {
            try
            {
                return _productRepository.GetProductById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetProductById)} - Error while getting Products. Message={ex.Message}");
                return null;
            }
        }
        public int GetProductIdByCode(string code)
        {
            try
            {
                return _productRepository.GetProductIdByCode(code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetProductIdByCode)} - Error while getting Products. Message={ex.Message}");
                return 0;
            }
        }
        public void UpdateProduct(string code, int quantity)
        {
            try
            {
                _productRepository.UpdateProduct(code, quantity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetProductIdByCode)} - Error while getting Products. Message={ex.Message}");
            }
        }
        public Product? AddProduct(Product request)
        {
            try
            {
                return _productRepository.AddProduct(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(request)} - Error while getting Users. Message={ex.Message}");
                return null;
            }
        }
    }
}
