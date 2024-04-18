using Microsoft.AspNetCore.Mvc;
using WholesalesRetailer.Services.Interfaces;
using WholesalesRetailer.Shared.Models;

namespace WholesalesRetailer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [HttpGet]
        public ActionResult<List<OrderFE>> GetOrders()
        {
            try
            {
                var response = _orderService.GetOrders();
                if (response != null) return Ok(response);
                else return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetOrders)} - Error while getting Orders. Message={ex.Message}");
                return StatusCode(500);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Order>>> GetOrdersById(int id)
        {
            try
            {
                var response = await _orderService.GetOrderById(id);
                if (response != null) return Ok(response);
                else return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetOrdersById)} - Error while getting Orders. Message={ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpGet("{id}/{date}")]
        public ActionResult<List<Order>> GetOrdersByCustomerIdFromDate(int id, DateTime date)
        {
            try
            {
                var response = _orderService.GetOrdersByCustomerIdFromDate(id, date);
                if (response != null) return Ok(response);
                else return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetOrdersByCustomerIdFromDate)} - Error while getting Orders. Message={ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost]
        public ActionResult<List<OrderFE>> AddNewOrders(ReceiveOrder orderFilters)
        {
            try
            {
                var response = _orderService.AddNewOrder(orderFilters);
                if (response != null) return Ok(response);
                else return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(AddNewOrders)} - Error while getting Orders. Message={ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost]
        public ActionResult<List<OrderFE>> GetOrdersByCustomerId(Filter request)
        {
            try
            {
                var response = _orderService.GetOrdersByCustomerId(request.CustomerId);
                if (response != null) return Ok(response);
                else return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetOrdersByCustomerId)} - Error while getting Orders. Message={ex.Message}");
                return StatusCode(500);
            }
        }

    }
}
