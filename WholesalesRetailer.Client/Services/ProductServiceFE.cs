using System.Net.Http.Json;
using WholesalesRetailer.Client.Services.Interfaces;
using WholesalesRetailer.Shared.Models;

namespace WholesalesRetailer.Client.Services
{
    public class ProductServiceFE : IProductServiceFE
    {
        private readonly HttpClient _httpClient;
        public ProductServiceFE(HttpClient http)
        {
            _httpClient = http;
        }
        public async Task<List<Product?>> GetProducts()
        {
            return await _httpClient.GetFromJsonAsync<List<Product?>>("api/product/GetProducts");
        }
        public async Task<Product?> AddProduct(ProductFE request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Product/AddProduct", request);
            return await result.Content.ReadFromJsonAsync<Product>();
        }
    }
}