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

CREATE TABLE Roles(
	rolId INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	rolName VARCHAR(50) NOT NULL,
	rolDescription VARCHAR(255) NOT NULL
)
GO

INSERT INTO Roles VALUES ('Administrador', 'Rol mas Alto')
INSERT INTO Roles VALUES ('Empleado', 'Rol secundario')
GO

CREATE TABLE LoginCredentials(
    LoginCredentialId INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    Username NVARCHAR(20) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
	RolId INT FOREIGN KEY REFERENCES Roles(rolId) NOT NULL,
    EmployeeId INT NOT NULL FOREIGN KEY REFERENCES Employees(EmployeeId)
);
GO

INSERT INTO LoginCredentials VALUES ('admin', 'admin123', 1, 1)
INSERT INTO LoginCredentials VALUES ('empleado', 'empleado123', 2, 1)
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
INSERT INTO Suppliers (SupplierName, Manager, SupplierPhone, SupplierEmail)
VALUES 
    ('Proveedor1', 'Gerente1', '123456789', 'proveedor1@example.com'),
    ('Proveedor2', 'Gerente2', '987654321', 'proveedor2@example.com');
GO

CREATE TABLE Categories(
	CategoryId INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	CategoryName VARCHAR(50) NOT NULL,
	CategoryDescription VARCHAR(255)
);
GO
INSERT INTO Categories (CategoryName, CategoryDescription)
VALUES 
    ('Categoría1', 'Descripción de la categoría 1'),
    ('Categoría2', 'Descripción de la categoría 2');
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
INSERT INTO RawMaterials (RawMaterialName, RawMaterialDescription, RawMaterialPurchasePrice, RawMaterialQuantity, CategoryId, SupplierId)
VALUES 
    ('Material1', 'Descripción del Material 1', 10.99, 100, 1, 1),
    ('Material2', 'Descripción del Material 2', 15.99, 150, 2, 2);
GO

CREATE TABLE Patterns(
	PatternId INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	PatternName NVARCHAR(50) NOT NULL,
	PatternDescription VARCHAR(255)
);
GO
INSERT INTO Patterns (PatternName, PatternDescription)
VALUES 
    ('Pattern1', 'Descripción del Pattern 1'),
    ('Pattern2', 'Descripción del Pattern 2');
GO

CREATE TABLE PatternDetails(
	PatternDetailId INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	RawMaterialQuantity DECIMAL NOT NULL,
	RawMaterialId INT NOT NULL FOREIGN KEY REFERENCES RawMaterials(RawMaterialId),
	PatternId INT FOREIGN KEY REFERENCES Patterns(PatternId)
);
GO

CREATE TABLE States(
	StateId INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	StateName VARCHAR(30) NOT NULL,
	StateDescription VARCHAR(255)
);
GO
INSERT INTO States (StateName, StateDescription)
VALUES ('Active', 'This state indicates that the item is active and available for use.');
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
INSERT INTO Customers (CustomerName, CustomerAddress, CustomerPhone)
VALUES ('Juan Pérez', 'Calle Falsa 123', '555-1234');
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
	SELECT lc.LoginCredentialId, lc.Username, lc.Password, lc.RolId, lc.EmployeeId, rol.rolName, emp.EmployeeName
	FROM LoginCredentials lc
	INNER JOIN Employees emp ON lc.EmployeeId = emp.EmployeeId
	INNER JOIN Roles rol ON lc.RolId = rol.rolId
END;
GO

CREATE OR ALTER PROCEDURE spLoginCredentials_GetCredentials
(
	@Username NVARCHAR(20),
    @Password NVARCHAR(255)
)
AS
BEGIN
	SELECT LoginCredentialId, Username, Password, RolId, EmployeeId
	FROM LoginCredentials
	WHERE Username = @Username AND Password = @Password
END;
GO

CREATE OR ALTER PROCEDURE spLoginCredentials_GetById
	@LoginCredentialId INT
