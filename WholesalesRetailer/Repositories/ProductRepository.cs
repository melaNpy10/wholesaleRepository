using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using WholesalesRetailer.Data;
using WholesalesRetailer.Repositories.Interfaces;
using WholesalesRetailer.Shared.Models;

namespace WholesalesRetailer.Repositories
{
    public class ProductRepository : IProductRepository
    {
        readonly ILogger<ProductRepository> _logger;
        private readonly DataContext _dataContext;
        public ProductRepository(ILogger<ProductRepository> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        public List<Product>? GetProducts()
        {
            try
            {
                var products = _dataContext.Products.Include(_ => _.CategoryId).ToList();
                return products;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetProducts)} - Error while getting Products. Message={ex.Message}");
                return null;
            }
        }

        public async Task<Product?> GetProductById(int id)
        {
            try
            {
                var products = await _dataContext.Products.FindAsync(id);
                if (products != null) return products;
                else return null;
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
                var productId = _dataContext.Products.Where(_ => _.Code.Equals(code)).Select(_ => _.ProductId).FirstOrDefault();
                if (productId != null) return productId;
                else return 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetProductIdByCode)} - Error while getting Products. Message={ex.Message}");
                return 0;
            }
        }
        public double? GetUnitPriceByCode(string code)
        {
            try
            {
                var productId = _dataContext.Products.Where(_ => _.Code.Equals(code)).Select(_ => _.List_Price).FirstOrDefault();
                if (productId != null) return productId;
                else return 0;
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
                Product product = _dataContext.Products.Where(_ => _.Code.Equals(code)).FirstOrDefault();

                if (product != null)
                {
                    product.Quantity = quantity;
                    _dataContext.Entry(product).CurrentValues.SetValues(product);
                    _dataContext.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetProductIdByCode)} - Error while getting Products. Message={ex.Message}");
            }
        }

        public int GetQuantity(string code)
        {
            try
            {
                int? quantity = _dataContext.Products.Where(_ => _.Code.Equals(code)).Select(_ => _.Quantity).FirstOrDefault();
                if (quantity != null) return (int)quantity;
                else return 0;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetProductIdByCode)} - Error while getting Products. Message={ex.Message}");
                return 0;
            }
        }

        public Product? AddProduct(Product request)
        {
            try
            {
                _dataContext.Products.Add(request);
                _dataContext.SaveChanges();
                return(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(AddProduct)} - Error while adding Products. Message={ex.Message}");
                return null;
            }
        }
    }
}