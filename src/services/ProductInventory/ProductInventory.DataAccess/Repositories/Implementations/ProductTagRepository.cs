using Dapper;
using ProductInventory.DataAccess.Persistance;
using ProductInventory.DataAccess.Repositories.Contracts;
using ProductInventory.Domain.Models;

namespace ProductInventory.DataAccess.Repositories.Implementations;

public class ProductTagRepository : IProductTagRepository
{
    private readonly DbConnectionAccessor _dbConnectionAccessor;

    public ProductTagRepository(DbConnectionAccessor dbConnectionAccessor)
    {
        _dbConnectionAccessor = dbConnectionAccessor;
    }

    public async Task<int> CreateProductTagAsync(ProductTag productTag)
    {
        await using var connection = _dbConnectionAccessor.GetConnection();
        var id = await connection.QuerySingleAsync<int>(
            "INSERT INTO ProductTag (name) VALUES (@name); " +
            "SELECT CAST(SCOPE_IDENTITY() AS INT);",
            new { name = productTag.Name });

        return id;
    }

    public async Task<ProductTag?> GetProductTagByIdAsync(int productTagId)
    {
        await using var connection = _dbConnectionAccessor.GetConnection();
        var productTag = await connection.QuerySingleOrDefaultAsync<ProductTag>(
            "SELECT * FROM ProductTag WHERE ProductTagId = @productTagId",
            new { productTagId });

        return productTag;
    }

    public async Task<List<ProductTag>> GetProductTagsAsync(int pageNumber, int pageSize)
    {
        await using var connection = _dbConnectionAccessor.GetConnection();
        int skip = (pageNumber - 1) * pageSize;
        int take = pageSize;

        var productTags = (await connection.QueryAsync<ProductTag>(
            "SELECT * FROM ProductTag " +
            "ORDER BY ProductTagId " +
            "OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY",
            new
            {
                skip, take
            })).ToList();

        return productTags;
    }

    public async Task<bool> DeleteProductTagAsync(int productTagId)
    {
        await using var connection = _dbConnectionAccessor.GetConnection();
        var rowsAffected = await connection.ExecuteAsync(
            "DELETE FROM ProductTag WHERE ProductTagId = @productTagId",
            new {productTagId});

        return rowsAffected == 1;
    }

    public async Task<bool> UpdateProductTagAsync(ProductTag productTag)
    {
        await using var connection = _dbConnectionAccessor.GetConnection();
        var rowsAffected = await connection.ExecuteAsync(
            "UPDATE ProductTag SET " +
            "name = @name " +
            "WHERE ProductTagId = @productTagId", new
            {
                name = productTag.Name,
                productTagId = productTag.ProductTagId
            });

        return rowsAffected == 1;
    }
}