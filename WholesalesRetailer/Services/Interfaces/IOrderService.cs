using Microsoft.EntityFrameworkCore.Storage.Json;
using WholesalesRetailer.Shared.Models;

namespace WholesalesRetailer.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order?> GetOrderById(int id);
        List<OrderFE>? GetOrders();
        List<OrderFE>? GetOrdersByCustomerId(int id);
        List<Order>? GetOrdersByCustomerIdFromDate(int id, DateTime date);
        public OrderFE AddNewOrder(ReceiveOrder orders);
        Dictionary<string,double?> GetOrderValue(int customerId, double? finalPrice);
        int CalculateNewQuantity(ProductData prod);
    }
}