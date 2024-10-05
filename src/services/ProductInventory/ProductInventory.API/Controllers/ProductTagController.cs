using Microsoft.AspNetCore.Mvc;
using ProductInventory.API.Controllers.Common;
using ProductInventory.Business.DTOs.ProductTag;
using ProductInventory.Business.Services.Conctracts;

namespace ProductInventory.API.Controllers;

[Route("/api/product-tags")]
public class ProductTagController : BaseApiController
{
    private readonly IProductTagService _productTagService;
    
    public ProductTagController(IProductTagService productTagService)
    {
        _productTagService = productTagService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductTagRequest request)
    {
        var response = await _productTagService.CreateProductTagAsync(request);
        if (!response.Success)
        {
            return BadRequest();
        }

        return Created("/api/product-tags/{id}", new {id = response.Data});
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetProductTagsRequest request)
    {
        var response = await _productTagService.GetProductTagsAsync(request);
        if (!response.Success)
        {
            return BadRequest();
        }

        return Ok(response.Data);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await _productTagService.GetProductTagByIdAsync(new GetProductTagByIdRequest(id));
        if (!response.Success)
        {
            return NotFound();
        }

        return Ok(response.Data);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateProductTagRequest request)
    {
        request = request with { ProductTagId = id };
        var response = await _productTagService.UpdateProductTagAsync(request);
        if (!response.Success)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _productTagService.DeleteProductTagAsync(new DeleteProductTagRequest(id));
        if (!response.Success)
        {
            return NotFound();
        }

        return NoContent();
    }
}