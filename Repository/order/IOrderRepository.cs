using Entities;

namespace Repository.order
{
    public interface IOrderRepository
    {
        Task<Order> AddOrder(Order order);
        Task<IEnumerable<Order>> Get();
    }
}