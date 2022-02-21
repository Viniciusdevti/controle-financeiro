create database controleFinanceiro

use controleFinanceiro

CREATE TABLE [dbo].Categoria(
	[IdCategoria]  bigInt IDENTITY(1, 1) NOT NULL PRIMARY KEY CLUSTERED,
	[Nome] nvarchar(300) UNIQUE NOT NULL,
	[Ativo] bit NOT NULL,
	
)

CREATE TABLE [dbo].SubCategoria(
	[IdSubCategoria]  bigInt IDENTITY(1, 1) NOT NULL PRIMARY KEY CLUSTERED,
	[Nome] nvarchar(300) UNIQUE NOT NULL,
	[IdCategoria] bigInt NOT NULL FOREIGN KEY REFERENCES [dbo].Categoria ([IdCategoria]),
	[Ativo] bit NOT NULL,
	)

	CREATE TABLE [dbo].Lancamento(
	[IdLancamento]  bigInt IDENTITY(1, 1) NOT NULL PRIMARY KEY CLUSTERED,
	[Valor] bigint NOT NULL,
	[DATA] datetime NOT NULL,
	[IdSubCategoria] bigInt NOT NULL FOREIGN KEY REFERENCES [dbo].SubCategoria ([IdSubCategoria]),
	[Comentario] nvarchar(300) NULL,
	[Ativo] bit NOT NULL,
	)


