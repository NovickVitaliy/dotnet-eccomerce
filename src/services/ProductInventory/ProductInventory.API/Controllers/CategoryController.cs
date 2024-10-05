using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProductInventory.API.Controllers.Common;
using ProductInventory.Business.DTOs.Category;
using ProductInventory.Business.Services.Conctracts;
using ProductInventory.DataAccess.Repositories.Contracts;
using ProductInventory.Domain.Models;

namespace ProductInventory.API.Controllers;

[Route("/api/categories")]
public class CategoryController : BaseApiController
{
    private readonly ICategoryService _categoryService;
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CreateCategoryRequest request)
    {
        var result = await _categoryService.CreateCategoryAsync(request);
        if (!result.Success)
        {
            return BadRequest();
        }
        return Created("/api/categories/{id}", new
        {
            id = result.Data
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories([FromQuery] GetCategoriesRequest request)
    {
        var response = await _categoryService.GetCategoriesAsync(request);
        if (!response.Success)
        {
            return BadRequest();
        }

        return Ok(response.Data);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCategoryById([FromQuery] GetCategoryByIdRequest request)
    {
        var response = await _categoryService.GetCategoryByIdAsync(request);
        if (!response.Success)
        {
            return NotFound();
        }

        return Ok(response.Data);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryRequest request)
    {
        var response = await _categoryService.UpdateCategoryAsync(request);
        if (!response.Success)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCategory(int id, DeleteCategoryRequest request)
    {
        var response = await _categoryService.DeleteCategoryAsync(request);
        if (!response.Success)
        {
            return NotFound();
        }

        return NoContent();
    }
}