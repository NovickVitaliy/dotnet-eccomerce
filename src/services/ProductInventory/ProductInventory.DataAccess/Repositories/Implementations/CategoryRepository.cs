using Dapper;
using ProductInventory.DataAccess.Persistance;
using ProductInventory.DataAccess.Repositories.Contracts;
using ProductInventory.Domain.Models;

namespace ProductInventory.DataAccess.Repositories.Implementations;

public class CategoryRepository : ICategoryRepository
{
    private readonly DbConnectionAccessor _dbConnectionAccessor;

    public CategoryRepository(DbConnectionAccessor dbConnectionAccessor)
    {
        _dbConnectionAccessor = dbConnectionAccessor;
    }

    public async Task<int> CreateCategoryAsync(Category category)
    {
        await using var connection = _dbConnectionAccessor.GetConnection();
        var id = await connection.QuerySingleAsync<int>(
            "INSERT INTO Category(Name, Description) VALUES (@name, @description); SELECT CAST(SCOPE_IDENTITY() AS int);",
            new { name = category.Name, description = category.Description });

        return id;
    }
    public async Task<Category?> GetCategoryByIdAsync(int categoryId)
    {
        await using var connection = _dbConnectionAccessor.GetConnection();

        var category = await connection.QuerySingleOrDefaultAsync<Category?>("SELECT * FROM Category WHERE CategoryId = @categoryId",
            new { categoryId });

        return category;
    }
    public async Task<List<Category>> GetCategoriesAsync(int pageNumber, int pageSize)
    {
        await using var connection = _dbConnectionAccessor.GetConnection();
        var skip = (pageNumber - 1) * pageSize;
        var take = pageSize;
        var categories = (await connection.QueryAsync<Category>("SELECT * FROM Category ORDER BY CategoryId "
                                                                + "OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY",
            new { skip, take })).ToList();

        return categories;
    }
    public async Task<bool> DeleteCategoryAsync(int categoryId)
    {
        await using var connection = _dbConnectionAccessor.GetConnection();
        const string sql = "DELETE FROM Category WHERE CategoryId = @categoryId";
        var rowsAffected = await connection.ExecuteAsync(sql, new { categoryId });
        return rowsAffected == 1;
    }
    public async Task<bool> UpdateCategoryAsync(Category category)
    {
        await using var connection = _dbConnectionAccessor.GetConnection();
        const string sql = "UPDATE Category SET Name = @name, Description = @description WHERE CategoryId = @categoryId";

        var rowsAffected = await connection.ExecuteAsync(sql, new { name = category.Name, description = category.Description, categoryId = category.CategoryId });
        return rowsAffected == 1;
    }
}