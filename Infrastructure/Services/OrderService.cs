using AutoMapper;
using Application.Common.Constants;
using Application.Common.Result.Abstractions;
using Application.Common.Result.Abstractions.Generics;
using Application.Common.Result.Implementations;
using Application.Common.Result.Implementations.Generics;
using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;

    public OrderService(
        IMapper mapper,
        IOrderRepository orderRepository,
        IUserRepository userRepository)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
        _userRepository = userRepository;
    }

    public async Task<IResult<List<OrderResponse>>> GetOrdersAsync()
    {
        try
        {
            var orders = await _orderRepository.GetAllAsync();

            if (orders.Count == 0)
                return Result<List<OrderResponse>>.CreateFailed(ErrorModel.OrderNotFound);

            var orderDto = _mapper.Map<List<OrderResponse>>(orders);

            return Result<List<OrderResponse>>.CreateSuccess(orderDto);
        }
        catch (Exception e)
        {
            return Result<List<OrderResponse>>.CreateFailed(ErrorModel.OrderNotFound).AddError(e.Message);
        }
    }

    public async Task<IResult<List<OrderResponse>>> GetOrdersByUserIdAsync(int userId)
    {
        try
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);

            if (orders.Count == 0)
                return Result<List<OrderResponse>>.CreateFailed(ErrorModel.OrderNotFound);

            return Result<List<OrderResponse>>.CreateSuccess(_mapper.Map<List<OrderResponse>>(orders));
        }
        catch (Exception e)
        {
            return Result<List<OrderResponse>>.CreateFailed(ErrorModel.OrderNotFound).AddError(e.Message);
        }
    }

    public async Task<IResult<OrderResponse>> AddOrderAsync(OrderAddDto orderDto)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(orderDto.UserId);

            if (!user.Success)
                return Result<OrderResponse>.CreateFailed(ErrorModel.UserNotFound);

            // Check if order for this user and date already exists
            var existingOrder = await _orderRepository.GetOrderByUserIdAndDateAsync(orderDto.UserId, orderDto.OrderDate);

            if (existingOrder != null)
                return Result<OrderResponse>.CreateFailed(ErrorModel.OnlyOneOrderMayBeCreatedForUserPerDay);

            var order = _mapper.Map<Order>(orderDto);

            await _orderRepository.AddAsync(order);

            return Result<OrderResponse>.CreateSuccess(_mapper.Map<OrderResponse>(order));
        }
        catch (Exception e)
        {
            return Result<OrderResponse>.CreateFailed(ErrorModel.OrderIsNotCreated).AddError(e.Message);
        }
    }

    public async Task<IResult<OrderResponse>> UpdateOrderAsync(OrderEditDto orderEditDto)
    {
        try
        {
            var order = await _orderRepository.GetByIdAsync(orderEditDto.Id);

            if (!order.Success)
                return Result<OrderResponse>.CreateFailed(ErrorModel.OrderNotFound);

            var user = await _userRepository.GetByIdAsync(orderEditDto.UserId);

            if (!user.Success)
                return Result<OrderResponse>.CreateFailed(ErrorModel.UserNotFound);

            var existingOrder = await _orderRepository.GetOrderByUserIdAndDateAsync(orderEditDto.UserId, orderEditDto.OrderDate);

            if (existingOrder != null && existingOrder.Id != orderEditDto.Id)
                return Result<OrderResponse>.CreateFailed(ErrorModel.OnlyOneOrderMayBeCreatedForUserPerDay);

            _mapper.Map(orderEditDto, order.Data);

            var orderResult = await _orderRepository.UpdateAsync(order.Data);

            var updatedOrderDto = _mapper.Map<OrderResponse>(orderResult);

            return Result<OrderResponse>.CreateSuccess(updatedOrderDto);
        }
        catch (Exception e)
        {
            return Result<OrderResponse>.CreateFailed(ErrorModel.OrderIsNotUpdated).AddError(e.Message);
        }
    }

    public async Task<IResult> DeleteOrderAsync(int id)
    {
        try
        {
            var orderResult = await _orderRepository.GetByIdAsync(id);

            if (!orderResult.Success)
                return Result.CreateFailed(ErrorModel.OrderNotFound);

            await _orderRepository.DeleteAsync(orderResult.Data);

            return Result.CreateSuccess();
        }
        catch (Exception e)
        {
            return Result.CreateFailed(ErrorModel.OrderIsNotDeleted).AddError(e.Message);
        }
    }
}