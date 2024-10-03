using System.Data.Common;
using Dapper;
using ProductInventory.DataAccess.Persistance;
using ProductInventory.DataAccess.Repositories.Contracts;
using ProductInventory.Domain.Models;

namespace ProductInventory.DataAccess.Repositories.Implementations;

public class ProductRepository : IProductRepository
{
    private readonly DbConnectionAccessor _dbConnectionAccessor;
    
    public ProductRepository(DbConnectionAccessor dbConnectionAccessor)
    {
        _dbConnectionAccessor = dbConnectionAccessor;
    }
    
    public async Task<int> CreateProductAsync(Product product)
    {
        await using var connection = _dbConnectionAccessor.GetConnection();
        var id = await connection.QuerySingleAsync<int>(
            "INSERT INTO Product(Name, Description, Price, StockQuantity, CategoryId, SupplierId) " +
            "VALUES (@name, @description, @price, @stockQuantity, @categoryId, @supplierId);" +
            "SELECT CAST(SCOPE_IDENTITY() AS INT);",
            new
            {
                name = product.Name,
                description = product.Description,
                price = product.Price,
                stockQuantity = product.StockQuantity,
                categoryId = product.CategoryId,
                supplierId = product.SupplierId
            });

        return id;
    }
    
    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        await using var connection = _dbConnectionAccessor.GetConnection();
        var product = await ReadProductAsync(connection, productId);
        if (product is null)
        {
            return null;
        }
        await AppendCategoryToProduct(product, connection);
        await AppendSupplierToProduct(product, connection);
        await AppendProductDetailToProduct(product, connection);
        await AppendProductTagsToProduct(product, connection);
        return product;
    }
    private async Task AppendProductTagsToProduct(Product product, DbConnection connection)
    {
        var productTags = await connection.QueryAsync<ProductTag>(
            "SELECT pt.ProductTagId, pt.Name" +
            " FROM ProductTagMapping ptm " +
            "INNER JOIN ProductTag pt ON ptm.productTagId = pt.ProductTagId " +
            "WHERE ptm.productId = @productId", new
            {
                productId = product.ProductId
            });
        product.ProductTags = productTags.ToList();
    }
    
    private async Task AppendProductDetailToProduct(Product product, DbConnection connection)
    {
        var productDetail = await connection.QuerySingleOrDefaultAsync<ProductDetail>(
            "SELECT * FROM ProductDetail WHERE ProductDetailId = @productDetailid",
            new {productDetailId = product.ProductId});
        product.ProductDetail = productDetail;
    }
    private async Task AppendSupplierToProduct(Product product, DbConnection connection)
    {
        var supplier = await connection.QuerySingleOrDefaultAsync<Supplier>(
            "SELECT * FROM Supplier WHERE SupplierId = @supplierId",
            new {supplierId = product.SupplierId});
        product.Supplier = supplier;
    }
    private async Task AppendCategoryToProduct(Product product, DbConnection connection)
    {
        var category = await connection.QuerySingleOrDefaultAsync<Category>(
            "SELECT * FROM Category WHERE CategoryId = @categoryId",
            new {categoryId = product.CategoryId});
        product.Category = category;
    }
    private async Task<Product?> ReadProductAsync(DbConnection connection, int productId)
    {
        return await connection.QuerySingleOrDefaultAsync<Product>(
            "SELECT * FROM Product WHERE ProductId = @productId",
            new {productId});
    }

    public Task<List<Product>> GetProductsAsync(int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }
    
    public async Task<bool> DeleteProductAsync(int productId)
    {
        await using var connection = _dbConnectionAccessor.GetConnection();
        var rowsAffected = await connection.ExecuteAsync(
            "DELETE FROM Product WHERE ProductId = @productId",
            new {productId});

        return rowsAffected == 1;
    }
    
    public async Task<bool> UpdateProductAsync(Product product)
    {
        await using var connection = _dbConnectionAccessor.GetConnection();
        var rowsAffected = await connection.ExecuteAsync(
            "UPDATE Product " +
            "SET Name = @name, Description = @description, Price = @price, " + 
            "StockQuantity = @stockQuantity, CategoryId = @categoryId, SupplierId = @supplierId",
            new
            {
                name = product.Name,
                description = product.Description,
                price = product.Price,
                stockQuantity = product.StockQuantity,
                categoryId = product.CategoryId,
                supplierId = product.SupplierId
            });

        return rowsAffected == 1;
    }
}