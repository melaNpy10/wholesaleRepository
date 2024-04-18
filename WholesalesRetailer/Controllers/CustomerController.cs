using Microsoft.AspNetCore.Mvc;
using WholesalesRetailer.Services.Interfaces;
using WholesalesRetailer.Shared.Models;

namespace WholesalesRetailer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _userService;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<List<Customer>> GetCustomers()
        {
            try
            {
                var response = _userService.GetCustomers();
                if (response != null) return Ok(response);
                else return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetCustomers)} - Error whilegetting Products. Message={ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Customer>>> GetCustomerById(int id)
        {
            try
            {
                var response = await _userService.GetCustomerById(id);
                if (response != null) return Ok(response);
                else return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetCustomerById)} - Error whilegetting Products. Message={ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost]
        public ActionResult<Customer> CreateCustomer(Customer request)
        {
            try
            {
                var response = _userService.CreateCustomer(request);
                if (response != null) return Ok(response);
                else return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(CreateCustomer)} - Error whilegetting Products. Message={ex.Message}");
                return StatusCode(500);
            }

        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerRebates>>> GetCustomersRebates()
        {
            try
            {
                var response = await _userService.GetCustomersRebates();
                if (response != null) return Ok(response);
                else return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetCustomersRebates)} - Error while getting Rebates. Message={ex.Message}");
                return StatusCode(500);
            }
        }
    }
}
