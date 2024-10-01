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
    ORDER BY id
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