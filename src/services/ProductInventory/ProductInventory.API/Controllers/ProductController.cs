using Microsoft.AspNetCore.Mvc;
using ProductInventory.API.Controllers.Common;
using ProductInventory.Business.DTOs.Product;
using ProductInventory.Business.Services.Conctracts;

namespace ProductInventory.API.Controllers;

[Route("/api/products")]
public class ProductController : BaseApiController
{
    private readonly IProductService _productService;
    
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductRequest request)
    {
        var response = await _productService.CreateProductAsync(request);
        if (!response.Success)
        {
            return BadRequest(response.Description);
        }

        return Created($"/api/products/{response.Data}", new {id = response.Data});
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetProductsRequest request)
    {
        var response = await _productService.GetProductsAsync(request);
        if (!response.Success)
        {
            return BadRequest(response.Description);
        }

        return Ok(response.Data);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await _productService.GetProductByIdAsync(new GetProductByIdRequest(id));
        if (!response.Success)
        {
            return NotFound(response.Description);
        }

        return Ok(response.Data);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateProductRequest request)
    {
        request = request with { ProductId = id };
        var response = await _productService.UpdateProductAsync(request);
        if (!response.Success)
        {
            return NotFound(response.Description);
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _productService.DeleteProductAsync(new DeleteProductRequest(id));
        if (!response.Success)
        {
            return NotFound(response.Description);
        }

        return NoContent();
    }
}