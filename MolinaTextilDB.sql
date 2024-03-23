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

-- Datos del pedido del cliente
CREATE TABLE CustomerOrder(
	CustomerOrderID INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	CustomerName NVARCHAR(50) NOT NULL,
	CustomerOrderIssue NVARCHAR(50) NOT NULL,
	CustomerOrderDescription NVARCHAR(255) NOT NULL,
	TextileRecipe NVARCHAR(255) NOT NULL,
	TotalAmount MONEY NOT NULL
);
GO

CREATE TABLE Roles(
	RolID INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	RolName NVARCHAR(50),
	RolDescription NVARCHAR(255)
);

-- Datos de los empleados
CREATE TABLE Employee (
    EmployeeID INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    EmployeeName NVARCHAR(50) NOT NULL,
    EmployeeContact NVARCHAR(50) NOT NULL,
    EmployeeUsername NVARCHAR(50) NOT NULL,
    EmployeePassword NVARCHAR(50) NOT NULL,
    EmployeeRolID INT FOREIGN KEY REFERENCES Roles(RolID) NOT NULL
);
GO
-- Agregar datos a la tabla InventoryRawMaterials
INSERT INTO InventoryRawMaterials (RawMaterialName, RawMaterialDescription, SupplierContact, StockQuantity, StockQuantityUsed)
VALUES 
    ('Cotton', 'High-quality cotton material', 'supplier1@gmail.com', 1000, 0),
    ('Polyester', 'Polyester fabric for various uses', 'supplier2@gmail.com', 800, 0),
    ('Wool', 'Fine wool material for luxury products', 'supplier3@gmail.com', 500, 0);

-- Agregar datos a la tabla CustomerOrder
INSERT INTO CustomerOrder (CustomerName, CustomerOrderIssue, CustomerOrderDescription, TextileRecipe, TotalAmount)
VALUES 
    ('John Doe', 'None', 'Ordered cotton fabric for shirts', 'Cotton 100%', 200),
    ('Jane Smith', 'Delayed delivery', 'Ordered polyester fabric for dresses', 'Polyester 80%, Spandex 20%', 300),
    ('Alice Johnson', 'Defective material', 'Ordered wool fabric for sweaters', '100% Pure Wool', 400);

-- Agregar datos a la tabla Roles
INSERT INTO Roles(RolName, RolDescription)
VALUES 
    ('Administrator', 'Rol Con Mayor Privilegio'),
    ('Employee', 'Rol para Empleados');

-- Agregar datos a la tabla Employee
INSERT INTO Employee (EmployeeName, EmployeeContact, EmployeeUsername, EmployeePassword, EmployeeRolID)
VALUES 
    ('Admin User', 'admin@gmail.com', 'admin', 'adminpassword', 1),
    ('John Smith', 'john@gmail.com.com', 'john', 'johnpassword', 2),
    ('Emily Johnson', 'emily@gmail.com', 'emily', 'emilypassword', 2);