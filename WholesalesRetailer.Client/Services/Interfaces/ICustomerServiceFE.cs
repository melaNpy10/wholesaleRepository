using WholesalesRetailer.Shared.Models;

namespace WholesalesRetailer.Client.Services.Interfaces
{
    public interface ICustomerServiceFE
    {
        Task<List<Customer>>? GetCustomers();
        Task<Customer> CreateCustomer(CustomerFE request);
        Task<List<CustomerRebates?>?> GetCustomersRebates();
    }
}
