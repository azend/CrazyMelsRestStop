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
lastName nVarChar(50)
	NOT NULL,
phoneNumber nVarChar(12) NOT NULL
);


CREATE TABLE _Product
(
prodID int 
	NOT NULL 
	PRIMARY KEY 
	IDENTITY(1,1),
prodName nVarChar(100) NOT NULL,
price uFloat NOT NULL,
prodWeight uFloat NOT NULL,
inStock bit NOT NULL
);


CREATE TABLE _Order
(
orderID int 
	NOT NULL 
	PRIMARY KEY 
	IDENTITY(1,1),
custID int NOT NULL
	FOREIGN KEY REFERENCES _Customer(custID),
poNumber nvarchar(30),
orderDate date NOT NULL
);


CREATE TABLE _Cart
(
orderID int 
	NOT NULL
	FOREIGN KEY REFERENCES _Order(orderID),
prodID int 
	NOT NULL
	FOREIGN KEY REFERENCES _Product(prodID),
quantity uInt,
CONSTRAINT cartID 
	PRIMARY KEY (orderID,prodID)
);