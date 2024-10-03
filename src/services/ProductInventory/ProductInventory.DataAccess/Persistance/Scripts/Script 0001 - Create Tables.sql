CREATE TABLE Category
(
    CategoryId          INT PRIMARY KEY IDENTITY (1,1),
    Name        VARCHAR(50) NOT NULL,
    Description VARCHAR(50) NOT NULL
);

CREATE TABLE Supplier
(
    SupplierId          INT PRIMARY KEY IDENTITY (1,1),
    Name        VARCHAR(255) NOT NULL,
    ContactInfo VARCHAR(255) NOT NULL,
    Address     VARCHAR(50)  NOT NULL
);

CREATE TABLE Product
(
    ProductId            INT PRIMARY KEY IDENTITY (1,1),
    Name          VARCHAR(255)   NOT NULL,
    Description   VARCHAR(MAX)   NOT NULL,
    Price         DECIMAL(18, 2) NOT NULL,
    StockQuantity INT            NOT NULL,
    CategoryId    INT            NOT NULL,
    SupplierId    INT            NOT NULL,
    FOREIGN KEY (CategoryId) REFERENCES Category (CategoryId) ON DELETE CASCADE,
    FOREIGN KEY (SupplierId) REFERENCES Supplier (SupplierId) ON DELETE CASCADE
);

CREATE TABLE ProductDetail
(
    ProductDetailId      INT PRIMARY KEY IDENTITY (1,1),
    weight         INT         NOT NULL,
    height         INT         NOT NULL,
    width          INT         NOT NULL,
    length         INT         NOT NULL,
    color          VARCHAR(50) NOT NULL,
    warrantyPeriod TIME        NOT NULL,
    FOREIGN KEY (ProductDetailId) REFERENCES Product (ProductId)
);

CREATE TABLE ProductTag
(
    ProductTagId   INT PRIMARY KEY IDENTITY (1,1),
    name VARCHAR(50) NOT NULL
);

CREATE TABLE ProductTagMapping
(
    productId    INT,
    productTagId INT,
    PRIMARY KEY (productId, productTagId),
    FOREIGN KEY (productId) REFERENCES Product (ProductId),
    FOREIGN KEY (productTagId) REFERENCES ProductTag (ProductTagId)
);