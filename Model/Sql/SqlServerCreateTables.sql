/* 
 * SQL Server Script
 * 
 * This script can be directly executed to configure the test database from
 * PCs located at CECAFI Lab. The database and the corresponding users are 
 * already created in the sql server, so it will create the tables needed 
 * in the samples. 
 * 
 * In a local environment (for example, with the SQLServerExpress instance 
 * included in the VStudio installation) it will be necessary to create the 
 * database and the user required by the connection string. So, the following
 * steps are needed:
 *
 *     Configure within the CREATE DATABASE sql-sentence the path where 
 *     database and log files will be created  
 *
 * This script can be executed from MS Sql Server Management Studio Express,
 * but also it is possible to use a command Line syntax:
 *
 *    > sqlcmd.exe -U [user] -P [password] -I -i SqlServerCreateTables.sql
 *
 */




USE [master]
GO

/****** Drop database if already exists  ******/
IF  EXISTS (SELECT name FROM sys.databases WHERE name = 'opinador')
DROP DATABASE [opinador]
GO

USE [master]
GO

/* DataBase Creation */
                                  

CREATE DATABASE [opinador] ON  PRIMARY 
( NAME = 'opinador', FILENAME = 'C:\Database\opinador.mdf') 
LOG ON 
( NAME = 'opinador_log', FILENAME = 'C:\Database\opinador_log.ldf')
GO


/* Create LoginUser */
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = 'user')
CREATE LOGIN [user] 
WITH   PASSWORD='password', 
	   DEFAULT_DATABASE=[opinador], 
	   DEFAULT_LANGUAGE=[Español], 
	   CHECK_EXPIRATION=OFF, 
	   CHECK_POLICY=OFF
GO


/* Set user as database owner */
USE opinador
GO

SP_CHANGEDBOWNER 'user'
GO


/* [END] Local Configuration */


/* 
 * Drop tables.                                                             
 * NOTE: before dropping a table (when re-executing the script), the tables 
 * having columns acting as foreign keys of the table to be dropped must be 
 * dropped first (otherwise, the corresponding checks on those tables could 
 * not be done).                                                            
 */

USE opinador

/* Drop Table Valoracion if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Valoracion]') 
AND type in ('U')) DROP TABLE [Valoracion]
GO


/* Drop Table Etiqueta if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Etiqueta]') 
AND type in ('U')) DROP TABLE [Etiqueta]
GO

/* Drop Table Comentario if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Comentario]') 
AND type in ('U')) DROP TABLE [Comentario]
GO


/* Drop Table Favorito if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Favorito]') 
AND type in ('U')) DROP TABLE [Favorito]
GO


/* Drop Table UserProfile if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[UserProfile]') 
AND type in ('U')) DROP TABLE [UserProfile]
GO




/* UserProfile : Table Creation */

CREATE TABLE UserProfile(
	usrId bigint IDENTITY(1,1) NOT NULL,
	loginName varchar(30) NOT NULL,
	enPassword varchar(50) NOT NULL,
	firstName varchar(30) NOT NULL,
	lastName varchar(50) NOT NULL,
	email varchar(60) NOT NULL,
	language varchar(2) NOT NULL,
	country varchar(2) NOT NULL,

    CONSTRAINT [PK_UserProfile] PRIMARY KEY (usrId ASC)
) 


/* Comentario : Table Creation */
CREATE TABLE Comentario(
	comentarioId bigint IDENTITY(1,1) NOT NULL,
	texto varchar(500) NOT NULL,
	fecha datetime NOT NULL,
	usrId bigint NOT NULL,
	productoId bigint NOT NULL,

    CONSTRAINT [PK_Comentario] PRIMARY KEY (comentarioId ASC),
	CONSTRAINT [FK_ComentarioUserId] FOREIGN KEY(usrId) 
		REFERENCES UserProfile (usrId) ON DELETE CASCADE
) 


/* Favorito : Table Creation */
CREATE TABLE Favorito(
	favoritoId bigint IDENTITY(1,1) NOT NULL,
	nombre varchar(30) NOT NULL,
	fecha datetime NOT NULL,
	comentario varchar(500) NOT NULL,
	usrId bigint NOT NULL,
	productoId bigint NOT NULL,

    CONSTRAINT [PK_Favorito] PRIMARY KEY (favoritoId ASC),
	CONSTRAINT [FK_FavoritoUserId] FOREIGN KEY(usrId) 
		REFERENCES UserProfile (usrId) ON DELETE CASCADE
) 

/* Etiqueta : Table Creation */
CREATE TABLE Etiqueta(
	etiquetaId bigint IDENTITY(1,1) NOT NULL,
	tag varchar(30) NOT NULL,
	ocurrencias integer NOT NULL,

    CONSTRAINT [PK_Etiqueta] PRIMARY KEY (etiquetaId ASC)
) 

/* Cometario-Etiqueta : Table Creation */
CREATE TABLE ComentarioEtiqueta(
	comentarioId bigint NOT NULL,
	etiquetaId bigint NOT NULL,

    CONSTRAINT [PK_ComentarioEtiqueta] PRIMARY KEY (comentarioId ASC,etiquetaId),
	CONSTRAINT [FK_ComentarioEtiquetaComentarioId] FOREIGN KEY(comentarioId) 
		REFERENCES Comentario (comentarioId) ON DELETE CASCADE,
	CONSTRAINT [FK_ComentarioEtiquetaEtiquetaId] FOREIGN KEY(etiquetaId) 
		REFERENCES Etiqueta (etiquetaId) ON DELETE CASCADE
) 


/* Valoracion : Table Creation */
CREATE TABLE Valoracion(
	valoracionId bigint IDENTITY(1,1) NOT NULL,
	voto integer NOT NULL,
	comentario varchar(500),
	fecha datetime NOT NULL,
	usrId bigint NOT NULL,
	vendedorId varchar(30) NOT NULL

    CONSTRAINT [PK_Valoracion] PRIMARY KEY (valoracionId ASC),
	CONSTRAINT [FK_ValoracionUsrId] FOREIGN KEY(usrId) 
		REFERENCES UserProfile (usrId) ON DELETE CASCADE,
	CONSTRAINT validVoto CHECK (voto >= 1 AND voto <= 5)
) 




CREATE NONCLUSTERED INDEX [IX_ComentarioIndexByUsrId] 
ON Comentario (comentarioId ASC, usrId ASC)

CREATE NONCLUSTERED INDEX [IX_ComentarioIndexByProductoId] 
ON Comentario (comentarioId ASC, productoId ASC)

CREATE NONCLUSTERED INDEX [IX_FavoritoIndexByUsrId] 
ON Favorito (favoritoId ASC, usrId ASC)

CREATE NONCLUSTERED INDEX [IX_FavoritoIndexByProductoId] 
ON Favorito (favoritoId ASC, productoId ASC)

CREATE NONCLUSTERED INDEX [IX_ComentarioEtiquetaIndexByComentarioIdEtiquetaId] 
ON ComentarioEtiqueta (comentarioId ASC, etiquetaId ASC)

CREATE NONCLUSTERED INDEX [IX_ValoracionIndexByUsrId] 
ON Valoracion (valoracionId ASC, usrId ASC)

CREATE NONCLUSTERED INDEX [IX_ValoracionIndexByVendedorId] 
ON Valoracion (valoracionId ASC, vendedorId ASC)

GO