-- Insert categories
INSERT INTO Category (Name, Description)
VALUES
    ('Electronics', 'Devices and gadgets'),
    ('Furniture', 'Home and office furniture'),
    ('Clothing', 'Men and women apparel'),
    ('Books', 'All kinds of books'),
    ('Sports', 'Sports equipment and apparel');

-- Insert suppliers
INSERT INTO Supplier (Name, ContactInfo, Address)
VALUES
    ('Supplier One', '123-456-7890', '1234 Elm St.'),
    ('Supplier Two', '987-654-3210', '5678 Oak St.'),
    ('Supplier Three', '555-555-5555', '910 Pine St.'),
    ('Supplier Four', '111-222-3333', '234 Maple St.'),
    ('Supplier Five', '444-444-4444', '789 Birch St.');

-- Insert products
INSERT INTO Product (Name, Description, Price, StockQuantity, CategoryId, SupplierId)
VALUES
    ('Smartphone', 'Latest model with high-end specs', 699.99, 50, 1, 1),
    ('Laptop', 'Lightweight laptop with powerful performance', 999.99, 30, 1, 2),
    ('Office Chair', 'Ergonomic chair with adjustable height', 149.99, 100, 2, 3),
    ('T-shirt', 'Comfortable cotton t-shirt', 19.99, 200, 3, 4),
    ('Basketball', 'Standard size basketball', 29.99, 150, 5, 5);

-- Insert product details
INSERT INTO ProductDetail (weight, height, width, length, color, warrantyPeriod)
VALUES
    (200, 15, 7, 1, 'Black', '1:00:00'),
    (1200, 30, 40, 2, 'Silver', '2:00:00'),
    (5000, 120, 60, 70, 'Gray', '1:00:00'),
    (200, 10, 20, 1, 'White', '0:00:00'),
    (800, 20, 20, 20, 'Orange', '0:00:00');

-- Insert product tags
INSERT INTO ProductTag (name)
VALUES
    ('New Arrival'),
    ('Discounted'),
    ('Best Seller'),
    ('Limited Edition'),
    ('Eco-Friendly');

-- Insert product-tag mappings
INSERT INTO ProductTagMapping (productId, productTagId)
VALUES
    (1, 1),
    (1, 3),
    (2, 2),
    (3, 4),
    (4, 5),
    (5, 3);
