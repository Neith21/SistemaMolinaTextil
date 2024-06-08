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

CREATE TABLE Roles(
	rolId INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	rolName VARCHAR(50) NOT NULL,
	rolDescription VARCHAR(255) NOT NULL
)
GO

CREATE TABLE LoginCredentials(
    LoginCredentialId INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    Username NVARCHAR(20) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
	RolId INT FOREIGN KEY REFERENCES Roles(rolId) NOT NULL,
    EmployeeId INT NOT NULL FOREIGN KEY REFERENCES Employees(EmployeeId)
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
    @StateId INT,
    @NewCustomerOrderId INT OUTPUT -- Parámetro de salida para el ID
)
AS
BEGIN
    INSERT INTO CustomerOrders (CreationDate, DeliveryDate, TotalAmount, CustomerId, EmployeeId, StateId)
    VALUES (@CreationDate, @DeliveryDate, @TotalAmount, @CustomerId, @EmployeeId, @StateId);

    SET @NewCustomerOrderId = SCOPE_IDENTITY();
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

CREATE OR ALTER PROCEDURE spCustomersOrdersDetails_GetAllSpecific
	@CustomerOrderId INT
AS
BEGIN
	SELECT CustomerOrderDetails.CustomerOrderDetailId, CustomerOrderDetails.UnitPrice, CustomerOrderDetails.CustomerOrderDetailQuantity, CustomerOrderDetails.CustomerOrderId, Products.ProductName
	FROM CustomerOrderDetails
	INNER JOIN Products ON CustomerOrderDetails.ProductId = Products.ProductId
	WHERE CustomerOrderId = @CustomerOrderId;
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

-- Tabla Employees
INSERT INTO Employees (EmployeeName, EmployeeLastname, EmployeeAddress, EmployeePhone, EmployeeEmail)
VALUES 
('Juan', 'Pérez', 'San Ildefonso', '71505007', 'juan.perez@textileria.com'),
('Ana', 'Gómez', 'San Ildefonso', '63012526', 'ana.gomez@textileria.com'),
('Luis', 'Martínez', 'San Ildefonso', '74568542', 'luis.martinez@textileria.com'),
('María', 'Rodríguez', 'San Ildefonso', '70122012', 'maria.rodriguez@textileria.com'),
('Carlos', 'López', 'Santa Barbara', '63254584', 'carlos.lopez@textileria.com'),
('Lucía', 'Hernández', 'Divisadero', '71505454', 'lucia.hernandez@textileria.com'),
('Miguel', 'García', 'Ciudad Dolores', '74589654', 'miguel.garcia@textileria.com'),
('Sofía', 'Jiménez', 'Apastepeque', '63212542', 'sofia.jimenez@textileria.com'),
('Javier', 'Ruiz', 'San Ildefonso', '65879521', 'javier.ruiz@textileria.com'),
('Elena', 'Torres', 'San Ildefonso', '75212425', 'elena.torres@textileria.com');
GO

-- Tabla Roles
INSERT INTO Roles VALUES ('Administrador', 'Rol mas Alto')
INSERT INTO Roles VALUES ('Empleado', 'Rol secundario')
GO

-- Tabla LoginCredentials
INSERT INTO LoginCredentials VALUES ('admin', 'admin123', 1, 1)
INSERT INTO LoginCredentials VALUES ('empleado', 'empleado123', 2, 1)
GO

-- Tabla Suppliers
INSERT INTO Suppliers (SupplierName, Manager, SupplierPhone, SupplierEmail)
VALUES
('Coplasa', 'Fernando Ortega', '71650339', 'sanvicente@coplasa.com.sv'),
('Deposito de telas', 'Gabriela Moreno', '23473000', 'sanvicente@depotelas.com')
GO

-- Tabla Categories
INSERT INTO Categories (CategoryName, CategoryDescription)
VALUES
('Algodón', 'Material textil suave y cómodo'),
('Poliéster', 'Material textil resistente y duradero'),
('Lana', 'Material textil cálido y suave'),
('Seda', 'Material textil suave y brillante'),
('Lino', 'Material textil fresco y ligero'),
('Nylon', 'Material textil resistente al desgaste'),
('Rayón', 'Material textil con textura suave'),
('Spandex', 'Material textil elástico y resistente'),
('Acrílico', 'Material textil similar a la lana'),
('Franela', 'Material textil suave y cálido');
GO

