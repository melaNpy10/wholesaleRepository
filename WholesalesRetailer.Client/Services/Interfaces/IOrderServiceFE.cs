using WholesalesRetailer.Shared.Models;

namespace WholesalesRetailer.Client.Services.Interfaces
{
    public interface IOrderServiceFE
    {
        Task<List<OrderFE>>? GetOrders();

        Task<List<OrderFE>> GetOrdersByCustomerId(int request);

        Task<OrderFE> CreateOrder(OrderRequest request);
    }
}
