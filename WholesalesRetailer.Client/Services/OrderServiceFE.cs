using System.Net.Http.Json;
using WholesalesRetailer.Client.Services.Interfaces;
using WholesalesRetailer.Shared.Models;

namespace WholesalesRetailer.Client.Services
{
    public class OrderServiceFE : IOrderServiceFE
    {
        private readonly HttpClient _httpClient;
        public OrderServiceFE(HttpClient http)
        {
            _httpClient = http;
        }

        public async Task<List<OrderFE>>? GetOrders()
        {
            return await _httpClient.GetFromJsonAsync<List<OrderFE>>("api/order/GetOrders");
        }

        public async Task<List<OrderFE>> GetOrdersByCustomerId(int request)
        {
            Filter id = new Filter() { CustomerId = request };
            var result = await _httpClient.PostAsJsonAsync("api/Order/GetOrdersByCustomerId", id);
            return await result.Content.ReadFromJsonAsync<List<OrderFE?>>();
        }

        public async Task<OrderFE?> CreateOrder(OrderRequest? request)
        {
            List<ProductData> prod = new();
            ProductData product= new ProductData()
            {
                ProductCode = request.Code,
                Quantity = (int)request.Quantity
            };
            prod.Add(product);
            ReceiveOrder newRequest = new ReceiveOrder()
            {
                CustomerId = (int)request.CustomerId,
                ProductData = prod
            };

            var result = await _httpClient.PostAsJsonAsync("api/Order/AddNewOrders", newRequest);
            return await result.Content.ReadFromJsonAsync<OrderFE?>();
        }

    }
}
