using Application.Common.Result.Implementations;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [ProducesResponseType(typeof(List<OrderResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorInfo), StatusCodes.Status400BadRequest)]
    [HttpGet]
    public async Task<ActionResult> GetAllAsync()
    {
        var result = await _orderService.GetOrdersAsync();

        return result.Success ? Ok(result.Data) : BadRequest(result.ErrorInfo.Error);
    }

    [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorInfo), StatusCodes.Status400BadRequest)]
    [HttpGet("{userId}")]
    public async Task<ActionResult<List<OrderResponse>>> GetOrdersByUserIdAsync([FromRoute] int userId)
    {
        var result = await _orderService.GetOrdersByUserIdAsync(userId);

        return result.Success ? Ok(result.Data) : BadRequest(result.ErrorInfo.Error);
    }

    [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorInfo), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<OrderResponse>> CreateAsync([FromBody] OrderAddDto orderDto)
    {
        var result = await _orderService.AddOrderAsync(orderDto);

        return result.Success ? Ok(result.Data) : BadRequest(result.ErrorInfo.Error);
    }

    [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorInfo), StatusCodes.Status400BadRequest)]
    [HttpPut]
    public async Task<ActionResult<OrderResponse>> UpdateAsync([FromBody] OrderEditDto orderEditDto)
    {
        var result = await _orderService.UpdateOrderAsync(orderEditDto);

        return result.Success ? Ok(result.Data) : BadRequest(result.ErrorInfo.Error);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorInfo), StatusCodes.Status400BadRequest)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int id)
    {
        var result = await _orderService.DeleteOrderAsync(id);

        return result.Success ? Ok() : BadRequest(result.ErrorInfo.Error);
    }
}