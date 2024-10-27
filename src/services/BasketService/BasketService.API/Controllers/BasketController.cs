using BasketService.Application.Contracts;
using BasketService.Application.DTOs.Basket;
using Microsoft.AspNetCore.Mvc;

namespace BasketService.Controllers;

[Route("/api/baskets")]
public class BasketController : ControllerBase
{
    private readonly IBasketService _basketService;
    
    public BasketController(IBasketService basketService)
    {
        _basketService = basketService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateBasketRequest request)
    {
        var result = await _basketService.CreateBasketAsync(request);
        if (!result.Success)
        {
            return BadRequest(result.Description);
        }

        return Created($"/api/baskets/{result.Data}", new {userId=result.Data});
    }

    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateAsync(string userId, UpdateBasketRequest request)
    {
        var result = await _basketService.UpdateBasketAsync(request);
        if (!result.Success)
        {
            return NotFound(result.Description);
        }

        return NoContent();
    }

    [HttpDelete("{basketId}")]
    public async Task<IActionResult> DeleteAsync(string basketId)
    {
        var result = await _basketService.DeleteBasketAsync(basketId);
        if (!result.Success)
        {
            return NotFound(result.Description);
        }

        return NoContent();
    }

    [HttpGet("{userId:int}")]
    public async Task<IActionResult> GetByUserIdAsync(int userId)
    {
        var result = await _basketService.GetBasketByUserIdAsync(userId);
        if (!result.Success)
        {
            return NotFound(result.Description);
        }

        return Ok(result.Data);
    }
}