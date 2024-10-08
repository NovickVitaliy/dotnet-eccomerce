using Microsoft.AspNetCore.Mvc;
using OrderManagement.API.Controllers.Common;
using OrderManagement.Business.DTOs.Customer;
using OrderManagement.Business.Services.Contracts;

namespace OrderManagement.API.Controllers;

[Route("/api/customers")]
public class CustomerController : BaseApiController
{
    private readonly ICustomerService _customerService;
    
    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateCustomerRequest request)
    {
        var result = await _customerService.CreateCustomerAsync(request);
        if (!result.Success)
        {
            return BadRequest(result.Description);
        }

        return Created($"/api/customers/{result.Data}", new {id = result.Data});
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        var result = await _customerService.GetCustomersAsync(pageNumber, pageSize);
        if (!result.Success)
        {
            return BadRequest();
        }

        return Ok(result.Data);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var result = await _customerService.GetCustomerByIdAsync(id);
        if (!result.Success)
        {
            return NotFound();
        }

        return Ok(result.Data);
    }
}