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

INSERT INTO LoginCredentials VALUES('Juan503', 'Zopa123', 1);
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
	@LoginCredentialId INT
AS
BEGIN
	DELETE FROM LoginCredentials
	WHERE LoginCredentialId = @LoginCredentialId;
END;
GO

CREATE OR ALTER PROCEDURE spLoginCredentials_GetAll
AS
BEGIN
	SELECT lc.LoginCredentialId, lc.Username, lc.Password, e.EmployeeName
	FROM LoginCredentials lc
	INNER JOIN Employees e
	ON lc.EmployeeId = e.EmployeeId;
END;
GO

CREATE OR ALTER PROCEDURE spLoginCredentials_GetAllSpecific
	@EmployeeId INT
AS
BEGIN
	SELECT lc.LoginCredentialId, lc.Username, lc.Password, e.EmployeeName
	FROM LoginCredentials lc
	INNER JOIN Employees e
	ON lc.EmployeeId = e.EmployeeId
	WHERE lc.EmployeeId = @EmployeeId;
END;
GO

CREATE OR ALTER PROCEDURE spLoginCredentials_GetById
	@LoginCredentialId INT
AS
BEGIN
	 SELECT LoginCredentialId, Username, Password, EmployeeId
	 FROM LoginCredentials
	 WHERE LoginCredentialId = @LoginCredentialId;
END
GO

CREATE OR ALTER PROCEDURE spLoginCredentials_Insert
(
	@Username NVARCHAR(20),
    @Password NVARCHAR(255),
    @EmployeeId INT
)
AS
BEGIN
	INSERT INTO LoginCredentials
	VALUES(@Username, @Password, @EmployeeId);
END
GO

CREATE OR ALTER PROCEDURE spLoginCredentials_Update
(
    @LoginCredentialId INT,
    @Username NVARCHAR(20),
    @Password NVARCHAR(255),
    @EmployeeId INT
)
AS
BEGIN
    UPDATE LoginCredentials
    SET Username = @Username,
        Password = @Password,
		EmployeeId = @EmployeeId
    WHERE LoginCredentialId = @LoginCredentialId;
END;
GO

--SP Employees--
CREATE OR ALTER PROCEDURE spEmployee_GetById
(
	@EmployeeId INT
)	
AS
BEGIN
	 SELECT EmployeeId, EmployeeName, EmployeeLastname, EmployeeAddress, EmployeePhone, EmployeeEmail
	 FROM Employees
	 WHERE EmployeeId = @EmployeeId;
END
GO

CREATE OR ALTER PROCEDURE spEmployee_GetAll
AS
BEGIN
	 SELECT EmployeeId, EmployeeName, EmployeeLastname, EmployeeAddress, EmployeePhone, EmployeeEmail
	 FROM Employees
END;
GO

CREATE OR ALTER PROCEDURE spEmployee_Insert
(
	@EmployeeName NVARCHAR(50),
    @EmployeeLastname NVARCHAR(50),
    @EmployeeAddress VARCHAR(255),
    @EmployeePhone VARCHAR(30),
    @EmployeeEmail NVARCHAR(100)
)
AS
BEGIN
	INSERT INTO Employees
	VALUES(@EmployeeName, @EmployeeLastname, @EmployeeAddress, @EmployeePhone, @EmployeeEmail);
END
GO

CREATE OR ALTER PROCEDURE spEmployee_Update
(
	@EmployeeId INT,
    @EmployeeName NVARCHAR(50),
    @EmployeeLastname NVARCHAR(50),
    @EmployeeAddress VARCHAR(255),
    @EmployeePhone VARCHAR(30),
    @EmployeeEmail NVARCHAR(100)
)
AS
BEGIN
    UPDATE Employees
    SET EmployeeName = @EmployeeName,
        EmployeeLastname = @EmployeeLastname,
        EmployeeAddress = @EmployeeAddress,
		EmployeePhone = @EmployeePhone,
		EmployeeEmail = @EmployeeEmail
    WHERE EmployeeId = @EmployeeId;
END;
GO

CREATE OR ALTER PROCEDURE spEmployee_Delete
	@EmployeeId INT
AS
BEGIN
	DELETE FROM Employees
	WHERE EmployeeId = @EmployeeId;
END;
GO

--SP Customers--
CREATE OR ALTER PROCEDURE dbo.spCustomers_GetAll
AS
BEGIN
    SELECT CustomerID, CustomerName, CustomerAddress, CustomerPhone 
    FROM Customers;
END;
GO

CREATE OR ALTER PROCEDURE dbo.spCustomers_GetById
(
    @CustomerID INT
)
AS
BEGIN
    SELECT CustomerID, CustomerName, CustomerAddress, CustomerPhone 
    FROM Customers
    WHERE CustomerID = @CustomerID;
END;
GO

CREATE OR ALTER PROCEDURE dbo.spCustomers_Insert
(
    @CName NVARCHAR(50),
    @Address NVARCHAR(255),
    @Phone VARCHAR(20)
)
AS
BEGIN
    INSERT INTO Customers (CustomerName, CustomerAddress, CustomerPhone)
    VALUES (@CName, @Address, @Phone);
END;
GO

CREATE OR ALTER PROCEDURE dbo.spCustomers_Update
(
    @CName NVARCHAR(50),
    @Address NVARCHAR(255),
    @Phone VARCHAR(20),
    @CustomerID INT
)
AS
BEGIN
    UPDATE Customers 
    SET CustomerName = @CName,
        CustomerAddress = @Address,
        CustomerPhone = @Phone
    WHERE CustomerID = @CustomerID;
END;
GO

CREATE OR ALTER PROCEDURE dbo.spCustomers_Delete
(
    @CustomerID INT
)
AS
BEGIN
    DELETE FROM Customers WHERE CustomerID = @CustomerID;
END;
GO