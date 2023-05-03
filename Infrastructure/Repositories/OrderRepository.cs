using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces.Repositories;
using Application.Common.Result.Abstractions.Generics;
using Application.Common.Result.Implementations.Generics;
using Application.Common.Constants;

namespace Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _dbContext;

    public OrderRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await _dbContext.Orders
            .Include(o => o.User)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
    }

    public async Task<IResult<Order>> GetByIdAsync(int id)
    {
        var order = await _dbContext.Orders.FindAsync(id);

        return (order != null)
            ? Result<Order>.CreateSuccess(order)
            : Result<Order>.CreateFailed(ErrorModel.OrderNotFound);
    }

    public async Task<List<Order>> GetOrdersByUserIdAsync(int userId)
    {
        return await _dbContext.Orders
            .Include(o => o.User)
            .Where(o => o.UserId == userId)
            .ToListAsync();
    }

    public async Task<Order> GetOrderByUserIdAndDateAsync(int userId, DateTime orderDate)
    {
        var order = await _dbContext.Orders
            .Where(o => o.UserId == userId && o.OrderDate.Date == orderDate.Date)
            .FirstOrDefaultAsync();

        return order;
    }

    public async Task<Order> AddAsync(Order order)
    {
        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();

        return order;
    }

    public async Task<Order> UpdateAsync(Order order)
    {
        _dbContext.Orders.Update(order);
        await _dbContext.SaveChangesAsync();

        return order;
    }

    public async Task DeleteAsync(Order order)
    {
        _dbContext.Orders.Remove(order);
        await _dbContext.SaveChangesAsync();
    }
}