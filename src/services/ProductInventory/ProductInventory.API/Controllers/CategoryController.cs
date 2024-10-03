using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProductInventory.API.Controllers.Common;
using ProductInventory.DataAccess.Repositories.Contracts;
using ProductInventory.Domain.Models;

namespace ProductInventory.API.Controllers;
[Route("/api/categories")]
public class CategoryController : BaseApiController
{
    private readonly ICategoryRepository _categoryRepository;
    public CategoryController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    [HttpPost]
    public async Task<Results<BadRequest, Created>> CreateCategory(Category category)
    {
        var id = await _categoryRepository.CreateCategoryAsync(category);

        return TypedResults.Created($"/api/categories/{id}");
    }
}