AS
BEGIN
	SELECT lc.LoginCredentialId, lc.Username, lc.Password, lc.RolId, lc.EmployeeId, rol.rolName, emp.EmployeeName
	FROM LoginCredentials lc
	INNER JOIN Employees emp ON lc.EmployeeId = emp.EmployeeId
	INNER JOIN Roles rol ON lc.RolId = rol.rolId
	WHERE LoginCredentialId = @LoginCredentialId
END;
GO

CREATE OR ALTER PROCEDURE spLoginCredentials_Insert
(
    @Username NVARCHAR(20),
    @Password NVARCHAR(255),
	@RolId INT,
    @EmployeeId INT

)
AS
BEGIN
	INSERT INTO LoginCredentials
	VALUES(@Username, @Password, @RolId, @EmployeeId);
END
GO

CREATE OR ALTER PROCEDURE spLoginCredentials_Update
(
	@LoginCredentialId INT,
    @Username NVARCHAR(20),
    @Password NVARCHAR(255),
	@RolId INT,
    @EmployeeId INT
)
AS
BEGIN
    UPDATE LoginCredentials
    SET Username = @Username,
        Password = @Password,
        RolId = @RolId,
		EmployeeId = @EmployeeId
    WHERE LoginCredentialId = @LoginCredentialId;
END;
GO

CREATE OR ALTER PROCEDURE spRoles_GetAll
AS
BEGIN
	SELECT rolId, rolName, rolDescription FROM Roles
END;
GO

--Sp CustomerOrders
CREATE OR ALTER PROCEDURE spCustomerOrders_Delete
    @CustomerOrderId INT
AS
BEGIN
    DELETE FROM CustomerOrders
    WHERE CustomerOrderId = @CustomerOrderId;
END;
GO

CREATE OR ALTER PROCEDURE spCustomerOrders_GetAll
AS
BEGIN
    SELECT co.CustomerOrderId, co.CreationDate, co.DeliveryDate, co.TotalAmount, co.CustomerId, co.EmployeeId, co.StateId,
           c.CustomerName, e.EmployeeName, s.StateName
    FROM CustomerOrders co
    INNER JOIN Customers c ON co.CustomerId = c.CustomerId
    INNER JOIN Employees e ON co.EmployeeId = e.EmployeeId
    INNER JOIN States s ON co.StateId = s.StateId;
END;
GO

CREATE OR ALTER PROCEDURE spCustomerOrders_GetById
    @CustomerOrderId INT
AS
BEGIN
    SELECT co.CustomerOrderId, co.CreationDate, co.DeliveryDate, co.TotalAmount, co.CustomerId, co.EmployeeId, co.StateId,
           c.CustomerName, e.EmployeeName, s.StateName
    FROM CustomerOrders co
    INNER JOIN Customers c ON co.CustomerId = c.CustomerId
    INNER JOIN Employees e ON co.EmployeeId = e.EmployeeId
    INNER JOIN States s ON co.StateId = s.StateId
    WHERE co.CustomerOrderId = @CustomerOrderId;
END
GO

CREATE OR ALTER PROCEDURE spCustomerOrders_Insert
(
    @CreationDate DATE,
    @DeliveryDate DATE,
    @TotalAmount MONEY,
    @CustomerId INT,
    @EmployeeId INT,
    @StateId INT
)
AS
BEGIN
    INSERT INTO CustomerOrders (CreationDate, DeliveryDate, TotalAmount, CustomerId, EmployeeId, StateId)
    VALUES (@CreationDate, @DeliveryDate, @TotalAmount, @CustomerId, @EmployeeId, @StateId);
END;
GO

CREATE OR ALTER PROCEDURE spCustomerOrders_Update
(
    @CustomerOrderId INT,
    @CreationDate DATE,
    @DeliveryDate DATE,
    @TotalAmount MONEY,
    @CustomerId INT,
    @EmployeeId INT,
    @StateId INT
)
AS
BEGIN
    UPDATE CustomerOrders
    SET CreationDate = @CreationDate,
        DeliveryDate = @DeliveryDate,
        TotalAmount = @TotalAmount,
        CustomerId = @CustomerId,
        EmployeeId = @EmployeeId,
        StateId = @StateId
    WHERE CustomerOrderId = @CustomerOrderId;
END;
GO


