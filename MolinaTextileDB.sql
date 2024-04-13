CREATE DATABASE MolinaTextileDB;
GO

USE MolinaTextileDB;
GO

CREATE TABLE Employees(
    EmployeeId INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    EmployeeName NVARCHAR(50) NOT NULL,
    EmployeeLastname NVARCHAR(50) NOT NULL,
    EmployeeAddress VARCHAR(255) NOT NULL,
    EmployeePhone VARCHAR(30) NOT NULL,
    EmployeeEmail NVARCHAR(100) NOT NULL
);
GO

INSERT INTO Employees VALUES('Juan', 'Perez', 'Colina Abajo', '1111-1111', 'jpca12@gmail.com');
GO

CREATE TABLE LoginCredentials(
    LoginCredentialId INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    Username NVARCHAR(20) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    EmployeeId INT NOT NULL FOREIGN KEY REFERENCES Employees(EmployeeId) ON DELETE CASCADE
);
GO
---------------------------------------------------------------------
CREATE TABLE Suppliers(
	SupplierId INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	SupplierName NVARCHAR(50) NOT NULL,
	Manager NVARCHAR(50),
	SupplierPhone VARCHAR(30) NOT NULL,
	SupplierEmail NVARCHAR(100)
);
GO

CREATE TABLE Categories(
	CategoryId INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	CategoryName VARCHAR(50) NOT NULL,
	CategoryDescription VARCHAR(255)
);
GO

CREATE TABLE RawMaterials(
	RawMaterialId INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	RawMaterialName VARCHAR(50) NOT NULL,
	RawMaterialDescription VARCHAR(255),
	RawMaterialPurchasePrice MONEY,
	RawMaterialQuantity DECIMAL NOT NULL,
	CategoryId INT FOREIGN KEY REFERENCES Categories(CategoryId),
	SupplierId INT NOT NULL FOREIGN KEY REFERENCES Suppliers(SupplierId),
);
GO

CREATE TABLE Patterns(
	PatternId INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	PatternName NVARCHAR(50) NOT NULL,
	PatternDescription VARCHAR(255)
);
GO

CREATE TABLE PatternDetails(
	PatternDetailId INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	RawMaterialQuantity DECIMAL NOT NULL,
	MaterialId INT NOT NULL FOREIGN KEY REFERENCES RawMaterials(RawMaterialId),
	PatternId INT FOREIGN KEY REFERENCES Patterns(PatternId)
);
GO

CREATE TABLE States(
	StateId INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	StateName VARCHAR(30) NOT NULL,
	StateDescription VARCHAR(255)
);
GO

CREATE TABLE Products(
	ProductId INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	ProductName VARCHAR(50) NOT NULL,
	ProductSize VARCHAR(10),
	PatternId INT NOT NULL FOREIGN KEY REFERENCES Patterns(PatternId),
	StateId INT NOT NULL FOREIGN KEY REFERENCES States(StateId)
);
GO

CREATE TABLE Customers(
	CustomerId INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	CustomerName NVARCHAR(100) NOT NULL,
	CustomerAddress NVARCHAR(255),
	CustomerPhone VARCHAR(20)
);
GO

CREATE TABLE CustomerOrders(
	CustomerOrderId INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	CreationDate DATE NOT NULL,
	DeliveryDate DATE,
	TotalAmount MONEY NOT NULL,
	CustomerId INT NOT NULL FOREIGN KEY REFERENCES Customers(CustomerId),
	EmployeeId INT NOT NULL FOREIGN KEY REFERENCES Employees(EmployeeId),
	StateId INT NOT NULL FOREIGN KEY REFERENCES States(StateId)
);
GO

CREATE TABLE CustomerOrderDetails(
	CustomerOrderDetailId INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	UnitPrice MONEY NOT NULL,
	CustomerOrderDetailQuantity INT NOT NULL,
	CustomerOrderId INT NOT NULL FOREIGN KEY REFERENCES CustomerOrders(CustomerOrderId),
	ProductId INT NOT NULL FOREIGN KEY REFERENCES Products(ProductId)
);
GO

--SP Credentials--
CREATE OR ALTER PROCEDURE spLoginCredentials_Delete
	@DetailId INT
AS
BEGIN
	DELETE FROM LoanDetails
	WHERE DetailId = @DetailId;
END;
GO

CREATE OR ALTER PROCEDURE spLoginCredentials_GetAll
AS
BEGIN
	SELECT ld.DetailId, ld.Quantity, ld.LoanId, b.Title
	FROM LoanDetails ld
	INNER JOIN Books b
	ON ld.BookId = b.BookId;
END;
GO

CREATE OR ALTER PROCEDURE spLoginCredentials_GetAllSpecific
	@LoanId INT
AS
BEGIN
	SELECT ld.DetailId, ld.Quantity, ld.LoanId, b.Title
	FROM LoanDetails ld
	INNER JOIN Books b
	ON ld.BookId = b.BookId
	WHERE ld.LoanId = @LoanId;
END;
GO

CREATE OR ALTER PROCEDURE spLoginCredentials_GetById
	@DetailId INT
AS
BEGIN
	 SELECT DetailId, LoanId, BookId, Quantity
	 FROM LoanDetails
	 WHERE DetailId = @DetailId;
END
GO

CREATE OR ALTER PROCEDURE spLoginCredentials_Insert
(
	@Quantity INT,
    @LoanId INT,
    @BookId INT
)
AS
BEGIN
	INSERT INTO LoanDetails
	VALUES(@Quantity, @LoanId, @BookId);
END
GO

CREATE OR ALTER PROCEDURE spLoginCredentials_Update
(
    @DetailId INT,
    @Quantity INT,
    @LoanId INT,
    @BookId INT
)
AS
BEGIN
    UPDATE LoanDetails
    SET Quantity = @Quantity,
        LoanId = @LoanId,
        BookId = @BookId
    WHERE DetailId = @DetailId;
END;
GO

CREATE OR ALTER PROCEDURE spLEmployees_GetAll
AS
BEGIN
	SELECT BookId, Title, AuthorId, PublisherId, PublicationYear, Genre, Quantity
	FROM Employees;
END;
GO
