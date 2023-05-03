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

    [HttpGet]
    public async Task<ActionResult<List<OrderResponse>>> GetAllAsync()
    {
        var result = await _orderService.GetOrdersAsync();

        return result.Success ? Ok(result.Data) : BadRequest(result.ErrorInfo.Error);
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<List<OrderResponse>>> GetOrdersByUserIdAsync([FromRoute] int userId)
    {
        var result = await _orderService.GetOrdersByUserIdAsync(userId);

        return result.Success ? Ok(result.Data) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<OrderResponse>> CreateAsync([FromBody] OrderAddDto orderDto)
    {
        var result = await _orderService.AddOrderAsync(orderDto);

        return result.Success ? Ok(result.Data) : BadRequest(result.ErrorInfo.Error);
    }

    [HttpPut]
    public async Task<ActionResult<OrderResponse>> UpdateAsync([FromBody] OrderEditDto orderEditDto)
    {
        var result = await _orderService.UpdateOrderAsync(orderEditDto);

        return result.Success ? Ok(result.Data) : BadRequest(result.ErrorInfo.Error);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync([FromRoute]int id)
    {
        var result = await _orderService.DeleteOrderAsync(id);

        return result.Success ? NoContent() : BadRequest(result.ErrorInfo.Error);
    }
}