create database Store_api
use Store_api
CREATE TABLE [Products] (
  [product_id] VARCHAR(20) PRIMARY KEY,
  [category_id] INT,
  [product_name] VARCHAR(255),
  [brand_id] int,
  [price] DECIMAL(10,2),
  [productimage_id] int,
  [description] TEXT,
  [detail] TEXT
)
GO

CREATE TABLE [Product_Size_Quantity] (
  [product_size_quantity_id] INT PRIMARY KEY IDENTITY(1,1),
	[product_id] VARCHAR(20),
  [size] VARCHAR(10),
  [quantity] INT
);


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

CREATE TABLE [Product_Review] (
  [product_review_id] INT PRIMARY KEY,
  [product_id] VARCHAR(20),
  [user_id] VARCHAR(20),
  [rating] INT,
  [comment] TEXT,
	[review_date] DATE
);


CREATE TABLE [Shopping_Cart] (
  [cart_id] varchar(20) PRIMARY KEY,
  [user_id] varchar(20),
  [product_id] VARCHAR(20),
  [product_size_quantity_id] INT,
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
  [product_size_quantity_id] INT,
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

ALTER TABLE [Product_Review] ADD FOREIGN KEY ([product_id]) REFERENCES [Products] ([product_id])
GO
ALTER TABLE [Product_Review] ADD FOREIGN KEY ([user_id]) REFERENCES [User] ([user_id])
GO

ALTER TABLE [Product_Size_Quantity] ADD FOREIGN KEY ([product_id]) REFERENCES [Products] ([product_id])
GO


ALTER TABLE [Shopping_Cart] ADD FOREIGN KEY ([product_size_quantity_id]) REFERENCES [Product_Size_Quantity]  ([product_size_quantity_id])
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
ALTER TABLE [Order_Details] ADD FOREIGN KEY ([product_size_quantity_id]) REFERENCES [Product_Size_Quantity]  ([product_size_quantity_id])
GO



-- Thêm d? li?u vào b?ng Role (n?u có)
INSERT INTO Role (role_id, role_name)
VALUES ('R1', 'Customer'),
       ('R2', 'Admin');

INSERT INTO oderStatusCheck (oderstatus_id, [status])
VALUES (1, 'Pending'),
       (2, 'Processing'),
       (3, 'Shipped'),
       (4, 'Delivered');


-- Thêm d? li?u vào b?ng Brands
INSERT INTO Brands (brand_id, brand_name)
VALUES (1, 'Nike'),
       (2, 'Adidas'),
       (3, 'Puma'),
	   (4, 'Mizuno'),
	   (5, 'Joma'),
	   (6, 'Asics'),
	   (7, 'Kamito'),
	   (8, 'Zocker');

-- Thêm d? li?u vào b?ng Category
INSERT INTO Category (category_id, category_name)
VALUES (1, N'Giày cỏ nhân tạo'),
       (2,N'Giày Futsal'),
	   (3,N'Giày cỏ tự nhiên') ;
    
	-- Thêm d? li?u vào b?ng Product_image
INSERT INTO Product_image (productimage_id, productimage_url)
VALUES (1, 'basoc_den0.jpg'),
       (2, 'basoc_xanh0.jpg'),
       (3, 'basoc_vang0.jpg'),
/*giày nhân tạo*//*nike*/
       (4,'img/giày nhân tạo/Nike/1.jpg'),
	   (5,'img/giày nhân tạo/Nike/2.jpg'),
	   (6,'img/giày nhân tạo/Nike/3.jpg'),
	   (7,'img/giày nhân tạo/Nike/4.jpg'),
	   (8,'img/giày nhân tạo/Nike/5.jpg'),
	   (9,'img/giày nhân tạo/Nike/6.jpg'),
	  
/*giày nhân tạo*//*adidas*/
       (10,'img/giày nhân tạo/Adidas/.1jpg'),
	   (11,'img/giày nhân tạo/Adidas/.2jpg'),
	   (12,'img/giày nhân tạo/Adidas/.3jpg'),
	   (13,'img/giày nhân tạo/Adidas/.4jpg'),
	   (14,'img/giày nhân tạo/Adidas/.5jpg'),
	   (15,'img/giày nhân tạo/Adidas/.6jpg'),
	   (16,'img/giày nhân tạo/Adidas/.7jpg'),
/*giày nhân tạo*//*puma*/
       (17,'img/giày nhân tạo/Puma/1.jpg'), 
	   (18,'img/giày nhân tạo/Puma/2.jpg'),
	   (19,'img/giày nhân tạo/Puma/3.jpg'), 
	   (20,'img/giày nhân tạo/Puma/4.jpg'), 
	   (21,'img/giày nhân tạo/Puma/5.jpg'), 
       (22,'img/giày nhân tạo/Puma/6.jpg'), 
	   (23,'img/giày nhân tạo/Puma/7.jpg'), 

	   
/*giày nhân tạo*//*mizuno*/
       (24,'img/giày nhân tạo/Mizuno/1.jpg'),
	   (25,'img/giày nhân tạo/Mizuno/2.jpg'),
	   (26,'img/giày nhân tạo/Mizuno/3.jpg'),
	   (27,'img/giày nhân tạo/Mizuno/4.jpg'),
	   (28,'img/giày nhân tạo/Mizuno/5.jpg'),
	   (29,'img/giày nhân tạo/Mizuno/6.jpg'),


/*giày nhân tạo*//*kamito*/
       (30,'img/giày nhân tạo/Kamito/1.jpg'),
	   (31,'img/giày nhân tạo/Kamito/2.jpg'),
	   (32,'img/giày nhân tạo/Kamito/3.jpg'),
	   (33,'img/giày nhân tạo/Kamito/4.jpg'),
	   (34,'img/giày nhân tạo/Kamito/5.jpg'),
/*giày nhân tạo*//*kamito*/
	   (35,'img/giày nhân tạo/Zocker/1.jpg'),
	   (36,'img/giày nhân tạo/Zocker/2.jpg'),
	   (37,'img/giày nhân tạo/Zocker/3.jpg'),
	   (38,'img/giày nhân tạo/Zocker/4.jpg'),
	   (39,'img/giày nhân tạo/Zocker/5.jpg'),




/*giày futsal*//*Nike*/
	   (40,'img/giày futsal/Nike/1.jpg'),
	   (41,'img/giày futsal/Nike/2.jpg'),
	   (42,'img/giày futsal/Nike/3.jpg'),
	   (43,'img/giày futsal/Nike/4.jpg'),
	   (44,'img/giày futsal/Nike/5.jpg'),
	   (45,'img/giày futsal/Nike/6.jpg'),
	   (46,'img/giày futsal/Nike/7.jpg'),
	   

/*giày futsal*//*Adidas*/
	   (47,'img/giày futsal/Adidas/1.jpg'),
	   (48,'img/giày futsal/Adidas/2.jpg'),
	   (49,'img/giày futsal/Adidas/3.jpg'),
	   (50,'img/giày futsal/Adidas/4.jpg'),
	   (51,'img/giày futsal/Adidas/5.jpg'),
	   (52,'img/giày futsal/Adidas/6.jpg'),

/*giày futsal*//*Puma*/

/*giày futsal*//*Mizuno*/
	   (53,'img/giày futsal/Mizuno/1.jpg'),
	   (54,'img/giày futsal/Mizuno/2.jpg'),
	   (55,'img/giày futsal/Mizuno/3.jpg'),

/*giày futsal*//*Joma*/
	   (56,'img/giày futsal/Joma/.1jpg'),
	   (57,'img/giày futsal/Joma/.2jpg'),
	   (58,'img/giày futsal/Joma/.3jpg'),
	   (59,'img/giày futsal/Joma/.4jpg'),
	   (60,'img/giày futsal/Joma/.5jpg'),

/*giày futsal*//*Asics*/
	   (61,'img/giày futsal/Asics/1.jpg'),
	   (62,'img/giày futsal/Asics/2.jpg'),
	   (63,'img/giày futsal/Asics/3.jpg'),
	   (64,'img/giày futsal/Asics/4.jpg'),

/*giày futsal*//*Kamito*/
	   (65,'img/giày futsal/Kamito/1.jpg'),


/*giày futsal*//*Zocker*/

/*giày tự nhiên*//*Nike*/
	   (66,'img/giày tự nhiên/Nike/1.jpg'),
	   (67,'img/giày tự nhiên/Nike/2.jpg'),
	   (68,'img/giày tự nhiên/Nike/3.jpg'),
	   (69,'img/giày tự nhiên/Nike/4.jpg'),
	   (70,'img/giày tự nhiên/Nike/5.jpg'),
	   (71,'img/giày tự nhiên/Nike/6.jpg'),

/*giày tự nhiên*//*Adidas*/
	   (72,'img/giày tự nhiên/Adidas/1.jpg'),
	   (73,'img/giày tự nhiên/Adidas/2.jpg'),
	   (74,'img/giày tự nhiên/Adidas/3.jpg'),
	   (75,'img/giày tự nhiên/Adidas/4.jpg'),
	   (76,'img/giày tự nhiên/Adidas/5.jpg'),
	   (77,'img/giày tự nhiên/Adidas/6.jpg'),

/*giày tự nhiên*//*Puma*/
	   (78,'img/giày tự nhiên/Puma/1.jpg'),
	   (79,'img/giày tự nhiên/Puma/2.jpg'),
	   (80,'img/giày tự nhiên/Puma/3.jpg'),
	   (81,'img/giày tự nhiên/Puma/4.jpg'),
	   (82,'img/giày tự nhiên/Puma/5.jpg'),


/*giày tự nhiên*//*Mizuno*/
	   (83,'img/giày tự nhiên/Mizuno/1.jpg'),
	   (84,'img/giày tự nhiên/Mizuno/2.jpg'),
	   (85,'img/giày tự nhiên/Mizuno/3.jpg'),
	   (86,'img/giày tự nhiên/Mizuno/4.jpg'),
	   (87,'img/giày tự nhiên/Mizuno/5.jpg'),
	   (88,'img/giày tự nhiên/Mizuno/6.jpg'),

/*giày tự nhiên*//*Joma*/

/*giày tự nhiên*//*Acsic*/
	   (89,'img/giày tự nhiên/Asics/1.jpg'),
	   (90,'img/giày tự nhiên/Asics/2.jpg');
UPDATE [Product_image]
SET [productimage_url] = REPLACE([productimage_url], 'giày futsal', 'futsal')   
UPDATE [Product_image]
SET [productimage_url] = REPLACE([productimage_url], 'giày nhân tạo', 'conhantao')
UPDATE [Product_image]
SET [productimage_url] = REPLACE([productimage_url], 'giày futsal', 'futsal')
-- Thêm d? li?u vào b?ng Products
INSERT INTO Products (product_id, category_id, product_name, brand_id, price, productimage_id, [description], detail)
VALUES 
('S0001', 1, 'Giày đá bóng sân nhân tạo Adidas Predator', 1, 99.99, 1, 'Giày đá bóng sân nhân tạo Adidas Predator với công nghệ mới nhất giúp tăng cường kiểm soát bóng và linh hoạt khi chơi.', 'Chất liệu: Da tổng hợp; Size: 39-45; Màu sắc: Đen, Đỏ'),
('S0002', 1, 'Giày đá bóng sân nhân tạo Nike Mercurial', 2, 89.99, 2, 'Giày đá bóng sân nhân tạo Nike Mercurial được thiết kế để tăng tốc độ và độ bám trên mặt sân nhân tạo.', 'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Xanh dương, Cam'),
('S0003', 1, 'Giày đá bóng sân nhân tạo Puma Future', 3, 79.99, 3, 'Giày đá bóng sân nhân tạo Puma Future với công nghệ đệm tốt giúp giảm thiểu cảm giác mỏi chân khi chơi.', 'Chất liệu: Da tổng hợp; Size: 36-42; Màu sắc: Đen, Trắng'),
('S0004', 1, 'Giày đá bóng sân nhân tạo Adidas X Ghosted', 1, 109.99, 4, 'Giày đá bóng sân nhân tạo Adidas X Ghosted với thiết kế siêu nhẹ giúp tăng tốc độ và linh hoạt khi chơi.', 'Chất liệu: Da tổng hợp; Size: 37-43; Màu sắc: Đen, Cam'),
('S0005', 1, 'Giày đá bóng sân nhân tạo Nike Tiempo Legend', 2, 99.99, 5, 'Giày đá bóng sân nhân tạo Nike Tiempo Legend với lớp lót êm ái giúp tăng sự thoải mái cho đôi chân khi chơi.', 'Chất liệu: Da tổng hợp; Size: 39-45; Màu sắc: Trắng, Đen'),
('S0006', 1, 'Giày đá bóng sân nhân tạo Puma One', 3, 84.99, 6, 'Giày đá bóng sân nhân tạo Puma One với thiết kế gọn nhẹ và đệm tốt giúp cải thiện sự linh hoạt khi chơi.', 'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Xanh lá, Đỏ'),
('S0007', 1, 'Giày đá bóng sân nhân tạo Adidas Copa Sense', 1, 119.99, 7, 'Giày đá bóng sân nhân tạo Adidas Copa Sense với thiết kế dạng vớ giúp ôm sát chân và tăng cảm giác khi điều khiển bóng.', 'Chất liệu: Da tổng hợp; Size: 36-42; Màu sắc: Đen, Xám'),
('S0008', 1, 'Giày đá bóng sân nhân tạo Nike Phantom GT', 2, 109.99, 8, 'Giày đá bóng sân nhân tạo Nike Phantom GT với phần đế chống trượt giúp cải thiện độ bám trên mặt sân nhân tạo.', 'Chất liệu: Da tổng hợp; Size: 37-43; Màu sắc: Cam, Đen'),
('S0009', 1, 'Giày đá bóng sân nhân tạo Puma King Platinum', 3, 99.99, 9, 'Giày đá bóng sân nhân tạo Puma King Platinum với lớp đệm êm ái giúp giảm áp lực lên đôi chân khi chơi.', 'Chất liệu: Da tổng hợp; Size: 39-45; Màu sắc: Trắng, Đen'),
('S0010', 1, 'Giày đá bóng sân nhân tạo Adidas Nemeziz', 1, 129.99, 10, 'Giày đá bóng sân nhân tạo Adidas Nemeziz với thiết kế dạng vớ giúp ôm sát chân và tăng cảm giác khi điều khiển bóng.', 'Chất liệu: Da tổng hợp; Size: 36-42; Màu sắc: Đỏ, Đen'),
('S0011', 1, 'Giày đá bóng sân nhân tạo Nike Hypervenom', 2, 119.99, 11, 'Giày đá bóng sân nhân tạo Nike Hypervenom với thiết kế gọn nhẹ và đệm tốt giúp cải thiện sự linh hoạt khi chơi.', 'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Cam, Đen'),
('S0012', 1, 'Giày đá bóng sân nhân tạo Puma Ultra', 3, 109.99, 12, 'Giày đá bóng sân nhân tạo Puma Ultra với công nghệ mới giúp tăng cường kiểm soát bóng và linh hoạt khi chơi','Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Cam, Đen'),
('S0013', 1, 'Giày đá bóng sân nhân tạo Adidas X Speedflow', 1, 99.99, 13, 'Giày đá bóng sân nhân tạo Adidas X Speedflow với thiết kế siêu nhẹ giúp tăng tốc độ và linh hoạt khi chơi.', 'Chất liệu: Da tổng hợp; Size: 37-43; Màu sắc: Xanh dương, Đen'),
('S0014', 1, 'Giày đá bóng sân nhân tạo Nike Mercurial Superfly', 2, 129.99, 14, 'Giày đá bóng sân nhân tạo Nike Mercurial Superfly được thiết kế để tăng tốc độ và độ bám trên mặt sân nhân tạo.', 'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Đỏ, Đen'),
('S0015', 1, 'Giày đá bóng sân nhân tạo Puma Future Z', 3, 109.99, 15, 'Giày đá bóng sân nhân tạo Puma Future Z với công nghệ đệm tốt giúp giảm thiểu cảm giác mỏi chân khi chơi.', 'Chất liệu: Da tổng hợp; Size: 36-42; Màu sắc: Đen, Xanh lá'),
('S0016', 1, 'Giày đá bóng sân nhân tạo Adidas Copa Mundial', 1, 79.99, 16, 'Giày đá bóng sân nhân tạo Adidas Copa Mundial với thiết kế cổ điển và đệm êm ái giúp tăng sự thoải mái cho đôi chân khi chơi.', 'Chất liệu: Da tổng hợp; Size: 39-45; Màu sắc: Đen'),
('S0017', 1, 'Giày đá bóng sân nhân tạo Nike Phantom Vision', 2, 119.99, 17, 'Giày đá bóng sân nhân tạo Nike Phantom Vision với thiết kế đẹp mắt và đệm tốt giúp cải thiện sự linh hoạt khi chơi.', 'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Cam, Đen'),
('S0018', 1, 'Giày đá bóng sân nhân tạo Puma Future Z', 3, 109.99, 18, 'Giày đá bóng sân nhân tạo Puma Future Z với công nghệ mới giúp tăng cường kiểm soát bóng và linh hoạt khi chơi.', 'Chất liệu: Da tổng hợp; Size: 37-43; Màu sắc: Đỏ, Đen'),
('S0019', 1, 'Giày đá bóng sân nhân tạo Adidas Predator Freak', 1, 139.99, 19, 'Giày đá bóng sân nhân tạo Adidas Predator Freak với thiết kế độc đáo và đệm êm ái giúp tăng sự thoải mái cho đôi chân khi chơi.', 'Chất liệu: Da tổng hợp; Size: 36-42; Màu sắc: Xanh lá, Đen'),
('S0020', 1, 'Giày đá bóng sân nhân tạo Nike Mercurial Vapor', 2, 129.99, 20, 'Giày đá bóng sân nhân tạo Nike Mercurial Vapor với lớp lót êm ái giúp tăng sự thoải mái cho đôi chân khi chơi.', 'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Trắng, Đen'),
('S0021', 1, 'Giày đá bóng sân nhân tạo Adidas Predator', 1, 99.99, 1, 'Giày đá bóng sân nhân tạo Adidas Predator với công nghệ mới nhất giúp tăng cường kiểm soát bóng và linh hoạt khi chơi.', 'Chất liệu: Da tổng hợp; Size: 39-45; Màu sắc: Đen, Đỏ'),
('S0022', 1, 'Giày đá bóng sân nhân tạo Nike Mercurial', 2, 89.99, 2, 'Giày đá bóng sân nhân tạo Nike Mercurial được thiết kế để tăng tốc độ và độ bám trên mặt sân nhân tạo.', 'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Xanh dương, Cam'),
('S0023', 1, 'Giày đá bóng sân nhân tạo Puma Future', 3, 79.99, 3, 'Giày đá bóng sân nhân tạo Puma Future với công nghệ đệm tốt giúp giảm thiểu cảm giác mỏi chân khi chơi.', 'Chất liệu: Da tổng hợp; Size: 36-42; Màu sắc: Đen, Trắng'),
('S0024', 1, 'Giày đá bóng sân nhân tạo Adidas X Ghosted', 1, 109.99, 4, 'Giày đá bóng sân nhân tạo Adidas X Ghosted với thiết kế siêu nhẹ giúp tăng tốc độ và linh hoạt khi chơi.', 'Chất liệu: Da tổng hợp; Size: 37-43; Màu sắc: Đen, Cam'),
('S0025', 1, 'Giày đá bóng sân nhân tạo Nike Tiempo Legend', 2, 99.99, 5, 'Giày đá bóng sân nhân tạo Nike Tiempo Legend với lớp lót êm ái giúp tăng sự thoải mái cho đôi chân khi chơi.', 'Chất liệu: Da tổng hợp; Size: 39-45; Màu sắc: Trắng, Đen'),
('S0026', 1, 'Giày đá bóng sân nhân tạo Puma One', 3, 84.99, 6, 'Giày đá bóng sân nhân tạo Puma One với thiết kế gọn nhẹ và đệm tốt giúp cải thiện sự linh hoạt khi chơi.', 'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Xanh lá, Đỏ'),
('S0027', 1, 'Giày đá bóng sân nhân tạo Adidas Copa Sense', 1, 119.99, 7, 'Giày đá bóng sân nhân tạo Adidas Copa Sense với thiết kế dạng vớ giúp ôm sát chân và tăng cảm giác khi điều khiển bóng.', 'Chất liệu: Da tổng hợp; Size: 36-42; Màu sắc: Đen, Xám'),
('S0028', 1, 'Giày đá bóng sân nhân tạo Nike Phantom GT', 2, 109.99, 8, 'Giày đá bóng sân nhân tạo Nike Phantom GT với phần đế chống trượt giúp cải thiện độ bám trên mặt sân nhân tạo.', 'Chất liệu: Da tổng hợp; Size: 37-43; Màu sắc: Cam, Đen'),
('S0029', 1, 'Giày đá bóng sân nhân tạo Puma King Platinum', 3, 99.99, 9, 'Giày đá bóng sân nhân tạo Puma King Platinum với lớp đệm êm ái giúp giảm áp lực lên đôi chân khi chơi.', 'Chất liệu: Da tổng hợp; Size: 39-45; Màu sắc: Trắng, Đen'),
('S0030', 1, 'Giày đá bóng sân nhân tạo Adidas Nemeziz', 1, 129.99, 10, 'Giày đá bóng sân nhân tạo Adidas Nemeziz với thiết kế dạng vớ giúp ôm sát chân và tăng cảm giác khi điều khiển bóng.', 'Chất liệu: Da tổng hợp; Size: 36-42; Màu sắc: Đỏ, Đen'),
('S0031', 1, 'Giày đá bóng sân nhân tạo Nike Hypervenom', 2, 119.99, 11, 'Giày đá bóng sân nhân tạo Nike Hypervenom với thiết kế gọn nhẹ và đệm tốt giúp cải thiện sự linh hoạt khi chơi.', 'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Cam, Đen'),
('S0032', 1, 'Giày đá bóng sân nhân tạo Puma Ultra', 3, 109.99, 12, 'Giày đá bóng sân nhân tạo Puma Ultra với công nghệ mới giúp tăng cường kiểm soát bóng và linh hoạt khi chơi','Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Cam, Đen'),
('S0033', 1, 'Giày đá bóng sân nhân tạo Adidas X Speedflow', 1, 99.99, 13, 'Giày đá bóng sân nhân tạo Adidas X Speedflow với thiết kế siêu nhẹ giúp tăng tốc độ và linh hoạt khi chơi.', 'Chất liệu: Da tổng hợp; Size: 37-43; Màu sắc: Xanh dương, Đen'),
('S0034', 1, 'Giày đá bóng sân nhân tạo Nike Mercurial Superfly', 2, 129.99, 14, 'Giày đá bóng sân nhân tạo Nike Mercurial Superfly được thiết kế để tăng tốc độ và độ bám trên mặt sân nhân tạo.', 'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Đỏ, Đen'),
('S0035', 1, 'Giày đá bóng sân nhân tạo Puma Future Z', 3, 109.99, 15, 'Giày đá bóng sân nhân tạo Puma Future Z với công nghệ đệm tốt giúp giảm thiểu cảm giác mỏi chân khi chơi.', 'Chất liệu: Da tổng hợp; Size: 36-42; Màu sắc: Đen, Xanh lá'),
('S0036', 1, 'Giày đá bóng sân nhân tạo Adidas Copa Mundial', 1, 79.99, 16, 'Giày đá bóng sân nhân tạo Adidas Copa Mundial với thiết kế cổ điển và đệm êm ái giúp tăng sự thoải mái cho đôi chân khi chơi.', 'Chất liệu: Da tổng hợp; Size: 39-45; Màu sắc: Đen'),
('S0037', 1, 'Giày đá bóng sân nhân tạo Nike Phantom Vision', 2, 119.99, 17, 'Giày đá bóng sân nhân tạo Nike Phantom Vision với thiết kế đẹp mắt và đệm tốt giúp cải thiện sự linh hoạt khi chơi.', 'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Cam, Đen'),
('S0038', 1, 'Giày đá bóng sân nhân tạo Puma Future Z', 3, 109.99, 18, 'Giày đá bóng sân nhân tạo Puma Future Z với công nghệ mới giúp tăng cường kiểm soát bóng và linh hoạt khi chơi.', 'Chất liệu: Da tổng hợp; Size: 37-43; Màu sắc: Đỏ, Đen'),
('S0039', 1, 'Giày đá bóng sân nhân tạo Adidas Predator Freak', 1, 139.99, 19, 'Giày đá bóng sân nhân tạo Adidas Predator Freak với thiết kế độc đáo và đệm êm ái giúp tăng sự thoải mái cho đôi chân khi chơi.', 'Chất liệu: Da tổng hợp; Size: 36-42; Màu sắc: Xanh lá, Đen'),
('S0040', 1, 'Giày đá bóng sân nhân tạo Nike Mercurial Vapor', 2, 129.99, 20, 'Giày đá bóng sân nhân tạo Nike Mercurial Vapor với lớp lót êm ái giúp tăng sự thoải mái cho đôi chân khi chơi.', 'Chất liệu: Da tổng hợp; Size: 38-44; Màu sắc: Trắng, Đen');



CREATE TABLE #TempSizes (
    Size VARCHAR(10)
);
-- Thêm các kích thước vào bảng tạm thời
INSERT INTO #TempSizes (Size)
VALUES ('39'), ('40'), ('41'), ('42');

-- Tạo biến @Quantity để đặt số lượng sản phẩm cho mỗi kích thước
DECLARE @Quantity INT = 20;

-- Tạo câu lệnh INSERT INTO để thêm dữ liệu vào bảng Product_Size_Quantity cho mỗi sản phẩm và kích thước
INSERT INTO Product_Size_Quantity (product_id, size, quantity)
SELECT p.product_id, ts.Size, @Quantity
FROM Products p
CROSS JOIN #TempSizes ts;

-- Xóa bảng tạm thời
DROP TABLE #TempSizes;