CREATE DATABASE MolinaTextileDB
GO

USE MolinaTextileDB
GO

CREATE TABLE Employees(
	EmployeeID INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	NameEmployee NVARCHAR(50),
	LastnameEmployee NVARCHAR(50),
	ContactEmployee NVARCHAR(50)
)
GO

CREATE TABLE CredentialsLogin(
	CredentialID INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	EmployeeID INT FOREIGN KEY REFERENCES Employees(EmployeeID),
	UsernameCredential NVARCHAR(20),
	PasswordCredential NVARCHAR(255)
)
GO
---------------------------------------------------------------------
CREATE TABLE Suppliers(
	SupplierID INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	NameSupplier NVARCHAR(50),
	ManagerSupplier NVARCHAR(50),
	ContactSupplier NVARCHAR(50)
)
GO

CREATE TABLE Categories(
	CategoryID INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	NameCategory NVARCHAR(50),
	DescriptionCategory NVARCHAR(255)
)
GO

CREATE TABLE Materials(
	MaterialID INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	NameMaterial NVARCHAR(50),
	DescriptionMaterial NVARCHAR(255),
	CategoryID INT FOREIGN KEY REFERENCES Categories(CategoryID),
	SupplierID INT FOREIGN KEY REFERENCES Suppliers(SupplierID),
	purchasePriceMaterial MONEY,
	QuantityMaterial DECIMAL
)
GO

CREATE TABLE DetailsPatterns(
	DetailPatternID INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	MaterialID INT FOREIGN KEY REFERENCES Materials(MaterialID),
	Quantity DECIMAL
)
GO

CREATE TABLE Patterns(
	PatternID INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	NamePattern NVARCHAR(50),
	DescriptionPattern NVARCHAR(255),
	DetailPatternID INT FOREIGN KEY REFERENCES DetailsPatterns(DetailPatternID)
)
GO

CREATE TABLE States(
	StateID INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	NameState NVARCHAR(50),
	DescriptionState NVARCHAR(255)
)
GO

CREATE TABLE Products(
	ProductID INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	NameProduct NVARCHAR(50),
	SizeProduct NVARCHAR(10),
	PatternID INT FOREIGN KEY REFERENCES Materials(MaterialID),
	StateID INT FOREIGN KEY REFERENCES States(StateID)
)
GO

CREATE TABLE Customers(
	CustomerID INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	NameCustomer NVARCHAR(50),
	AddressCustomer NVARCHAR(50),
	phoneCustomer NVARCHAR(20)
)
GO

CREATE TABLE CustomersOrders(
	CustomerOrderID INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	CustomerID INT FOREIGN KEY REFERENCES Customers(CustomerID),
	EmployeeID INT FOREIGN KEY REFERENCES Employees(EmployeeID),
	CreationDate NVARCHAR(50),
	DeliveryDate NVARCHAR(50),
	OrderPrice MONEY,
	StateID INT FOREIGN KEY REFERENCES States(StateID)
)

CREATE TABLE CustomersOrdersDetails(
	CustomerOrderDetailID INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	CustomerOrderID INT FOREIGN KEY REFERENCES CustomersOrders(CustomerOrderID),
	ProductID INT FOREIGN KEY REFERENCES Products(ProductID),
	UnitPrice MONEY,
	Amount INT
)
GO