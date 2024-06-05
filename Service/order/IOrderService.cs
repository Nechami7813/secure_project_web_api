using Entities;

namespace Service
{
    public interface IOrderService
    {
        Task<CustomHttpResponse<Order>> AddOrder(Order order);
        Task<IEnumerable<Order>> GetOrders();
    }
}