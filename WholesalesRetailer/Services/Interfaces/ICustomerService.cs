using WholesalesRetailer.Shared.Models;

namespace WholesalesRetailer.Services.Interfaces
{
    public interface ICustomerService
    {
        List<Customer>? GetCustomers();
        Task<Customer?> GetCustomerById(int id);
        Customer? CreateCustomer(Customer newCustomer);
        Task<List<CustomerRebates>> GetCustomersRebates();
    }
}