using Application.Common.Result.Abstractions;
using Application.Common.Result.Abstractions.Generics;
using Application.DTOs;

namespace Application.Interfaces;

public interface IOrderService
{
    Task<IResult<List<OrderResponse>>> GetOrdersAsync();
    Task<IResult<List<OrderResponse>>> GetOrdersByUserIdAsync(int userId);
    Task<IResult<OrderResponse>> AddOrderAsync(OrderAddDto orderDto);
    Task<IResult<OrderResponse>> UpdateOrderAsync(OrderEditDto orderDto);
    Task<IResult> DeleteOrderAsync(int orderId);
}