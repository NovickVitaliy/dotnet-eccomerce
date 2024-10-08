using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.API.Controllers.Common;
using OrderManagement.Business.DTOs.Order;
using OrderManagement.Business.Services.Contracts;

namespace OrderManagement.API.Controllers;

[Route("/api/orders")]
public class OrderController : BaseApiController
{
    private readonly IOrderService _orderService;
    
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateOrderRequest request)
    {
        var result = await _orderService.CreateOrderAsync(request);
        if (!result.Success)
        {
            return BadRequest();
        }

        return Created($"/api/orders/{result.Data}", new {id = result.Data});
    }
    
    //TODO: in future for admins
    // [Authorize]
    [HttpPost("change-status")]
    public async Task<IActionResult> ChangeStatusAsync(ChangeOrderStatusRequest request)
    {
        var result = await _orderService.ChangeStatusAsync(request);
        if (!result.Success)
        {
            return NotFound();
        }

        return NoContent();
    }
}