namespace ProductInventory.Business.DTOs.Category;

public record UpdateCategoryRequest(int CategoryId, string Name, string Description);