using WholesalesRetailer.Client.Pages;
using WholesalesRetailer.Data;
using WholesalesRetailer.Repositories.Interfaces;
using WholesalesRetailer.Shared.Models;

namespace WholesalesRetailer.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        readonly ILogger<OrderRepository> _logger;
        private readonly DataContext _dataContext;

        public OrderRepository(ILogger<OrderRepository> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        public List<OrderFE>? GetOrders()
        {
            try
            {
                var query = from o in _dataContext.Orders
                            join c in _dataContext.Customers on o.CustomerId equals c.CustomerId
                            select new OrderFE
                            {
                                OrderId = o.OrderId,
                                CustomerId = o.CustomerId,
                                CustomerName = c.CustomerName,
                                StatusId = o.StatusId,
                                OrderDate = o.OrderDate,
                                OrderValue = o.OrderValue
                            };
                return query.ToList();                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetOrders)} - Error while getting Orders. Message={ex.Message}");
                return null;
            }
        }

        public async Task<Order?> GetOrderById(int id)
        {
            try
            {
                var order = await _dataContext.Orders.FindAsync(id);
                if (order != null) return order;
                else return null;
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
                var query = from o in _dataContext.Orders
                            join c in _dataContext.Customers on o.CustomerId equals c.CustomerId
                            where o.CustomerId == id
                            select new OrderFE
                            {
                                OrderId = o.OrderId,
                                CustomerId = o.CustomerId,
                                CustomerName = c.CustomerName,
                                StatusId = o.StatusId,
                                OrderDate = o.OrderDate,
                                OrderValue = o.OrderValue
                            };
                var order =  query.ToList(); ;
                if (order != null) return order;
                else return null;
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
                var order = _dataContext.Orders.Where(_ => _.CustomerId == id && _.OrderDate >= date).ToList();
                if (order != null) return order;
                else return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetOrdersByCustomerIdFromDate)} -Error while getting Orders.Message ={ex.Message}");
                return null;
            }
        }
        public void InsertOrderItems(OrderItems orderItem)
        {
            try
            {
                _dataContext.OrderItems.Add(orderItem);
                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(InsertOrderItems)} -Error while inserting Orders Items into Data Base.Message ={ex.Message}");
            }
        }
        public double? GetCashBack(int customerId)
        {
            try
            {
                var value = _dataContext.CashBacks.Where(_ => _.CustomerId.Equals(customerId)).Select(_ => _.Value).FirstOrDefault(); ;
                return value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetCashBack)} -Error while Geting CashBack for {nameof(customerId)}.Message ={ex.Message}");
                return null;
            }
        }

        public double? GetRebate(int customerId)
        {
            try
            {
                var value = _dataContext.Customers.Where(_ => _.CustomerId.Equals(customerId)).Select(_ => _.RebateId).FirstOrDefault();
                var rebate = _dataContext.Rebates.Where(_ => _.RebateId.Equals(value)).Select(_ => _.Value).FirstOrDefault();
                return rebate;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetCashBack)} -Error while Geting CashBack for {nameof(customerId)}.Message ={ex.Message}");
                return null;
            }
        }

        public void InsertCashBack(CashBack cashBack)
        {
            try
            {
                CashBack val  = _dataContext.CashBacks.Where(_ => _.CustomerId.Equals(cashBack.CustomerId)).FirstOrDefault();
                if(val!=null)
                {
                    val.Value = cashBack.Value;
                    _dataContext.Entry(cashBack).CurrentValues.SetValues(val);
                    _dataContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(InsertCashBack)} -Error while inserting CashBack into Data Base.Message ={ex.Message}");
            }
        }

        public int? GetLastOrderId()
        {
            try
            {
                int? id = _dataContext.Orders.Max(_ => (int?)_.OrderId);
                if (id != null) return id;
                else return 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(InsertCashBack)} -Error while inserting CashBack into Data Base.Message ={ex.Message}");
                return 0;
            }
        }

        public void InsertOrder(Order order)
        {
            try
            {
                _dataContext.Orders.Add(order);
                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(InsertOrder)} -Error while inserting Orders into Data Base.Message ={ex.Message}");
            }
        }
    }
}
