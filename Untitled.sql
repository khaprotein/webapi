create database Store_api
use Store_api
CREATE TABLE [Products] (
  [product_id] VARCHAR(20) PRIMARY KEY,
  [category_id] INT,
  [product_name] VARCHAR(255),
  [brand_id] int,
  [price] DECIMAL(10,2),
  [stockQuantity] int,
  [productimage_id] int,
  [description] TEXT
)
GO

CREATE TABLE [Product_image] (
  [productimage_id] int PRIMARY KEY,
  [productimage_url] varchar(255)
)
GO

CREATE TABLE [Brands] (
  [brand_id] int PRIMARY KEY,
  [brand_name] VARCHAR(100)
)
GO

CREATE TABLE [Category] (
  [category_id] INT PRIMARY KEY,
  [category_name] VARCHAR(100)
)
GO

CREATE TABLE [User] (
  [user_id] varchar(20) PRIMARY KEY,
  [role_id] varchar(20),
  [name] varchar(255),
  [password] varchar(10),
  [email] varchar(255),
  [address] varchar(255),
  [phonenumber] VARCHAR(15)
)
GO

CREATE TABLE [Shopping_Cart] (
  [cart_id] varchar(20) PRIMARY KEY,
  [user_id] varchar(20),
  [product_id] VARCHAR(20),
  [quantity] INT
)
GO

CREATE TABLE [Orders] (
  [orders_id] varchar(20) PRIMARY KEY,
  [user_id] varchar(20),
  [total_amount] DECIMAL(10,2),
  [orders_date] date,
  [shipping_address] text,
  [user_phone] VARCHAR(20),
  [oderstatus_id] int
)
GO

CREATE TABLE [Order_Details] (
  [order_details_id] VARCHAR(20) PRIMARY KEY,
  [order_id] varchar(20),
  [product_id] VARCHAR(20),
  [price_oder] DECIMAL(10,2),
  [quantity] INT
)
GO

CREATE TABLE [Role] (
  [role_id] varchar(20) PRIMARY KEY,
  [role_name] varchar(255)
)
GO

CREATE TABLE [oderStatusCheck] (
  [oderstatus_id] int PRIMARY KEY,
  [status] varchar(255)
)
GO

ALTER TABLE [Products] ADD FOREIGN KEY ([category_id]) REFERENCES [Category] ([category_id])
GO

ALTER TABLE [Products] ADD FOREIGN KEY ([brand_id]) REFERENCES [Brands] ([brand_id])
GO

ALTER TABLE [Products] ADD FOREIGN KEY ([productimage_id]) REFERENCES [Product_image] ([productimage_id])
GO

ALTER TABLE [Shopping_Cart] ADD FOREIGN KEY ([product_id]) REFERENCES [Products] ([product_id])
GO

ALTER TABLE [Shopping_Cart] ADD FOREIGN KEY ([user_id]) REFERENCES [User] ([user_id])
GO

ALTER TABLE [User] ADD FOREIGN KEY ([role_id]) REFERENCES [Role] ([role_id])
GO

ALTER TABLE [Orders] ADD FOREIGN KEY ([user_id]) REFERENCES [User] ([user_id])
GO

ALTER TABLE [Orders] ADD FOREIGN KEY ([oderstatus_id]) REFERENCES [oderStatusCheck] ([oderstatus_id])
GO

ALTER TABLE [Order_Details] ADD FOREIGN KEY ([order_id]) REFERENCES [Orders] ([orders_id])
GO

ALTER TABLE [Order_Details] ADD FOREIGN KEY ([product_id]) REFERENCES [Products] ([product_id])
GO

-- Thêm d? li?u vào b?ng Brands
INSERT INTO Brands (brand_id, brand_name)
VALUES (1, 'Nike'),
       (2, 'Adidas'),
       (3, 'Puma');

-- Thêm d? li?u vào b?ng Category
INSERT INTO Category (category_id, category_name)
VALUES (1, 'Football Shoes');

-- Thêm d? li?u vào b?ng Products
INSERT INTO Products (product_id, category_id, product_name, brand_id, price, stockQuantity, productimage_id, description)
VALUES ('S1', 1, 'Nike Mercurial Superfly 8 Elite FG', 1, 250.00, 50, 1, 'High-performance football boots designed for speed and agility.'),
       ('S2', 1, 'Adidas Copa Sense.1 FG', 2, 200.00, 70, 2, 'Classic design combined with modern technology for control and comfort.'),
       ('S3', 1, 'Puma Ultra 1.2 FG/AG', 3, 180.00, 40, 3, 'Lightweight boots built for acceleration and agility.');

-- Thêm d? li?u vào b?ng Product_image
INSERT INTO Product_image (productimage_id, productimage_url)
VALUES (1, 'basoc_den0.jpg'),
       (2, 'basoc_xanh0.jpg'),
       (3, 'basoc_vang0.jpg');

-- Thêm d? li?u vào b?ng Role (n?u có)
INSERT INTO Role (role_id, role_name)
VALUES ('R1', 'Customer'),
       ('R2', 'Admin');

-- Thêm d? li?u vào b?ng User (n?u có)
INSERT INTO [User] ([user_id], role_id, [name], [password], email, [address], phonenumber)
VALUES ('U1', 'R1', 'John Doe', 'password12', 'john@example.com', '123 Main St, City, Country', '1234567890');

-- Thêm d? li?u vào b?ng Orders (n?u có)
INSERT INTO Orders (orders_id, user_id, total_amount, orders_date, shipping_address, user_phone, oderstatus_id)
VALUES ('O1', 'U1', 450.00, '2024-03-12', '123 Main St, City, Country', '1234567890', 1);

-- Thêm d? li?u vào b?ng Order_Details (n?u có)
INSERT INTO Order_Details (order_details_id, order_id, product_id, price_oder, quantity)
VALUES ('OD1', 'O1', 'S1', 250.00, 2),
       ('OD2', 'O1', 'S2', 200.00, 1);

INSERT INTO oderStatusCheck (oderstatus_id, [status])
VALUES (1, 'Pending'),
       (2, 'Processing'),
       (3, 'Shipped'),
       (4, 'Delivered');

-- Thêm d? li?u vào b?ng Shopping_Cart (n?u có)
INSERT INTO Shopping_Cart (cart_id, user_id, product_id, quantity)
VALUES ('C1', 'U1', 'S3', 1);

-- C?p nh?t s? lý?ng t?n kho trong b?ng Products (n?u c?n thi?t)
UPDATE Products
SET stockQuantity = 39
WHERE product_id = 'S3';