-- Tabla RawMaterials
INSERT INTO RawMaterials (RawMaterialName, RawMaterialDescription, RawMaterialPurchasePrice, RawMaterialQuantity, CategoryId, SupplierId)
VALUES
('Tela de Algodón', 'Tela de alta calidad', 150.00, 500, 1, 1),
('Hilo de Poliéster', 'Hilo resistente para coser', 50.00, 2000, 2, 2),
('Tela de Lana', 'Tela cálida para abrigos', 200.00, 300, 3, 1),
('Tela de Seda', 'Tela suave y brillante', 250.00, 150, 4, 2),
('Tela de Lino', 'Tela fresca y ligera', 180.00, 400, 5, 1),
('Hilo de Nylon', 'Hilo resistente al desgaste', 40.00, 2500, 6, 2),
('Tela de Rayón', 'Tela con textura suave', 160.00, 350, 7, 1),
('Tela de Spandex', 'Tela elástica y resistente', 220.00, 100, 8, 2),
('Fibra Acrílica', 'Fibra similar a la lana', 120.00, 600, 9, 1),
('Tela de Franela', 'Tela suave y cálida', 140.00, 450, 10, 2);
GO

-- Tabla Patterns
INSERT INTO Patterns (PatternName, PatternDescription)
VALUES
('Patrón de Camiseta', 'Patrón para camisetas de diferentes tallas'),
('Patrón de Pantalón', 'Patrón para pantalones de vestir'),
('Patrón de Chaqueta', 'Patrón para chaquetas de invierno'),
('Patrón de Vestido', 'Patrón para vestidos casuales'),
('Patrón de Falda', 'Patrón para faldas de varias longitudes'),
('Patrón de Uniforme', 'Patrón para uniformes escolares y empresariales'),
('Patrón de Deportivos', 'Patrón para ropa deportiva'),
('Patrón de Traje', 'Patrón para trajes formales'),
('Patrón de Blusa', 'Patrón para blusas elegantes'),
('Patrón de Suéter', 'Patrón para suéteres de invierno');
GO

-- Tabla PatternDetails
INSERT INTO PatternDetails (RawMaterialQuantity, RawMaterialId, PatternId)
VALUES
(1.5, 1, 1),
(2.0, 2, 2),
(3.0, 3, 3),
(2.5, 4, 4),
(1.8, 5, 5),
(2.2, 6, 6),
(2.5, 7, 7),
(3.5, 8, 8),
(1.7, 9, 9),
(2.8, 10, 10);
GO

-- Tabla States
INSERT INTO States (StateName, StateDescription)
VALUES
('Producción', 'Estado en que el producto se encuentra en proceso de fabricación'),
('En almacén', 'Estado en que el producto está almacenado y listo para la distribución'),
('En venta', 'Estado en que el producto está disponible para la venta al público'),
('Agotado', 'Estado en que el producto ya no está disponible en inventario'),
('Devuelto', 'Estado en que el producto ha sido devuelto por el cliente');
GO

-- Tabla Products
INSERT INTO Products (ProductName, ProductSize, PatternId, StateId)
VALUES
('Camiseta Básica', 'M', 1, 2),
('Pantalón de Vestir', 'L', 2, 2),
('Chaqueta', 'XL', 3, 2),
('Vestido Casual', 'S', 4, 2),
('Uniforme Escolar', 'L', 6, 2),
('Conjunto Deportivo', 'M', 7, 2),
('Traje Formal', 'XL', 8, 2);
GO

-- Tabla Customers
INSERT INTO Customers (CustomerName, CustomerAddress, CustomerPhone)
VALUES
('Josefa Rodriguez', 'San Ildefonso', '00000000'),
('C.E. Maria Luisa Vda de Marin', 'San Ildefonso', '00000000');
GO

-- Tabla CustomerOrders
INSERT INTO CustomerOrders (CreationDate, DeliveryDate, TotalAmount, CustomerId, EmployeeId, StateId)
VALUES
('2024-06-05', '2024-06-08', 8.00, 1, 1, 1),
('2024-06-05', '2024-07-05', 400.00, 2, 2, 1);
GO

-- Tabla CustomerOrderDetails
INSERT INTO CustomerOrderDetails (UnitPrice, CustomerOrderDetailQuantity, CustomerOrderId, ProductId)
VALUES
(8.00, 1, 1, 1),
(16.00, 25, 2, 5);
GO