/*
CREATE OR ALTER PROCEDURE spLEmployees_GetAll
AS
BEGIN
	SELECT BookId, Title, AuthorId, PublisherId, PublicationYear, Genre, Quantity
	FROM Employees;
END;
GO
*/

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


--SP Suppliers--

CREATE OR ALTER PROCEDURE spSupplier_GetById
(
	@SupplierId INT
)	
AS
BEGIN
	 SELECT SupplierId, SupplierName, Manager, SupplierPhone, SupplierEmail
	 FROM Suppliers
	 WHERE SupplierId = @SupplierId;
END
GO

CREATE OR ALTER PROCEDURE spSupplier_GetAll
AS
BEGIN
	 SELECT SupplierId, SupplierName, Manager, SupplierPhone, SupplierEmail
	 FROM Suppliers
END;
GO

CREATE OR ALTER PROCEDURE spSupplier_Insert
(
    @SupplierName NVARCHAR(50),
    @Manager VARCHAR(255),
    @SupplierPhone VARCHAR(30),
    @SupplierEmail NVARCHAR(100)
)
AS
BEGIN
	INSERT INTO Suppliers
	VALUES(@SupplierName, @Manager, @SupplierPhone, @SupplierEmail);
END
GO

CREATE OR ALTER PROCEDURE spSupplier_Update
(
	@SupplierId INT,
    @SupplierName NVARCHAR(50),
    @Manager VARCHAR(255),
    @SupplierPhone VARCHAR(30),
    @SupplierEmail NVARCHAR(100)
)
AS
BEGIN
    UPDATE Suppliers
    SET SupplierName = @SupplierName,
        Manager = @Manager,
        SupplierPhone = @SupplierPhone,
		SupplierEmail = @SupplierEmail
    WHERE SupplierId = @SupplierId;
END;
GO

CREATE OR ALTER PROCEDURE spSupplier_Delete
	@SupplierId INT
AS
BEGIN
	DELETE FROM Suppliers
	WHERE SupplierId = @SupplierId;
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
    @CustomerName NVARCHAR(50),
    @CustomerAddress NVARCHAR(255),
    @CustomerPhone VARCHAR(20)
)
AS
BEGIN
    INSERT INTO Customers (CustomerName, CustomerAddress, CustomerPhone)
    VALUES (@CustomerName, @CustomerAddress, @CustomerPhone);
END;
GO

CREATE OR ALTER PROCEDURE dbo.spCustomers_Update
(
    @CustomerName NVARCHAR(50),
    @CustomerAddress NVARCHAR(255),
    @CustomerPhone VARCHAR(20),
    @CustomerID INT
)
AS
BEGIN
    UPDATE Customers 
    SET CustomerName = @CustomerName,
        CustomerAddress = @CustomerAddress,
        CustomerPhone = @CustomerPhone
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
----SP States ----------------------------------
CREATE OR ALTER PROCEDURE dbo.spStates_GetAll
AS
BEGIN
    SELECT StateId, StateName, StateDescription
    FROM States;
END;
GO
CREATE OR ALTER PROCEDURE dbo.spStates_GetById
(
    @StateId INT
)
AS
BEGIN
    SELECT StateId, StateName, StateDescription
    FROM States
    WHERE StateId = @StateId;
END;
GO

CREATE OR ALTER PROCEDURE dbo.spStates_Insert
(
    @StateName VARCHAR(30),
    @StateDescription VARCHAR(255)
)
AS
BEGIN
    INSERT INTO States (StateName, StateDescription)
    VALUES (@StateName, @StateDescription);
END;
GO

CREATE OR ALTER PROCEDURE dbo.spStates_Update
(
    @StateName VARCHAR(30),
    @StateDescription VARCHAR(255),
    @StateId INT
)
AS
BEGIN
    UPDATE States 
    SET StateName = @StateName,
        StateDescription = @StateDescription
    WHERE StateId = @StateId;
END;
GO

CREATE OR ALTER PROCEDURE dbo.spStates_Delete
(
    @StateId INT
)
AS
BEGIN
    DELETE FROM States WHERE StateId = @StateId;
END;
GO

