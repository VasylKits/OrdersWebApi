using Domain.Entities;
using Interfaces.Repositories;

namespace Application.Interfaces.Repositories;

public interface IOrderRepository : IBaseRepository<Order>
{
    Task<List<Order>> GetOrdersByUserIdAsync(int userId);
    Task<Order> GetOrderByUserIdAndDateAsync(int userId, DateTime orderDate);
}