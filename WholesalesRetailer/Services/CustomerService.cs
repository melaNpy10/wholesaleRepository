using WholesalesRetailer.Client.Pages;
using WholesalesRetailer.Repositories.Interfaces;
using WholesalesRetailer.Services.Interfaces;
using WholesalesRetailer.Shared.Models;

namespace WholesalesRetailer.Services
{
    public class CustomerService : ICustomerService
    {
        readonly ILogger<CustomerService> _logger;
        private readonly ICustomerRepository _userRepository;
        public CustomerService(ILogger<CustomerService> logger, ICustomerRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }
        public List<Customer>? GetCustomers()
        {
            try
            {
                return _userRepository.GetCustomers();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetCustomers)} - Error while getting Users. Message={ex.Message}");
                return null;
            }
        }

        public Task<Customer?> GetCustomerById(int id)
        {
            try
            {
                return _userRepository.GetCustomerById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetCustomerById)} - Error while getting Products. Message={ex.Message}");
                return null;
            }

        }

        public  Customer? CreateCustomer(Customer newCustomer)
        {
            try
            {
               return _userRepository.CreateCustomer(newCustomer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetCustomers)} - Error while getting Users. Message={ex.Message}");
                return null;
            }
        }
        public async Task<List<CustomerRebates>> GetCustomersRebates()
        {
            try
            {
                List<CustomerRebates> customers = new List<CustomerRebates>();
                List<Customer?> custList = _userRepository.GetCustomers();
                foreach(var cusReb in custList)
                {
                    var customerRebate = new CustomerRebates()
                    {
                        CustomerId = cusReb.CustomerId,
                        CustomerName = cusReb.CustomerName,
                        Adress = cusReb.Adress,
                        Email = cusReb.Email,
                        RebateId = cusReb.RebateId,
                        Procent = cusReb.RebateId != null? _userRepository.GetRebateProcent((int)cusReb.RebateId) : null,
                        Value = _userRepository.GetRebateValue(cusReb.CustomerId)
                    };
                    customers.Add(customerRebate);
                }
                
                return customers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetCustomers)} - Error while getting Users. Message={ex.Message}");
                return null;
            }
        }
    }
}
