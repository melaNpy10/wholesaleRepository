using WholesalesRetailer.Shared.Models;

namespace WholesalesRetailer.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetOrderById(int id);
        List<OrderFE>? GetOrders();
        List<OrderFE>? GetOrdersByCustomerId(int id);
        List<Order>? GetOrdersByCustomerIdFromDate(int id, DateTime date);
        void InsertOrderItems(OrderItems orderItem);
        double? GetCashBack(int customerId);
        double? GetRebate(int customerId);
        void InsertCashBack(CashBack cashBack);
        int? GetLastOrderId();
        void InsertOrder(Order order);
    }
}