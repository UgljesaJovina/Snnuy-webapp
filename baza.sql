USE [master]
GO
/****** Object:  Database [MaturskiDb]    Script Date: 6/6/2024 9:39:05 PM ******/
CREATE DATABASE [MaturskiDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SnnuyWebSiteDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQL2022\MSSQL\DATA\SnnuyWebSiteDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SnnuyWebSiteDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQL2022\MSSQL\DATA\SnnuyWebSiteDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [MaturskiDb] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MaturskiDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MaturskiDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MaturskiDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MaturskiDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MaturskiDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MaturskiDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [MaturskiDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MaturskiDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MaturskiDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MaturskiDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MaturskiDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MaturskiDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MaturskiDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MaturskiDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MaturskiDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MaturskiDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MaturskiDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MaturskiDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MaturskiDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MaturskiDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MaturskiDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MaturskiDb] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [MaturskiDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MaturskiDb] SET RECOVERY FULL 
GO
ALTER DATABASE [MaturskiDb] SET  MULTI_USER 
GO
ALTER DATABASE [MaturskiDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MaturskiDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MaturskiDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MaturskiDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MaturskiDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MaturskiDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'MaturskiDb', N'ON'
GO
ALTER DATABASE [MaturskiDb] SET QUERY_STORE = ON
GO
ALTER DATABASE [MaturskiDb] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [MaturskiDb]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 6/6/2024 9:39:05 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomCards]    Script Date: 6/6/2024 9:39:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomCards](
	[Id] [uniqueidentifier] NOT NULL,
	[CardName] [nvarchar](50) NOT NULL,
	[PostingDate] [datetime2](7) NOT NULL,
	[CardDescription] [nvarchar](500) NOT NULL,
	[CardImageName] [nvarchar](max) NOT NULL,
	[Regions] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[ApprovalState] [int] NOT NULL,
	[OwnerAccountId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_CustomCards] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomCardsOTD]    Script Date: 6/6/2024 9:39:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomCardsOTD](
	[Id] [uniqueidentifier] NOT NULL,
	[CardId] [uniqueidentifier] NOT NULL,
	[SettingDate] [datetime2](7) NOT NULL,
	[CardSetterId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_CustomCardsOTD] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomCardUserAccount]    Script Date: 6/6/2024 9:39:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomCardUserAccount](
	[LikedCustomCardsId] [uniqueidentifier] NOT NULL,
	[LikedUsersId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_CustomCardUserAccount] PRIMARY KEY CLUSTERED 
(
	[LikedCustomCardsId] ASC,
	[LikedUsersId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Decks]    Script Date: 6/6/2024 9:39:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Decks](
	[Id] [uniqueidentifier] NOT NULL,
	[DeckCode] [nvarchar](max) NOT NULL,
	[DeckName] [nvarchar](50) NOT NULL,
	[PostingDate] [datetime2](7) NOT NULL,
	[OwnerAccountId] [uniqueidentifier] NULL,
	[Type] [int] NOT NULL,
 CONSTRAINT [PK_Decks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DecksOTD]    Script Date: 6/6/2024 9:39:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DecksOTD](
	[Id] [uniqueidentifier] NOT NULL,
	[DeckId] [uniqueidentifier] NOT NULL,
	[SettingDate] [datetime2](7) NOT NULL,
	[DeckSetterId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_DecksOTD] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeckUserAccount]    Script Date: 6/6/2024 9:39:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeckUserAccount](
	[LikedDecksId] [uniqueidentifier] NOT NULL,
	[LikedUsersId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_DeckUserAccount] PRIMARY KEY CLUSTERED 
(
	[LikedDecksId] ASC,
	[LikedUsersId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAccounts]    Script Date: 6/6/2024 9:39:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAccounts](
	[Id] [uniqueidentifier] NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[HashedPassword] [nvarchar](500) NOT NULL,
	[Permissions] [int] NOT NULL,
 CONSTRAINT [PK_UserAccounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_CustomCards_OwnerAccountId]    Script Date: 6/6/2024 9:39:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_CustomCards_OwnerAccountId] ON [dbo].[CustomCards]
(
	[OwnerAccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CustomCardsOTD_CardId]    Script Date: 6/6/2024 9:39:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_CustomCardsOTD_CardId] ON [dbo].[CustomCardsOTD]
(
	[CardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CustomCardsOTD_CardSetterId]    Script Date: 6/6/2024 9:39:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_CustomCardsOTD_CardSetterId] ON [dbo].[CustomCardsOTD]
(
	[CardSetterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CustomCardUserAccount_LikedUsersId]    Script Date: 6/6/2024 9:39:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_CustomCardUserAccount_LikedUsersId] ON [dbo].[CustomCardUserAccount]
(
	[LikedUsersId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Decks_OwnerAccountId]    Script Date: 6/6/2024 9:39:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_Decks_OwnerAccountId] ON [dbo].[Decks]
(
	[OwnerAccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_DecksOTD_DeckId]    Script Date: 6/6/2024 9:39:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_DecksOTD_DeckId] ON [dbo].[DecksOTD]
(
	[DeckId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_DecksOTD_DeckSetterId]    Script Date: 6/6/2024 9:39:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_DecksOTD_DeckSetterId] ON [dbo].[DecksOTD]
(
	[DeckSetterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_DeckUserAccount_LikedUsersId]    Script Date: 6/6/2024 9:39:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_DeckUserAccount_LikedUsersId] ON [dbo].[DeckUserAccount]
(
	[LikedUsersId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserAccounts_Username]    Script Date: 6/6/2024 9:39:05 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserAccounts_Username] ON [dbo].[UserAccounts]
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CustomCards]  WITH CHECK ADD  CONSTRAINT [FK_CustomCards_UserAccounts_OwnerAccountId] FOREIGN KEY([OwnerAccountId])
REFERENCES [dbo].[UserAccounts] ([Id])
GO
ALTER TABLE [dbo].[CustomCards] CHECK CONSTRAINT [FK_CustomCards_UserAccounts_OwnerAccountId]
GO
ALTER TABLE [dbo].[CustomCardsOTD]  WITH CHECK ADD  CONSTRAINT [FK_CustomCardsOTD_CustomCards_CardId] FOREIGN KEY([CardId])
REFERENCES [dbo].[CustomCards] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CustomCardsOTD] CHECK CONSTRAINT [FK_CustomCardsOTD_CustomCards_CardId]
GO
ALTER TABLE [dbo].[CustomCardsOTD]  WITH CHECK ADD  CONSTRAINT [FK_CustomCardsOTD_UserAccounts_CardSetterId] FOREIGN KEY([CardSetterId])
REFERENCES [dbo].[UserAccounts] ([Id])
GO
ALTER TABLE [dbo].[CustomCardsOTD] CHECK CONSTRAINT [FK_CustomCardsOTD_UserAccounts_CardSetterId]
GO
ALTER TABLE [dbo].[CustomCardUserAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomCardUserAccount_CustomCards_LikedCustomCardsId] FOREIGN KEY([LikedCustomCardsId])
REFERENCES [dbo].[CustomCards] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CustomCardUserAccount] CHECK CONSTRAINT [FK_CustomCardUserAccount_CustomCards_LikedCustomCardsId]
GO
ALTER TABLE [dbo].[CustomCardUserAccount]  WITH CHECK ADD  CONSTRAINT [FK_CustomCardUserAccount_UserAccounts_LikedUsersId] FOREIGN KEY([LikedUsersId])
REFERENCES [dbo].[UserAccounts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CustomCardUserAccount] CHECK CONSTRAINT [FK_CustomCardUserAccount_UserAccounts_LikedUsersId]
GO
ALTER TABLE [dbo].[Decks]  WITH CHECK ADD  CONSTRAINT [FK_Decks_UserAccounts_OwnerAccountId] FOREIGN KEY([OwnerAccountId])
REFERENCES [dbo].[UserAccounts] ([Id])
GO
ALTER TABLE [dbo].[Decks] CHECK CONSTRAINT [FK_Decks_UserAccounts_OwnerAccountId]
GO
ALTER TABLE [dbo].[DecksOTD]  WITH CHECK ADD  CONSTRAINT [FK_DecksOTD_Decks_DeckId] FOREIGN KEY([DeckId])
REFERENCES [dbo].[Decks] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DecksOTD] CHECK CONSTRAINT [FK_DecksOTD_Decks_DeckId]
GO
ALTER TABLE [dbo].[DecksOTD]  WITH CHECK ADD  CONSTRAINT [FK_DecksOTD_UserAccounts_DeckSetterId] FOREIGN KEY([DeckSetterId])
REFERENCES [dbo].[UserAccounts] ([Id])
GO
ALTER TABLE [dbo].[DecksOTD] CHECK CONSTRAINT [FK_DecksOTD_UserAccounts_DeckSetterId]
GO
ALTER TABLE [dbo].[DeckUserAccount]  WITH CHECK ADD  CONSTRAINT [FK_DeckUserAccount_Decks_LikedDecksId] FOREIGN KEY([LikedDecksId])
REFERENCES [dbo].[Decks] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DeckUserAccount] CHECK CONSTRAINT [FK_DeckUserAccount_Decks_LikedDecksId]
GO
ALTER TABLE [dbo].[DeckUserAccount]  WITH CHECK ADD  CONSTRAINT [FK_DeckUserAccount_UserAccounts_LikedUsersId] FOREIGN KEY([LikedUsersId])
REFERENCES [dbo].[UserAccounts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DeckUserAccount] CHECK CONSTRAINT [FK_DeckUserAccount_UserAccounts_LikedUsersId]
GO
USE [master]
GO
ALTER DATABASE [MaturskiDb] SET  READ_WRITE 
GO