--SP Product--

CREATE OR ALTER PROCEDURE spProduct_GetById
(
	@ProductId INT
)	
AS
BEGIN
	 SELECT ProductId, ProductName, ProductSize, PatternId, StateId
	 FROM Products
	 WHERE ProductId = @ProductId;
END
GO

CREATE OR ALTER PROCEDURE spProduct_GetAll
AS
BEGIN
	 SELECT P.ProductId, P.ProductName, P.ProductSize,  Pa.PatternName, S.StateName
	 FROM Products P INNER JOIN States S ON S.StateId = P.StateId
	 INNER JOIN Patterns Pa ON Pa.PatternId = P.PatternId
	 
END;
GO

CREATE OR ALTER PROCEDURE spProduct_Insert
(
    @ProductName NVARCHAR(50),
    @ProductSize VARCHAR(10),
    @PatternId INT,
    @StateId INT
)
AS
BEGIN
	INSERT INTO Products
	VALUES(@ProductName, @ProductSize, @PatternId, @StateId);
END
GO

CREATE OR ALTER PROCEDURE spProduct_Update
(
	@ProductId INT,
    @ProductName NVARCHAR(50),
    @ProductSize VARCHAR(10),
    @PatternId INT,
    @StateId INT
)
AS
BEGIN
    UPDATE Products
    SET ProductName = @ProductName,
        ProductSize = @ProductSize,
        PatternId = @PatternId,
		StateId = @StateId
    WHERE ProductId = @ProductId;
END;
GO

CREATE OR ALTER PROCEDURE spProduct_Delete
	@ProductId INT
AS
BEGIN
	DELETE FROM Products
	WHERE ProductId = @ProductId;
END;
GO 

--SP Patrones--

CREATE OR ALTER PROCEDURE spPattern_GetById
(
	@PatternId INT
)	
AS
BEGIN
	 SELECT PatternId, PatternName, PatternDescription
	 FROM Patterns
	 WHERE PatternId = @PatternId;
END
GO

CREATE OR ALTER PROCEDURE spPattern_GetAll
AS
BEGIN
	 SELECT PatternId, PatternName, PatternDescription
	 FROM Patterns
END;
GO

CREATE OR ALTER PROCEDURE spPattern_Insert
(
    @PatternName NVARCHAR(50),
    @PatternDescription VARCHAR(255)
)
AS
BEGIN
	INSERT INTO Patterns
	VALUES(@PatternName, @PatternDescription);
END
GO

CREATE OR ALTER PROCEDURE spPattern_Update
(
	@PatternId INT,
    @PatternName NVARCHAR(50),
    @PatternDescription VARCHAR(255)
)
AS
BEGIN
    UPDATE Patterns
    SET PatternName = @PatternName,
        PatternDescription = @PatternDescription
    WHERE PatternId = @PatternId;
END;
GO

CREATE OR ALTER PROCEDURE spPattern_Delete
	@PatternId INT
AS
BEGIN
	DELETE FROM Patterns
	WHERE PatternId = @PatternId;
END;
GO 

----------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE spCustomersOrdersDetails_GetAll
AS
BEGIN
	SELECT CustomerOrderDetails.CustomerOrderDetailId, CustomerOrderDetails.UnitPrice, CustomerOrderDetails.CustomerOrderDetailQuantity, CustomerOrderDetails.CustomerOrderId, Products.ProductName
	FROM CustomerOrderDetails
	INNER JOIN Products ON CustomerOrderDetails.ProductId = Products.ProductId;
END;
GO

CREATE OR ALTER PROCEDURE spCustomersOrdersDetails_GetById
(
	@CustomerOrderDetailId INT
)	
AS
BEGIN
	 SELECT CustomerOrderDetailId, UnitPrice, CustomerOrderDetailQuantity, CustomerOrderId, ProductId 
	 FROM CustomerOrderDetails
	 WHERE CustomerOrderDetailId = @CustomerOrderDetailId
END
GO

