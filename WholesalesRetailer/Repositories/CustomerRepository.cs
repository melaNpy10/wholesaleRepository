using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Collections.Generic;
using WholesalesRetailer.Client.Pages;
using WholesalesRetailer.Data;
using WholesalesRetailer.Repositories.Interfaces;
using WholesalesRetailer.Shared.Models;

namespace WholesalesRetailer.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        readonly ILogger<ProductRepository> _logger;
        private readonly DataContext _dataContext;
        public CustomerRepository(ILogger<ProductRepository> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        public List<Customer>? GetCustomers()
        {
            try
            {
                return _dataContext.Customers.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetCustomers)} - Error while getting Users. Message={ex.Message}");
                return null;
            }
        }

        public async Task<Customer?> GetCustomerById(int id)
        {
            try
            {
                var customer = await _dataContext.Customers.FindAsync(id);
                if (customer != null) return customer;
                else return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetCustomerById)} - Error while getting Products. Message={ex.Message}");
                return null;
            }
        }

        public Customer? CreateCustomer(Customer newCustomer)
        {
            try
            {   
                _dataContext.Customers.Add(newCustomer);
                _dataContext.SaveChanges();
                return(newCustomer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetCustomerById)} - Error while getting Products. Message={ex.Message}");
                return null;
            }
        }
        public string? GetCustomerName(int id)
        {

            try
            {
                string? name = _dataContext.Customers.Where(_ => _.CustomerId == id).Select(_ => _.CustomerName).FirstOrDefault();
                return name;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetCustomers)} - Error while getting Users. Message={ex.Message}");
                return null;
            }
        }

        public double? GetRebateValue(int customerId)
        {
            try
            {
                return _dataContext.CashBacks.Where(_ => _.CustomerId == customerId).Select(_ => _.Value).FirstOrDefault();
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetRebateValue)} - Error while getting CashBack. Message={ex.Message}");
                return null;
            }
        }

        public double? GetRebateProcent(int id)
        {
            try
            {
                return _dataContext.Rebates.Where(_ => _.RebateId == id).Select(_ => _.Value).FirstOrDefault();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetRebateValue)} - Error while getting CashBack. Message={ex.Message}");
                return null;
            }
        }
    }
}
