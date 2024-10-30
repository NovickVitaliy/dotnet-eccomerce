using Dapper;
using ProductInventory.DataAccess.Persistance;
using ProductInventory.DataAccess.Repositories.Contracts;
using ProductInventory.Domain.Models;

namespace ProductInventory.DataAccess.Repositories.Implementations.ProductDetails;

public class ProductDetailsRepository : IProductDetailsRepository
{
    private readonly DbConnectionAccessor _dbConnectionAccessor;
    public ProductDetailsRepository(DbConnectionAccessor dbConnectionAccessor)
    {
        _dbConnectionAccessor = dbConnectionAccessor;
    }
    public async Task<int> CreateProductDetailAsync(ProductDetail productDetail)
    {
        await using var connection = _dbConnectionAccessor.GetConnection();
        var id = await connection.ExecuteAsync(
            "INSERT INTO ProductDetail(weight, height, width, length, color, warrantyPeriod)" +
            "VALUES (@weigth, @height, @width, @length, @color, @warrantyPeriod);" +
            "SELECT CAST(SCOPE_IDENTITY() AS INT)",
            new
            {
                weigth = productDetail.Weight,
                height = productDetail.Height,
                width = productDetail.Width,
                length = productDetail.Length,
                color = productDetail.Color,
                warrantyPeriod = productDetail.WarrantyPeriod
            });

        return id;
    }
    public async Task<ProductDetail?> GetProductDetailByIdAsync(int productDetailId)
    {
        await using var connection = _dbConnectionAccessor.GetConnection();
        var productDetail = await connection.QuerySingleOrDefaultAsync<ProductDetail?>(
            "SELECT * FROM ProductDetail WHERE ProductDetailId = @productDetailId",
            new { productDetailId });

        return productDetail;
    }
    public async Task<List<ProductDetail>> GetProductDetailsAsync(int pageNumber, int pageSize)
    {
        await using var connection = _dbConnectionAccessor.GetConnection();
        var skip = (pageNumber - 1) * pageSize;
        var take = pageSize;
        var productDetails = (await connection.QueryAsync<ProductDetail>("SELECT * FROM ProductDetail ORDER BY ProductDetailId "
                                                                         + "OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY",
            new { skip, take })).ToList();

        return productDetails;
    }
    public async Task<bool> DeleteProductDetailAsync(int productDetailId)
    {
        await using var connection = _dbConnectionAccessor.GetConnection();
        const string sql = "DELETE FROM ProductDetail WHERE ProductDetailId = @productDetailId";
        var rowsAffected = await connection.ExecuteAsync(sql, new { productDetailId });
        return rowsAffected == 1;
    }
    public async Task<bool> UpdateProductDetailAsync(ProductDetail productDetail)
    {
        await using var connection = _dbConnectionAccessor.GetConnection();
        var rowsAffected = await connection.ExecuteAsync(
            "UPDATE ProductDetail SET " +
            "color = @color, height = @height, length = @length, " +
            " warrantyPeriod = @warrantyPeriod, weight = @weight, width = @width" +
            " WHERE ProductDetailId = @productDetailId",
            new
            {
                color = productDetail.Color,
                height = productDetail.Height,
                length = productDetail.Length,
                warrantyPeriod = productDetail.WarrantyPeriod,
                weight = productDetail.Weight,
                width = productDetail.Width,
                productDetailId = productDetail.ProductDetailId
            });

        return rowsAffected == 1;
    }
}