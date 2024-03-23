CREATE DATABASE MolinaTextilDB;
GO

USE MolinaTextilDB;
GO

--Inventario de materias primas--
CREATE TABLE InventoryRawMaterials(
	RawMaterialID INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	RawMaterialName NVARCHAR(50) NOT NULL,
	RawMaterialDescription NVARCHAR(255) NOT NULL,
	SupplierContact NVARCHAR(50) NOT NULL,
	StockQuantity DECIMAL NOT NULL,
	StockQuantityUsed DECIMAL NOT NULL
);
GO

-- Datos del cliente
CREATE TABLE CustomerOrder(
	CustomerOrderID INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	CustomerName NVARCHAR(50) NOT NULL,
	TotalAmount MONEY NOT NULL
);
GO

-- Detalles de la orden del cliente
CREATE TABLE CustomerOrderDetails(
	CustomerOrderDetailsID INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	CustomerOrderID INT FOREIGN KEY REFERENCES CustomerOrder(CustomerOrderID)  NOT NULL,
	CustomerOrderIssue NVARCHAR(50) NOT NULL,
	CustomerOrderDescription NVARCHAR(255) NOT NULL,
	TextileRecipe NVARCHAR(255),
	Amount MONEY NOT NULL
);
GO

-- Datos de los empleados
CREATE TABLE Employee (
    EmployeeID INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    EmployeeName NVARCHAR(50) NOT NULL,
    EmployeeContact NVARCHAR(50) NOT NULL,
    EmployeeUsername NVARCHAR(50) NOT NULL,
    EmployeePassword NVARCHAR(50) NOT NULL,
    EmployeeRoleID INT NOT NULL
);
GO
-- Agregar datos a la tabla InventoryRawMaterials
INSERT INTO InventoryRawMaterials (RawMaterialName, RawMaterialDescription, SupplierContact, StockQuantity, StockQuantityUsed)
VALUES ('Cotton', 'High-quality cotton material', 'Supplier1@gmail.com', 1000, 0),
       ('Polyester', 'Polyester fabric for various uses', 'Supplier2@gmail.com', 800, 0),
       ('Wool', 'Fine wool material for luxury products', 'Supplier3@gmail.com', 500, 0);
GO
-- Agregar datos a la tabla CustomerOrder
INSERT INTO CustomerOrder (CustomerName, TotalAmount)
VALUES ('John Doe', 500),
       ('Jane Smith', 700),
       ('Alice Johnson', 900);
GO
-- Agregar datos a la tabla CustomerOrderDetails
INSERT INTO CustomerOrderDetails (CustomerOrderID, CustomerOrderIssue, CustomerOrderDescription, TextileRecipe, Amount)
VALUES (1, 'Delayed delivery', 'Ordered cotton fabric for shirts', 'Cotton 100%', 200),
       (2, 'Incorrect color', 'Ordered polyester fabric for dresses', 'Polyester 80%, Spandex 20%', 300),
       (3, 'Defective material', 'Ordered wool fabric for sweaters', '100% Pure Wool', 400);
GO
-- Agregar datos a la tabla Employee
INSERT INTO Employee (EmployeeName, EmployeeContact, EmployeeUsername, EmployeePassword, EmployeeRoleID)
VALUES ('Admin User', 'admin@gmail.com', 'admin', 'adminpassword', 1),
       ('John Smith', 'john@gmail.com', 'john', 'johnpassword', 2),
       ('Emily Johnson', 'emily@gmail.com', 'emily', 'emilypassword', 2);
GO
Update Employee set EmployeeName = 'John Cena', EmployeeContact = 'cena@gmail.com' where EmployeeID = 1
Go
Select * From Employee