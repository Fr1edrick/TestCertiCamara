/****************************************************/
/*		SCRIPT PARA CREACIÓN DE BASE DE DATOS					*/
/*							PRUEBA PARA SENIOR									*/
/*		CREADO POR: FREDY L. VAQUIRO PERDOMO					*/
/*							- JULIO 13 DE 2022 -								*/
/****************************************************/
DROP DATABASE IF EXISTS LogQueries;
CREATE DATABASE LogQueries;
GO

USE LogQueries;
GO

IF NOT EXISTS ( SELECT  *
				FROM    sys.schemas
				WHERE   name = N'logQry' )
EXEC('CREATE SCHEMA [logQry]');
GO

/* SQUEMA DDBB */
CREATE TABLE logQry.HistoryQueryLog(
	Id						UNIQUEIDENTIFIER NOT NULL,
	UrlQuery			VARCHAR(500) NOT NULL,
	ResponseQuery VARCHAR(100) NOT NULL,
	RegistryDate	DATETIME,
	CONSTRAINT		PK_HistoryQueryLog PRIMARY KEY (Id)
);
GO

ALTER TABLE logQry.HistoryQueryLog
	ADD
	CONSTRAINT		DF_HistoryQueryLog_Id DEFAULT (NEWID()) FOR Id,
	CONSTRAINT		DF_HistoryQueryLog_RegistryDate DEFAULT (CURRENT_TIMESTAMP) FOR RegistryDate;
GO

CREATE TABLE logQry.Product(
	Id						UNIQUEIDENTIFIER NOT NULL,
	NameProduct		VARCHAR(50) NOT NULL,
	MountCredit		DECIMAL(12,2) NULL,
	Balance				DECIMAL(12,2) NULL,
	NumberQuotas	INT NULL,
	PriceQuota		DECIMAL(12,2) NULL,
	CONSTRAINT		PK_Product PRIMARY KEY (Id),
);
GO

ALTER TABLE logQry.Product
	ADD
	CONSTRAINT		DF_Product_Id DEFAULT(NEWID()) FOR Id,
	CONSTRAINT		DF_Product_MountCredit DEFAULT(1000000) FOR MountCredit,
	CONSTRAINT		DF_Product_NumberQuotas DEFAULT(12) FOR NumberQuotas,
	CONSTRAINT		UQ_Product_NameProduct UNIQUE(NameProduct);
GO

CREATE TABLE logQry.CardTarget(
	Id						UNIQUEIDENTIFIER NOT NULL,
	NameCard			VARCHAR(50) NOT NULL,
	CardType			VARCHAR(50) NOT NULL,
	DateExpire		DATETIME NULL,
	IdProduct			UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT		PK_CardTarget PRIMARY KEY (Id),
	CONSTRAINT		FK_CardTarget_IdProduct FOREIGN KEY (IdProduct) REFERENCES logQry.Product (Id)
);
GO

ALTER TABLE logQry.CardTarget
	ADD
	CONSTRAINT		DF_CardTarget_Id DEFAULT (NEWID()) FOR Id,
	CONSTRAINT		UQ_CardTarget_IdProduct UNIQUE (IdProduct),
	CONSTRAINT		UQ_CardTarget_NameCard UNIQUE (Id, NameCard);
GO

CREATE TABLE logQry.Client(
	Id						UNIQUEIDENTIFIER NOT NULL,
	ClientName		VARCHAR(50) NOT NULL,
	ClientEmail		VARCHAR(80) NULL,
	IdProduct			UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT		PK_Client PRIMARY KEY (Id),
	CONSTRAINT		FK_Client_IdProduct FOREIGN KEY (IdProduct) REFERENCES logQry.Product(Id),
)
GO

ALTER TABLE logQry.Client
	ADD
	CONSTRAINT		DF_Client_Id DEFAULT(NEWID()) FOR Id,
	CONSTRAINT		UQ_Client_Name UNIQUE(Id, ClientName)
GO

CREATE TABLE logQry.MovementTransaction(
	Id						UNIQUEIDENTIFIER NOT NULL,
	IdClient			UNIQUEIDENTIFIER NOT NULL,
	QuantityPayed DECIMAL(12,2) NOT NULL,
	DatePayed			DATETIME NOT NULL,
	StatusPayed		INT NOT NULL,
	CONSTRAINT		PK_MovementTransaction PRIMARY KEY (Id),
	CONSTRAINT		PK_MovementTransaction_IdClient FOREIGN KEY (IdClient) REFERENCES logQry.Client (Id),
)
GO

ALTER TABLE logQry.MovementTransaction
	ADD
	CONSTRAINT		DF_MovementTransaction_Id DEFAULT (NEWID()) FOR Id,
	CONSTRAINT		DF_MovementTransaction_DatePayed DEFAULT (CURRENT_TIMESTAMP) FOR DatePayed,
	CONSTRAINT		DF_MovementTransaction_StatusPayed DEFAULT (-1) FOR StatusPayed
GO

