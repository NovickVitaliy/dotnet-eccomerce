CREATE PROCEDURE sp_GetPaginatedProducts(
    @pageNumber INT,
    @pageSize INT
)
AS
BEGIN
    DECLARE @skipItems INT;

    SELECT @skipItems = (@pageNumber - 1) * @pageSize;

    SELECT *
    FROM Product
    ORDER BY ProductId
    OFFSET @skipItems ROWS FETCH NEXT @pageSize ROWS ONLY;
END
GO;


CREATE PROCEDURE sp_InsertProductTag(
    @tagName VARCHAR(50)
)
AS
BEGIN
    INSERT INTO ProductTag(name)
    VALUES (@tagName)
END
GO;

CREATE PROCEDURE sp_InsertCategory(
    @name VARCHAR(50),
    @description VARCHAR(50)
)
AS
BEGIN
    INSERT INTO Category(Name, Description) VALUES (@name, @description)
END
GO;

CREATE PROCEDURE sp_GetAllCategory
AS
BEGIN
    SELECT * FROM Category
END
GO;

CREATE PROCEDURE sp_InsertProduct(
    @name VARCHAR(50),
    @description VARCHAR(50),
    @price INT,
    @stockQuantity INT,
    @categoryId INT,
    @supplierId INT
)
AS
BEGIN
    INSERT INTO Product(Name, Description, Price, StockQuantity, CategoryId, SupplierId)
    VALUES (@name, @description, @price, @stockQuantity, @categoryId, @supplierId)
END
GO;

CREATE PROCEDURE sp_InsertProductDetail(
    @weight INT,
    @height INT,
    @width INT,
    @length INT,
    @color VARCHAR(50),
    @warrantyPeriod TIME
)
AS
BEGIN
    INSERT INTO ProductDetail (weight, height, width, length, color, warrantyPeriod)
    VALUES (@weight, @height, @width, @length, @color, @warrantyPeriod)
END
GO;

CREATE PROCEDURE sp_InsertProductTagMapping(
    @productId INT,
    @productTagId INT
)
AS
BEGIN
    INSERT INTO ProductTagMapping(productId, productTagId)
    VALUES (@productId, @productTagId)
END
GO;

CREATE PROCEDURE sp_InsertSupplier(
    @name VARCHAR(50),
    @contactInfo VARCHAR(50),
    @address VARCHAR(50)
)
AS
BEGIN
    INSERT INTO Supplier(Name, ContactInfo, Address)
    VALUES (@name, @contactInfo, @address)
END
GO;