CREATE OR ALTER PROCEDURE spCustomersOrdersDetails_Insert
(
	@UnitPrice MONEY,
	@CustomerOrderDetailQuantity INT,
	@CustomerOrderId INT,
	@ProductId INT
)
AS
BEGIN
	INSERT INTO CustomerOrderDetails
	VALUES(@UnitPrice, @CustomerOrderDetailQuantity, @CustomerOrderId, @ProductId);
END
GO

CREATE OR ALTER PROCEDURE spCustomersOrdersDetails_Update
(
    @CustomerOrderDetailId INT,
	@UnitPrice MONEY,
	@CustomerOrderDetailQuantity INT,
	@CustomerOrderId INT,
	@ProductId INT
)
AS
BEGIN
    UPDATE CustomerOrderDetails
    SET UnitPrice = @UnitPrice,
        CustomerOrderDetailQuantity = @CustomerOrderDetailQuantity,
        CustomerOrderId = @CustomerOrderId,
		ProductId = @ProductId
    WHERE CustomerOrderDetailId = @CustomerOrderDetailId;
END;
GO

CREATE OR ALTER PROCEDURE spCustomersOrdersDetails_Delete
	@CustomerOrderDetailId INT
AS
BEGIN
	DELETE FROM CustomerOrderDetails
	WHERE CustomerOrderDetailId = @CustomerOrderDetailId;
END;
GO

--SP PatternDetails--
CREATE OR ALTER PROCEDURE spPatternDetails_Delete
    @PatternDetailId INT
AS
BEGIN
    DELETE FROM PatternDetails
    WHERE PatternDetailId = @PatternDetailId;
END;
GO

CREATE OR ALTER PROCEDURE spPatternDetails_GetAll
AS
BEGIN
    SELECT pd.PatternDetailId, pd.RawMaterialQuantity, pd.RawMaterialId, pd.PatternId, rm.RawMaterialName, p.PatternName
    FROM PatternDetails pd
    INNER JOIN RawMaterials rm ON pd.RawMaterialId = rm.RawMaterialId
    INNER JOIN Patterns p ON pd.PatternId = p.PatternId;
END;
GO

CREATE OR ALTER PROCEDURE spPatternDetails_GetById
    @PatternDetailId INT
AS
BEGIN
    SELECT pd.PatternDetailId, pd.RawMaterialQuantity, pd.RawMaterialId, pd.PatternId, rm.RawMaterialName, p.PatternName
    FROM PatternDetails pd
    INNER JOIN RawMaterials rm ON pd.RawMaterialId = rm.RawMaterialId
    INNER JOIN Patterns p ON pd.PatternId = p.PatternId
	WHERE pd.PatternDetailId = @PatternDetailId;
END
GO

CREATE OR ALTER PROCEDURE spPatternDetails_Insert
(
    @RawMaterialQuantity DECIMAL,
    @RawMaterialId INT,
    @PatternId INT
)
AS
BEGIN
    INSERT INTO PatternDetails (RawMaterialQuantity, RawMaterialId, PatternId)
    VALUES (@RawMaterialQuantity, @RawMaterialId, @PatternId);
END;
GO

CREATE OR ALTER PROCEDURE spPatternDetails_Update
(
    @PatternDetailId INT,
    @RawMaterialQuantity DECIMAL,
    @RawMaterialId INT,
    @PatternId INT
)
AS
BEGIN
    UPDATE PatternDetails
    SET RawMaterialQuantity = @RawMaterialQuantity,
        RawMaterialId = @RawMaterialId,
        PatternId = @PatternId
    WHERE PatternDetailId = @PatternDetailId;
END;
GO
--SP Necesarios--
CREATE OR ALTER PROCEDURE spPRawMaterials_GetAll
AS
BEGIN
    SELECT RawMaterialId, RawMaterialName, RawMaterialDescription, RawMaterialPurchasePrice, RawMaterialQuantity, CategoryId, SupplierId
    FROM RawMaterials;
END;
GO
CREATE OR ALTER PROCEDURE spPPatterns_GetAll
AS
BEGIN
    SELECT PatternId, PatternName, PatternDescription
    FROM Patterns;
END;
GO
--Fin, los sp de RawMaterials y Pattern no afectarán a los otros.

------------------------------------- STORES PROCEDURES CATEGORY ----------------------------------------------------------

