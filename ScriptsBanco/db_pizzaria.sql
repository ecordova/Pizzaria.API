USE [master]
GO
/****** Object:  Database [db_pizzaria]    Script Date: 14/08/2020 17:16:48 ******/
CREATE DATABASE [db_pizzaria]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'db_pizzaria', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\db_pizzaria.mdf' , SIZE = 663552KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'db_pizzaria_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\db_pizzaria_log.ldf' , SIZE = 1646592KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [db_pizzaria] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [db_pizzaria].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [db_pizzaria] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [db_pizzaria] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [db_pizzaria] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [db_pizzaria] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [db_pizzaria] SET ARITHABORT OFF 
GO
ALTER DATABASE [db_pizzaria] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [db_pizzaria] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [db_pizzaria] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [db_pizzaria] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [db_pizzaria] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [db_pizzaria] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [db_pizzaria] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [db_pizzaria] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [db_pizzaria] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [db_pizzaria] SET  DISABLE_BROKER 
GO
ALTER DATABASE [db_pizzaria] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [db_pizzaria] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [db_pizzaria] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [db_pizzaria] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [db_pizzaria] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [db_pizzaria] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [db_pizzaria] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [db_pizzaria] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [db_pizzaria] SET  MULTI_USER 
GO
ALTER DATABASE [db_pizzaria] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [db_pizzaria] SET DB_CHAINING OFF 
GO
ALTER DATABASE [db_pizzaria] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [db_pizzaria] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [db_pizzaria] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'db_pizzaria', N'ON'
GO
ALTER DATABASE [db_pizzaria] SET QUERY_STORE = OFF
GO
USE [db_pizzaria]
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 14/08/2020 17:16:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes](
	[ClienteID] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](100) NOT NULL,
	[Telefone] [varchar](20) NULL,
	[Celular] [varchar](20) NULL,
	[Cep] [varchar](10) NULL,
	[Endereco] [varchar](100) NULL,
	[Numero] [char](10) NULL,
	[Complemento] [varchar](30) NULL,
	[Bairro] [varchar](50) NULL,
	[Cidade] [varchar](100) NULL,
	[UF] [char](2) NULL,
	[Proximidade] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ClienteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pedidos]    Script Date: 14/08/2020 17:16:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedidos](
	[PedidoID] [int] IDENTITY(1,1) NOT NULL,
	[DataEmissao] [datetime] NULL,
	[ClienteID] [int] NULL,
	[NomeCliente] [varchar](100) NULL,
	[EnderecoCompleto] [varchar](200) NULL,
	[Proximidade] [varchar](50) NULL,
	[TelefoneContato] [varchar](50) NULL,
	[TotalItens] [int] NULL,
	[ValorTotal] [decimal](10, 2) NULL,
	[ValorFrete] [decimal](10, 2) NULL,
	[ValorACobrar] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[PedidoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PedidosItens]    Script Date: 14/08/2020 17:16:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PedidosItens](
	[PedidoItemID] [int] IDENTITY(1,1) NOT NULL,
	[PedidoID] [int] NOT NULL,
	[ProdutoID] [int] NOT NULL,
	[Quantidade] [decimal](3, 2) NULL,
	[ValorItem] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[PedidoItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Produtos]    Script Date: 14/08/2020 17:16:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Produtos](
	[ProdutoID] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](100) NOT NULL,
	[Valor] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProdutoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IDX_Clientes_Celular]    Script Date: 14/08/2020 17:16:48 ******/
CREATE NONCLUSTERED INDEX [IDX_Clientes_Celular] ON [dbo].[Clientes]
(
	[Celular] ASC,
	[ClienteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IDX_Clientes_Nome]    Script Date: 14/08/2020 17:16:48 ******/
CREATE NONCLUSTERED INDEX [IDX_Clientes_Nome] ON [dbo].[Clientes]
(
	[Nome] ASC,
	[ClienteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IDX_Clientes_Telefone]    Script Date: 14/08/2020 17:16:48 ******/
CREATE NONCLUSTERED INDEX [IDX_Clientes_Telefone] ON [dbo].[Clientes]
(
	[Telefone] ASC,
	[ClienteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IDX_PedidosItens_PedidoID]    Script Date: 14/08/2020 17:16:48 ******/
CREATE NONCLUSTERED INDEX [IDX_PedidosItens_PedidoID] ON [dbo].[PedidosItens]
(
	[PedidoID] ASC,
	[PedidoItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IDX_Produtos_Nome]    Script Date: 14/08/2020 17:16:48 ******/
CREATE NONCLUSTERED INDEX [IDX_Produtos_Nome] ON [dbo].[Produtos]
(
	[Nome] ASC,
	[ProdutoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Pedidos] ADD  DEFAULT (getdate()) FOR [DataEmissao]
GO
ALTER TABLE [dbo].[Pedidos] ADD  DEFAULT ((0)) FOR [TotalItens]
GO
ALTER TABLE [dbo].[Pedidos] ADD  DEFAULT ((0.00)) FOR [ValorTotal]
GO
ALTER TABLE [dbo].[Pedidos] ADD  DEFAULT ((0.00)) FOR [ValorFrete]
GO
ALTER TABLE [dbo].[Pedidos] ADD  DEFAULT ((0.00)) FOR [ValorACobrar]
GO
ALTER TABLE [dbo].[PedidosItens] ADD  DEFAULT ((0.00)) FOR [Quantidade]
GO
ALTER TABLE [dbo].[PedidosItens] ADD  DEFAULT ((0.00)) FOR [ValorItem]
GO
ALTER TABLE [dbo].[Pedidos]  WITH CHECK ADD  CONSTRAINT [FK_Pedidos_Clientes] FOREIGN KEY([ClienteID])
REFERENCES [dbo].[Clientes] ([ClienteID])
GO
ALTER TABLE [dbo].[Pedidos] CHECK CONSTRAINT [FK_Pedidos_Clientes]
GO
ALTER TABLE [dbo].[PedidosItens]  WITH CHECK ADD  CONSTRAINT [FK_PedidosItens_Pedidos] FOREIGN KEY([PedidoID])
REFERENCES [dbo].[Pedidos] ([PedidoID])
GO
ALTER TABLE [dbo].[PedidosItens] CHECK CONSTRAINT [FK_PedidosItens_Pedidos]
GO
ALTER TABLE [dbo].[PedidosItens]  WITH CHECK ADD  CONSTRAINT [FK_PedidosItens_Produtos] FOREIGN KEY([ProdutoID])
REFERENCES [dbo].[Produtos] ([ProdutoID])
GO
ALTER TABLE [dbo].[PedidosItens] CHECK CONSTRAINT [FK_PedidosItens_Produtos]
GO
USE [master]
GO
ALTER DATABASE [db_pizzaria] SET  READ_WRITE 
GO
