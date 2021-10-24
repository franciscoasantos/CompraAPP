IF DB_ID('CompraApp') IS NOT NULL
BEGIN
SET NOEXEC ON;
END

--Criação do banco de dados
USE [master]
CREATE DATABASE CompraApp;
GO

--Criação do usuário
CREATE LOGIN [compraapp] WITH PASSWORD=N'compraapp123', DEFAULT_DATABASE=[CompraApp], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
USE [CompraApp]
GO
CREATE USER [compraapp] FOR LOGIN [compraapp]
GO
USE [CompraApp]
GO
ALTER USER [compraapp] WITH DEFAULT_SCHEMA=[dbo]
GO
USE [CompraApp]
GO
ALTER ROLE [db_datareader] ADD MEMBER [compraapp]
GO
USE [CompraApp]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [compraapp]
GO
USE [CompraApp]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [compraapp]
GO

--Criação das tabelas e populando
USE [CompraApp]
CREATE TABLE users (
	id NUMERIC(9) PRIMARY KEY identity(1, 1)
	,nome VARCHAR(100)
	,cpf VARCHAR(11) UNIQUE
	,dataNascimento DATE
	,sexo CHAR(1)
	,endereco VARCHAR(100)
	)
GO

CREATE TABLE senhas (
	id NUMERIC(9) PRIMARY KEY identity(1, 1)
	,idUsuario NUMERIC(9) FOREIGN KEY REFERENCES users(id) UNIQUE
	,senha VARCHAR(255)
	)
GO

CREATE TABLE aplicativos (
	id NUMERIC(9) PRIMARY KEY identity(1, 1)
	,nome VARCHAR(100)
	,preco NUMERIC(11, 2)
	)
GO

INSERT INTO aplicativos
VALUES (
	'App 1'
	,99.90
	)
	,(
	'App 2'
	,49.90
	)
	,(
	'App 3'
	,89.90
	)
	,(
	'App 4'
	,30.99
	)
GO

CREATE TABLE pedidos (
	id NUMERIC(9) PRIMARY KEY identity(1, 1)
	,idUsuario NUMERIC(9) FOREIGN KEY REFERENCES users(id)
	,idAplicativo NUMERIC(9) FOREIGN KEY REFERENCES aplicativos(id)
	,status CHAR(1)
	)
GO

CREATE TABLE cartoes (
	id NUMERIC(9) PRIMARY KEY identity(1, 1)
	,idUsuario NUMERIC(9) FOREIGN KEY REFERENCES users(id)
	,numero VARCHAR(100)
	,vencimento VARCHAR(100)
	,codigoSeguranca VARCHAR(100)
	)
GO