CREATE OR ALTER PROCEDURE spCategory_GetAll
AS
BEGIN
	 SELECT CategoryId, CategoryName, CategoryDescription
	 FROM Categories
END;
GO

CREATE OR ALTER PROCEDURE spCategory_GetById
(
	@CategoryId INT
)	
AS
BEGIN
	 SELECT CategoryId, CategoryName, CategoryDescription
	 FROM Categories
	 WHERE CategoryId = @CategoryId;
END
GO


CREATE OR ALTER PROCEDURE spCategory_Insert
(
	@CategoryName VARCHAR(50),
	@CategoryDescription VARCHAR(255)
)
AS
BEGIN
	INSERT INTO Categories
	VALUES(@CategoryName, @CategoryDescription);
END
GO

CREATE OR ALTER PROCEDURE spCategory_Update
(
	@CategoryId INT,
	@CategoryName VARCHAR(50),
	@CategoryDescription VARCHAR(255)
)
AS
BEGIN
    UPDATE Categories
    SET CategoryName = @CategoryName,
        CategoryDescription = @CategoryDescription
    WHERE CategoryId = @CategoryId;
END;
GO

CREATE OR ALTER PROCEDURE spCategory_Delete
	@CategoryId INT
AS
BEGIN
	DELETE FROM Categories
	WHERE CategoryId = @CategoryId;
END;
GO

-------- SP Raw Materials -------------------------
CREATE OR ALTER PROCEDURE spRawMaterial_GetById
(
    @RawMaterialId INT
)
AS
BEGIN
    SELECT RawMaterialId, RawMaterialName, RawMaterialDescription, RawMaterialPurchasePrice, RawMaterialQuantity, CategoryId, SupplierId
    FROM RawMaterials
    WHERE RawMaterialId = @RawMaterialId;
END;
GO

CREATE OR ALTER PROCEDURE spRawMaterial_GetAll
AS
BEGIN
    SELECT RawMaterialId, RawMaterialName, RawMaterialDescription, RawMaterialPurchasePrice, RawMaterialQuantity, CategoryName, SupplierName
    FROM RawMaterials
	inner join Categories on Categories.CategoryId = RawMaterials.CategoryId
	inner join Suppliers on Suppliers.SupplierId = RawMaterials.SupplierId
END;
GO

CREATE OR ALTER PROCEDURE spRawMaterial_Insert
(
    @RawMaterialName VARCHAR(50),
    @RawMaterialDescription VARCHAR(255),
    @RawMaterialPurchasePrice MONEY,
    @RawMaterialQuantity DECIMAL,
    @CategoryId INT,
    @SupplierId INT
)
AS
BEGIN
    INSERT INTO RawMaterials (RawMaterialName, RawMaterialDescription, RawMaterialPurchasePrice, RawMaterialQuantity, CategoryId, SupplierId)
    VALUES (@RawMaterialName, @RawMaterialDescription, @RawMaterialPurchasePrice, @RawMaterialQuantity, @CategoryId, @SupplierId);
END;
GO

CREATE OR ALTER PROCEDURE spRawMaterial_Update
(
    @RawMaterialId INT,
    @RawMaterialName VARCHAR(50),
    @RawMaterialDescription VARCHAR(255),
    @RawMaterialPurchasePrice MONEY,
    @RawMaterialQuantity DECIMAL,
    @CategoryId INT,
    @SupplierId INT
)
AS
BEGIN
    UPDATE RawMaterials
    SET RawMaterialName = @RawMaterialName,
        RawMaterialDescription = @RawMaterialDescription,
        RawMaterialPurchasePrice = @RawMaterialPurchasePrice,
        RawMaterialQuantity = @RawMaterialQuantity,
        CategoryId = @CategoryId,
        SupplierId = @SupplierId
    WHERE RawMaterialId = @RawMaterialId;
END;
GO

CREATE OR ALTER PROCEDURE spRawMaterial_Delete
(
    @RawMaterialId INT
)
AS
BEGIN
    DELETE FROM RawMaterials
    WHERE RawMaterialId = @RawMaterialId;
END;
GO