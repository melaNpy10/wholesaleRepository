using WholesalesRetailer.Client.Pages;
using WholesalesRetailer.Shared.Models;

namespace WholesalesRetailer.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetCustomerById(int id);
        List<Customer>? GetCustomers();
        Customer? CreateCustomer(Customer newCustomer);
        string? GetCustomerName(int id);
        double? GetRebateValue(int customerId);
        double? GetRebateProcent(int id);
    }
}