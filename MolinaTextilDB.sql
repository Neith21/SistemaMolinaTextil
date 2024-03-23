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