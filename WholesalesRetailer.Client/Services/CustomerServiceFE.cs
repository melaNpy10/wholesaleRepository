using System.Net.Http.Json;
using WholesalesRetailer.Client.Pages;
using WholesalesRetailer.Client.Services.Interfaces;
using WholesalesRetailer.Shared.Models;

namespace WholesalesRetailer.Client.Services
{
    public class CustomerServiceFE : ICustomerServiceFE
    {
        private readonly HttpClient _httpClient;
        public CustomerServiceFE(HttpClient http)
        {
            _httpClient = http;
        }

        public async Task<List<Customer>>? GetCustomers()
        {
            return await _httpClient.GetFromJsonAsync<List<Customer>>("api/Customer/GetCustomers");
        }

        public async Task<Customer> CreateCustomer(CustomerFE request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Customer/CreateCustomer", request);
            return await result.Content.ReadFromJsonAsync<Customer>();
        }

        public async Task<List<CustomerRebates?>?> GetCustomersRebates()
        {
            var result = await _httpClient.GetFromJsonAsync<List<CustomerRebates>?>("/api/Customer/GetCustomersRebates");
            return result;
        }
    }
}
