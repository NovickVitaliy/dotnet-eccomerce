using Microsoft.AspNetCore.Mvc;
using ProductInventory.API.Controllers.Common;
using ProductInventory.Business.DTOs.ProductDetail;
using ProductInventory.Business.Services.Conctracts;

namespace ProductInventory.API.Controllers;

[Route("/api/product-details")]
public class ProductDetailController : BaseApiController
{
    private readonly IProductDetailService _productDetailService;
    
    public ProductDetailController(IProductDetailService productDetailService)
    {
        _productDetailService = productDetailService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductDetailRequest request)
    {
        var response = await _productDetailService.CreateProductDetailAsync(request);
        if (!response.Success)
        {
            return BadRequest(response.Description);
        }

        return Created($"/api/product-details/{response.Data}", new { id = response.Data });
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetProductDetailsRequest request)
    {
        var response = await _productDetailService.GetProductDetailsAsync(request);
        if (!response.Success)
        {
            return BadRequest(response.Description);
        }

        return Ok(response.Data);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await _productDetailService.GetProductDetailsByIdAsync(new GetProductDetailByIdRequest(id));
        if (!response.Success)
        {
            return NotFound(response.Description);
        }

        return Ok(response.Data);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateProductDetailRequest request)
    {
        request = request with { ProductDetailId = id };
        var response = await _productDetailService.UpdateProductDetailAsync(request);
        if (!response.Success)
        {
            return NotFound(response.Description);
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _productDetailService.DeleteProductDetailAsync(new DeleteProductDetailRequest(id));
        if (!response.Success)
        {
            return NotFound(response.Description);
        }

        return NoContent();
    }
}