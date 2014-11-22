CREATE DATABASE SOA4;
GO
USE SOA4;
GO

CREATE TYPE uInt FROM int NOT NULL;
CREATE TYPE uFloat FROM float NOT NULL;
GO
CREATE RULE uIntRule as @value >= 0;
GO
CREATE RULE uFloatRule AS @value >= 0;
GO

EXEC sp_bindrule 'uIntRule', 'uInt';
EXEC sp_bindrule 'uFloatRule', 'uFloat';
GO

CREATE TABLE _Customer
(
custID int 
	NOT NULL 
	PRIMARY KEY 
	IDENTITY(1,1),
firstName nVarChar(50),
lastName nVarChar(50),
phoneNumber nVarChar(12)
);


CREATE TABLE _Product
(
prodID int 
	NOT NULL 
	PRIMARY KEY 
	IDENTITY(1,1),
prodName nVarChar(100),
price uFloat,
prodWeight uFloat,
inStock binary NOT NULL
);


CREATE TABLE _Order
(
orderID int 
	NOT NULL 
	PRIMARY KEY 
	IDENTITY(1,1),
custID int 
	FOREIGN KEY REFERENCES _Customer(custID),
poNumber nvarchar(30),
orderDate date NOT NULL
);


CREATE TABLE _Cart
(
orderID int 
	FOREIGN KEY REFERENCES _Order(orderID),
prodID int 
	FOREIGN KEY REFERENCES _Product(prodID),
quantity uInt,
CONSTRAINT cartID 
	PRIMARY KEY (orderID,prodID)
);