using System.Data.Common;
using Dapper;
using ProductInventory.DataAccess.Persistance;
using ProductInventory.DataAccess.Repositories.Contracts;
using ProductInventory.Domain.Models;

namespace ProductInventory.DataAccess.Repositories.Implementations.Product;

public class ProductRepository : IProductRepository
{
    private readonly DbConnectionAccessor _dbConnectionAccessor;

    public ProductRepository(DbConnectionAccessor dbConnectionAccessor)
    {
        _dbConnectionAccessor = dbConnectionAccessor;
    }

    public async Task<int> CreateProductAsync(Domain.Models.Product product)
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

    public async Task<Domain.Models.Product?> GetProductByIdAsync(int productId)
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
    private async Task AppendProductTagsToProduct(Domain.Models.Product product, DbConnection connection)
    {
        var productTags = await connection.QueryAsync<Domain.Models.ProductTag>(
            "SELECT pt.ProductTagId, pt.Name" +
            " FROM ProductTagMapping ptm " +
            "INNER JOIN ProductTag pt ON ptm.productTagId = pt.ProductTagId " +
            "WHERE ptm.productId = @productId", new
            {
                productId = product.ProductId
            });
        product.ProductTags = productTags.ToList();
    }

    private async Task AppendProductDetailToProduct(Domain.Models.Product product, DbConnection connection)
    {
        var productDetail = await connection.QuerySingleOrDefaultAsync<ProductDetail>(
            "SELECT * FROM ProductDetail WHERE ProductDetailId = @productDetailid",
            new { productDetailId = product.ProductId });
        product.ProductDetail = productDetail;
    }
    private async Task AppendSupplierToProduct(Domain.Models.Product product, DbConnection connection)
    {
        var supplier = await connection.QuerySingleOrDefaultAsync<Domain.Models.Supplier>(
            "SELECT * FROM Supplier WHERE SupplierId = @supplierId",
            new { supplierId = product.SupplierId });
        product.Supplier = supplier;
    }
    private async Task AppendCategoryToProduct(Domain.Models.Product product, DbConnection connection)
    {
        var category = await connection.QuerySingleOrDefaultAsync<Domain.Models.Category>(
            "SELECT * FROM Category WHERE CategoryId = @categoryId",
            new { categoryId = product.CategoryId });
        product.Category = category;
    }
    private async Task<Domain.Models.Product?> ReadProductAsync(DbConnection connection, int productId)
    {
        return await connection.QuerySingleOrDefaultAsync<Domain.Models.Product>(
            "SELECT * FROM Product WHERE ProductId = @productId",
            new { productId });
    }

    public async Task<List<Domain.Models.Product>> GetProductsAsync(int pageNumber, int pageSize)
    {
        string sql = @"
        SELECT 
            p.ProductId, p.Name, p.Description, p.Price, p.StockQuantity, p.SupplierId, p.CategoryId, 
            p.CategoryId, c.CategoryId, c.Name, c.Description,
            p.SupplierId, s.SupplierId, s.Name, s.Address, s.ContactInfo, 
            pd.ProductDetailId, pd.weight, pd.warrantyPeriod, pd.length, pd.height, pd.color, pd.width, 
            pt.ProductTagId, pt.Name AS TagName
        FROM Product p
        INNER JOIN Category c ON p.CategoryId = c.CategoryId
        INNER JOIN Supplier s ON p.SupplierId = s.SupplierId
        LEFT JOIN ProductDetail pd ON p.ProductId = pd.ProductDetailId
        LEFT JOIN ProductTagMapping ptm ON p.ProductId = ptm.ProductId
        LEFT JOIN ProductTag pt ON ptm.ProductTagId = pt.ProductTagId
        ORDER BY p.ProductId
        OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY;";

        await using var connection = _dbConnectionAccessor.GetConnection();
        var productDictionary = new Dictionary<int, Domain.Models.Product>();

        int offset = (pageNumber - 1) * pageSize; 

        var result = await connection.QueryAsync<Domain.Models.Product, Domain.Models.Category, Domain.Models.Supplier, ProductDetail, Domain.Models.ProductTag, Domain.Models.Product>(
            sql,
            (product, category, supplier, productDetail, productTag) =>
            {
                if (!productDictionary.TryGetValue(product.ProductId, out var productEntry))
                {
                    productEntry = product;
                    productEntry.Category = category;
                    productEntry.Supplier = supplier;
                    productEntry.ProductDetail = productDetail;
                    productEntry.ProductTags = new List<Domain.Models.ProductTag>();
                    productDictionary.Add(product.ProductId, productEntry);
                }

                if (productTag != null)
                {
                    productEntry.ProductTags.Add(productTag);
                }

                return productEntry;
            },
            new
            {
                offset, pageSize
            },
            splitOn: "CategoryId, SupplierId, ProductDetailId, ProductTagId"
        );

        return productDictionary.Values.ToList();
    }

    public async Task<bool> DeleteProductAsync(int productId)
    {
        await using var connection = _dbConnectionAccessor.GetConnection();
        var rowsAffected = await connection.ExecuteAsync(
            "DELETE FROM Product WHERE ProductId = @productId",
            new { productId });

        return rowsAffected == 1;
    }

    public async Task<bool> UpdateProductAsync(Domain.Models.Product product)
    {
        await using var connection = _dbConnectionAccessor.GetConnection();
        var rowsAffected = await connection.ExecuteAsync(
            "UPDATE Product " +
            "SET Name = @name, Description = @description, Price = @price, " +
            "StockQuantity = @stockQuantity, CategoryId = @categoryId, SupplierId = @supplierId WHERE ProductId = @productId",
            new
            {
                name = product.Name,
                description = product.Description,
                price = product.Price,
                stockQuantity = product.StockQuantity,
                categoryId = product.CategoryId,
                supplierId = product.SupplierId,
                productId = product.ProductId
            });

        return rowsAffected == 1;
    }
}