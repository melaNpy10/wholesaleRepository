using WholesalesRetailer.Repositories.Interfaces;
using WholesalesRetailer.Services.Interfaces;
using WholesalesRetailer.Shared.Models;

namespace WholesalesRetailer.Services
{
    public class OrderService : IOrderService
    {
        readonly ILogger<OrderService> _logger;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;

        public OrderService(ILogger<OrderService> logger, IOrderRepository orderRepository, IProductRepository productRepository, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
        }
        public List<OrderFE>? GetOrders()
        {
            try
            {
                List<OrderFE> list = _orderRepository.GetOrders();
                foreach(var order in list)
                {
                   int statusId = (int)order.StatusId;
                   Status statusOrder = new Status() { StatusDescription = (StatusDescription)statusId };
                   order.OrderStatus = statusOrder.StatusDescription.ToString();

                }
                return list;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetOrders)} - Error while getting Orders. Message={ex.Message}");
                return null;
            }
        }
        public Task<Order?> GetOrderById(int id)
        {
            try
            {
                return _orderRepository.GetOrderById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetOrderById)} - Error while getting Orders. Message={ex.Message}");
                return null;
            }
        }
        public List<OrderFE>? GetOrdersByCustomerId(int id)
        {
            try
            {
                List<OrderFE> list = _orderRepository.GetOrdersByCustomerId(id);
                foreach (var order in list)
                {
                    int statusId = (int)order.StatusId;
                    Status statusOrder = new Status() { StatusDescription = (StatusDescription)statusId };
                    order.OrderStatus = statusOrder.StatusDescription.ToString();

                }
                return list;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetOrdersByCustomerId)} - Error while getting Orders. Message={ex.Message}");
                return null;
            }
        }
        public List<Order>? GetOrdersByCustomerIdFromDate(int id, DateTime date)
        {
            try
            {
                return _orderRepository.GetOrdersByCustomerIdFromDate(id, date);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetOrdersByCustomerIdFromDate)} -Error while getting Orders.Message ={ex.Message}");
                return null;
            }
        }
        public OrderFE AddNewOrder(ReceiveOrder orders)
        {
            var lastOrderId = _orderRepository.GetLastOrderId();
            int newOrderId = (int)(lastOrderId != null ? lastOrderId + 1 : 1);

            double? finalPrice = 0;
            foreach (var prod in orders.ProductData)
            {
                OrderItems orderItem = new OrderItems()
                {
                    OrderId = newOrderId,
                    ProductId = _productRepository.GetProductIdByCode(prod.ProductCode),
                    Quantity = prod.Quantity,
                    UnitPrice = _productRepository.GetUnitPriceByCode(prod.ProductCode)
                };
                _orderRepository.InsertOrderItems(orderItem);
                _productRepository.UpdateProduct(prod.ProductCode, CalculateNewQuantity(prod));
                finalPrice = finalPrice + ((orderItem.UnitPrice != 0 && orderItem.Quantity != 0) ? orderItem.UnitPrice * orderItem.Quantity : 0);
            }
            Dictionary<string, double?> listValues = GetOrderValue(orders.CustomerId, finalPrice);

            OrderFE order = new OrderFE()
            {
                OrderId = newOrderId,
                CustomerId = orders.CustomerId,
                CustomerName = _customerRepository.GetCustomerName(orders.CustomerId),
                StatusId = 2,
                OrderDate = DateTime.UtcNow,
                ProdValue = finalPrice,
                ProcentRabate = listValues["Rebate"],
                CashBack = listValues["CashBack"],
                OrderValue = listValues["FinalValue"],
                FutureCashBack = listValues["FutureCashBack"]
            };
            int statusId = (int)order.StatusId;
            Status statusOrder = new Status() { StatusDescription = (StatusDescription)statusId };
            order.OrderStatus = statusOrder.StatusDescription.ToString();
            _orderRepository.InsertOrder(order);
            return order;
        }
        public Dictionary<string,double?> GetOrderValue(int customerId, double? finalPrice)
        {
            Dictionary<string, double?> listValues = new(); 

            double? orderValue = 0;
            if (finalPrice != 0 && finalPrice != null)
            {
                var value = _orderRepository.GetCashBack(customerId);
                var rebate = _orderRepository.GetRebate(customerId);
                
                if (value != null)
                {
                    listValues.Add("Rebate", null);
                    orderValue = finalPrice - value;
                    _orderRepository.InsertCashBack(new CashBack() { CustomerId = customerId, Value = null });
                    listValues.Add("CashBack", value);
                    listValues.Add("FutureCashBack", null);
                }
                else
                {
                    listValues.Add("Rebate", rebate);
                    orderValue = finalPrice;
                    var cashback = finalPrice * rebate / 100;
                    _orderRepository.InsertCashBack(new CashBack() { CustomerId = customerId, Value = cashback });
                    listValues.Add("FutureCashBack", cashback);
                    listValues.Add("CashBack", null);
                }
                value = _orderRepository.GetCashBack(customerId);
               
            }
            listValues.Add("FinalValue", orderValue);
            return listValues;
        }
        public int CalculateNewQuantity(ProductData prod)
        {
            var prodQuantity = _productRepository.GetQuantity(prod.ProductCode);
            var quantity = prodQuantity - prod.Quantity;
            return quantity;
        }
    }
}
