USE [master]
GO
/****** Object:  Database [RJPBank]    Script Date: 16/04/2023 8:33:57 PM ******/
CREATE DATABASE [RJPBank]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RJPBank', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\RJPBank.mdf' , SIZE = 4160KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'RJPBank_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\RJPBank_log.ldf' , SIZE = 1040KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [RJPBank] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RJPBank].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RJPBank] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RJPBank] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RJPBank] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RJPBank] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RJPBank] SET ARITHABORT OFF 
GO
ALTER DATABASE [RJPBank] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RJPBank] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RJPBank] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RJPBank] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RJPBank] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RJPBank] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RJPBank] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RJPBank] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RJPBank] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RJPBank] SET  ENABLE_BROKER 
GO
ALTER DATABASE [RJPBank] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RJPBank] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RJPBank] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RJPBank] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RJPBank] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RJPBank] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [RJPBank] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RJPBank] SET RECOVERY FULL 
GO
ALTER DATABASE [RJPBank] SET  MULTI_USER 
GO
ALTER DATABASE [RJPBank] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RJPBank] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RJPBank] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RJPBank] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'RJPBank', N'ON'
GO
USE [RJPBank]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 16/04/2023 8:33:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 16/04/2023 8:33:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[Balance] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 16/04/2023 8:33:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 16/04/2023 8:33:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[TransactionType] [smallint] NOT NULL,
	[Date] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230416162102_Account', N'5.0.17')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230416170502_transaction', N'5.0.17')
GO
SET IDENTITY_INSERT [dbo].[Accounts] ON 
GO
INSERT [dbo].[Accounts] ([Id], [CustomerId], [Balance]) VALUES (1, 1, CAST(3000.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Accounts] ([Id], [CustomerId], [Balance]) VALUES (2, 2, CAST(5000.00 AS Decimal(18, 2)))
GO
SET IDENTITY_INSERT [dbo].[Accounts] OFF
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 
GO
INSERT [dbo].[Customers] ([Id], [Name], [Surname]) VALUES (1, N'Name 1', N'Surname 1')
GO
INSERT [dbo].[Customers] ([Id], [Name], [Surname]) VALUES (2, N'Name 2', N'Surname 2')
GO
INSERT [dbo].[Customers] ([Id], [Name], [Surname]) VALUES (3, N'Name 3', N'Surname 3')
GO
INSERT [dbo].[Customers] ([Id], [Name], [Surname]) VALUES (4, N'Name 4', N'Surname 4')
GO
INSERT [dbo].[Customers] ([Id], [Name], [Surname]) VALUES (5, N'Name 5', N'Surname 5')
GO
INSERT [dbo].[Customers] ([Id], [Name], [Surname]) VALUES (6, N'Name 6', N'Surname 6')
GO
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
SET IDENTITY_INSERT [dbo].[Transactions] ON 
GO
INSERT [dbo].[Transactions] ([Id], [AccountId], [Amount], [TransactionType], [Date]) VALUES (1, 1, CAST(1000.00 AS Decimal(18, 2)), 1, CAST(N'2023-04-16T17:28:10.3103174' AS DateTime2))
GO
INSERT [dbo].[Transactions] ([Id], [AccountId], [Amount], [TransactionType], [Date]) VALUES (2, 1, CAST(2000.00 AS Decimal(18, 2)), 1, CAST(N'2023-04-16T17:29:18.4286110' AS DateTime2))
GO
INSERT [dbo].[Transactions] ([Id], [AccountId], [Amount], [TransactionType], [Date]) VALUES (3, 2, CAST(5000.00 AS Decimal(18, 2)), 1, CAST(N'2023-04-16T17:30:02.1566565' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Transactions] OFF
GO
/****** Object:  Index [IX_Accounts_CustomerId]    Script Date: 16/04/2023 8:33:58 PM ******/
CREATE NONCLUSTERED INDEX [IX_Accounts_CustomerId] ON [dbo].[Accounts]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Customers_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_Customers_CustomerId]
GO
USE [master]
GO
ALTER DATABASE [RJPBank] SET  READ_WRITE 
GO