-- DATABASE INFO
EXEC sp_addextendedproperty 'Descripción', 'Base de datos que almacena el log de consultas externas por medio del API';
-- TABLES INFO
EXEC sp_addextendedproperty 'Descripción','Tabla que almacena el log principal','Schema','logQry','Table','HistoryQueryLog';
EXEC sp_addextendedproperty 'Descripción','Tabla que almacena los productos','Schema','logQry','Table','Product';
EXEC sp_addextendedproperty 'Descripción','Tabla que almacena información de tarjetas','Schema','logQry','Table','CardTarget';
EXEC sp_addextendedproperty 'Descripción','Tabla que almacena los datos de los clientes','Schema','logQry','Table','Client';
EXEC sp_addextendedproperty 'Descripción','Tabla que almacena las transacciones','Schema','logQry','Table','MovementTransaction';
-- COLUMNS INFO
EXEC sp_addextendedproperty 'Id','Esta columna almacena el Guid de la llave primaria.','Schema','logQry','Table','HistoryQueryLog','Column','Id';
EXEC sp_addextendedproperty 'UrlQuery','Esta columna almacena el URL consultada.','Schema','logQry','Table','HistoryQueryLog','Column','UrlQuery';
EXEC sp_addextendedproperty 'ResponseQuery','Esta columna almacena el resultado de la URL consultada.','Schema','logQry','Table','HistoryQueryLog','Column','ResponseQuery';
EXEC sp_addextendedproperty 'RegistryDate','Esta columna almacena el fecha de consulta de la URL.','Schema','logQry','Table','HistoryQueryLog','Column','RegistryDate';

EXEC sp_addextendedproperty 'Id','Esta columna almacena el Id del producto.','Schema','logQry','Table','Product','Column','Id';
EXEC sp_addextendedproperty 'NameProduct','Esta columna almacena el nombre del producto. valor único en la tabla','Schema','logQry','Table','Product','Column','NameProduct';
EXEC sp_addextendedproperty 'MountCredit','Esta columna almacena el Monto de crédito del producto','Schema','logQry','Table','Product','Column','MountCredit';
EXEC sp_addextendedproperty 'Balance','Esta columna almacena el saldo de crédito del producto','Schema','logQry','Table','Product','Column','Balance';
EXEC sp_addextendedproperty 'NumberQuotas','Esta columna almacena el numero de cuota de la tarjeta.','Schema','logQry','Table','Product','Column','NumberQuotas';
EXEC sp_addextendedproperty 'PriceQuota','Esta columna almacena el precio de la cuota del producto.','Schema','logQry','Table','Product','Column','PriceQuota';

EXEC sp_addextendedproperty 'Id','Esta columna almacena el Id de la tarjeta.','Schema','logQry','Table','CardTarget','Column','Id';
EXEC sp_addextendedproperty 'NameCard','Esta columna almacena el nombre de la tarjeta.','Schema','logQry','Table','CardTarget','Column','NameCard';
EXEC sp_addextendedproperty 'CardType','Esta columna almacena el tipo de tarjeta.','Schema','logQry','Table','CardTarget','Column','CardType';
EXEC sp_addextendedproperty 'DateExpire','Esta columna almacena la fecha de expiración de la tarjeta.','Schema','logQry','Table','CardTarget','Column','DateExpire';
EXEC sp_addextendedproperty 'IdProduct','Esta columna almacena el Id del producto asociado.','Schema','logQry','Table','CardTarget','Column','IdProduct';

EXEC sp_addextendedproperty 'Id','Esta columna almacena el Id del cliente.','Schema','logQry','Table','Client','Column','Id';
EXEC sp_addextendedproperty 'ClientName','Esta columna almacena el nombre del cliente.','Schema','logQry','Table','Client','Column','ClientName';
EXEC sp_addextendedproperty 'ClientEmail','Esta columna almacena el correo electrónico del cliente.','Schema','logQry','Table','Client','Column','ClientEmail';
EXEC sp_addextendedproperty 'IdProduct','Esta columna almacena el Id del producto asociado.','Schema','logQry','Table','Client','Column','IdProduct';

EXEC sp_addextendedproperty 'Id','Esta columna almacena el Id de la transacción.','Schema','logQry','Table','MovementTransaction','Column','Id';
EXEC sp_addextendedproperty 'IdClient','Esta columna almacena el Id cliente asociado.','Schema','logQry','Table','MovementTransaction','Column','IdClient';
EXEC sp_addextendedproperty 'QuantityPayed','Esta columna almacena la cantidad pagada.','Schema','logQry','Table','MovementTransaction','Column','QuantityPayed';
EXEC sp_addextendedproperty 'DatePayed','Esta columna almacena la fecha de pago.','Schema','logQry','Table','MovementTransaction','Column','DatePayed';
EXEC sp_addextendedproperty 'StatusPayed','Esta columna almacena el estado del pago.','Schema','logQry','Table','MovementTransaction','Column','StatusPayed';
GO

INSERT INTO logQry.Product(Id, NameProduct,Balance,PriceQuota)
VALUES
('63F29B08-44C5-4D05-88F1-A2D4FB83DFE6','Tarjeta Rojita',1000000.00,83333.33),
('2E8ACB76-7CF2-47FE-8585-08BADC441502','Tarjeta chanchito',1000000.00,83333.33);
GO

INSERT INTO logQry.CardTarget(NameCard, CardType, DateExpire, IdProduct)
VALUES
('Universitaria','Crédito','01/12/2030','63F29B08-44C5-4D05-88F1-A2D4FB83DFE6'),
('Gastos','Débito','01/06/2022','2E8ACB76-7CF2-47FE-8585-08BADC441502');
GO

insert into logQry.Client(Id, ClientName, ClientEmail, IdProduct)
values
('9FE8966F-0ED8-4FD0-BE5C-4B55E7EFDC4B','Omar Cantillo','omar.cantillo@test.com','63F29B08-44C5-4D05-88F1-A2D4FB83DFE6'),
('E54BE2FF-E680-4CFE-BFC8-24C64CBD3E1F','Cecilia Restrepo','cecilia.restrepo@test.com','2E8ACB76-7CF2-47FE-8585-08BADC441502')
GO

insert into logQry.MovementTransaction(IdClient, QuantityPayed)
values
('9FE8966F-0ED8-4FD0-BE5C-4B55E7EFDC4B', 83333.33),
('E54BE2FF-E680-4CFE-BFC8-24C64CBD3E1F', 83333.33);
