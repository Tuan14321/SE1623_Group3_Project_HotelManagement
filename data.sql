USE [master]
GO
/****** Object:  Database [DataHotel]    Script Date: 11/6/2023 8:17:34 AM ******/
CREATE DATABASE [DataHotel]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DataHotel', FILENAME = N'D:\SQL\MSSQL15.MSSQLSERVER\MSSQL\DATA\DataHotel.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DataHotel_log', FILENAME = N'D:\SQL\MSSQL15.MSSQLSERVER\MSSQL\DATA\DataHotel_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [DataHotel] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DataHotel].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DataHotel] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DataHotel] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DataHotel] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DataHotel] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DataHotel] SET ARITHABORT OFF 
GO
ALTER DATABASE [DataHotel] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DataHotel] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DataHotel] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DataHotel] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DataHotel] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DataHotel] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DataHotel] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DataHotel] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DataHotel] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DataHotel] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DataHotel] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DataHotel] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DataHotel] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DataHotel] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DataHotel] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DataHotel] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DataHotel] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DataHotel] SET RECOVERY FULL 
GO
ALTER DATABASE [DataHotel] SET  MULTI_USER 
GO
ALTER DATABASE [DataHotel] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DataHotel] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DataHotel] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DataHotel] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DataHotel] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DataHotel] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'DataHotel', N'ON'
GO
ALTER DATABASE [DataHotel] SET QUERY_STORE = OFF
GO
USE [DataHotel]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 11/6/2023 8:17:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Phone] [nvarchar](10) NOT NULL,
	[Address] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Floor]    Script Date: 11/6/2023 8:17:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Floor](
	[FloorId] [int] IDENTITY(1,1) NOT NULL,
	[FloorName] [nvarchar](100) NULL,
 CONSTRAINT [PK_Floor] PRIMARY KEY CLUSTERED 
(
	[FloorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 11/6/2023 8:17:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[InvoiceId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[RoomId] [int] NOT NULL,
	[CheckInTime] [datetime] NULL,
	[CheckOutTime] [datetime] NULL,
	[TotalPrice] [float] NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[InvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 11/6/2023 8:17:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[PaymentId] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [int] NULL,
	[Amount] [float] NULL,
	[PaymentDate] [date] NULL,
	[PaymentMethod] [nvarchar](50) NULL,
 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[PaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 11/6/2023 8:17:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Room]    Script Date: 11/6/2023 8:17:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Room](
	[RoomId] [int] IDENTITY(1,1) NOT NULL,
	[RoomName] [nvarchar](50) NULL,
	[TypeId] [int] NOT NULL,
	[StatusId] [int] NULL,
	[FloorId] [int] NULL,
 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
(
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoomType]    Script Date: 11/6/2023 8:17:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomType](
	[TypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[Description] [nvarchar](max) NULL,
	[PricePerNight] [float] NULL,
	[PricePerHour] [float] NULL,
	[Capacity] [int] NULL,
 CONSTRAINT [PK_RoomType] PRIMARY KEY CLUSTERED 
(
	[TypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Service]    Script Date: 11/6/2023 8:17:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Service](
	[ServiceId] [int] IDENTITY(1,1) NOT NULL,
	[Servicename] [nvarchar](50) NOT NULL,
	[Price] [float] NOT NULL,
	[InvoiceId] [int] NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[ServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 11/6/2023 8:17:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[StatusId] [int] IDENTITY(1,1) NOT NULL,
	[StatusName] [nvarchar](100) NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[StatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 11/6/2023 8:17:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Floor] ON 

INSERT [dbo].[Floor] ([FloorId], [FloorName]) VALUES (1, N'1st floor')
INSERT [dbo].[Floor] ([FloorId], [FloorName]) VALUES (2, N'2nd floor')
INSERT [dbo].[Floor] ([FloorId], [FloorName]) VALUES (3, N'3rd floor')
INSERT [dbo].[Floor] ([FloorId], [FloorName]) VALUES (4, N'4th floor')
SET IDENTITY_INSERT [dbo].[Floor] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (1, N'Admin')
INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (2, N'Staff')
INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (3, N'Customer')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Room] ON 

INSERT [dbo].[Room] ([RoomId], [RoomName], [TypeId], [StatusId], [FloorId]) VALUES (1, N'101', 1, 1, 1)
INSERT [dbo].[Room] ([RoomId], [RoomName], [TypeId], [StatusId], [FloorId]) VALUES (2, N'102', 2, 1, 1)
INSERT [dbo].[Room] ([RoomId], [RoomName], [TypeId], [StatusId], [FloorId]) VALUES (3, N'103', 3, 1, 1)
INSERT [dbo].[Room] ([RoomId], [RoomName], [TypeId], [StatusId], [FloorId]) VALUES (4, N'104', 4, 1, 1)
INSERT [dbo].[Room] ([RoomId], [RoomName], [TypeId], [StatusId], [FloorId]) VALUES (5, N'105', 5, 1, 1)
INSERT [dbo].[Room] ([RoomId], [RoomName], [TypeId], [StatusId], [FloorId]) VALUES (6, N'201', 1, 1, 2)
INSERT [dbo].[Room] ([RoomId], [RoomName], [TypeId], [StatusId], [FloorId]) VALUES (7, N'202', 2, 1, 2)
INSERT [dbo].[Room] ([RoomId], [RoomName], [TypeId], [StatusId], [FloorId]) VALUES (8, N'203', 3, 1, 2)
INSERT [dbo].[Room] ([RoomId], [RoomName], [TypeId], [StatusId], [FloorId]) VALUES (9, N'204', 4, 1, 2)
INSERT [dbo].[Room] ([RoomId], [RoomName], [TypeId], [StatusId], [FloorId]) VALUES (10, N'205', 5, 1, 2)
INSERT [dbo].[Room] ([RoomId], [RoomName], [TypeId], [StatusId], [FloorId]) VALUES (11, N'301', 1, 1, 3)
INSERT [dbo].[Room] ([RoomId], [RoomName], [TypeId], [StatusId], [FloorId]) VALUES (12, N'302', 2, 1, 3)
INSERT [dbo].[Room] ([RoomId], [RoomName], [TypeId], [StatusId], [FloorId]) VALUES (13, N'303', 3, 1, 3)
INSERT [dbo].[Room] ([RoomId], [RoomName], [TypeId], [StatusId], [FloorId]) VALUES (14, N'304', 4, 1, 3)
INSERT [dbo].[Room] ([RoomId], [RoomName], [TypeId], [StatusId], [FloorId]) VALUES (15, N'305', 5, 1, 3)
INSERT [dbo].[Room] ([RoomId], [RoomName], [TypeId], [StatusId], [FloorId]) VALUES (16, N'401', 1, 1, 4)
INSERT [dbo].[Room] ([RoomId], [RoomName], [TypeId], [StatusId], [FloorId]) VALUES (17, N'402', 2, 1, 4)
INSERT [dbo].[Room] ([RoomId], [RoomName], [TypeId], [StatusId], [FloorId]) VALUES (18, N'403', 3, 1, 4)
INSERT [dbo].[Room] ([RoomId], [RoomName], [TypeId], [StatusId], [FloorId]) VALUES (19, N'404', 4, 1, 4)
INSERT [dbo].[Room] ([RoomId], [RoomName], [TypeId], [StatusId], [FloorId]) VALUES (20, N'405', 5, 1, 4)
SET IDENTITY_INSERT [dbo].[Room] OFF
GO
SET IDENTITY_INSERT [dbo].[RoomType] ON 

INSERT [dbo].[RoomType] ([TypeId], [Name], [Description], [PricePerNight], [PricePerHour], [Capacity]) VALUES (1, N'Single Room', N'Single Room', 200, 50, 1)
INSERT [dbo].[RoomType] ([TypeId], [Name], [Description], [PricePerNight], [PricePerHour], [Capacity]) VALUES (2, N'Double Room', N'Double Room', 300, 65, 2)
INSERT [dbo].[RoomType] ([TypeId], [Name], [Description], [PricePerNight], [PricePerHour], [Capacity]) VALUES (3, N'Quad Room', N'Quad Room', 350, 75, 4)
INSERT [dbo].[RoomType] ([TypeId], [Name], [Description], [PricePerNight], [PricePerHour], [Capacity]) VALUES (4, N'Single Vip Room', N'Single Vip Room', 1000, 200, 2)
INSERT [dbo].[RoomType] ([TypeId], [Name], [Description], [PricePerNight], [PricePerHour], [Capacity]) VALUES (5, N'Double Vip Room', N'Double Vip Room', 1700, 300, 4)
SET IDENTITY_INSERT [dbo].[RoomType] OFF
GO
SET IDENTITY_INSERT [dbo].[Status] ON 

INSERT [dbo].[Status] ([StatusId], [StatusName]) VALUES (1, N'Vacant')
INSERT [dbo].[Status] ([StatusId], [StatusName]) VALUES (2, N'Occupied')
INSERT [dbo].[Status] ([StatusId], [StatusName]) VALUES (3, N'Not CLean Yet')
INSERT [dbo].[Status] ([StatusId], [StatusName]) VALUES (4, N'Out Of Order')
SET IDENTITY_INSERT [dbo].[Status] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserId], [UserName], [Password], [RoleId]) VALUES (1, N'Admin', N'Admin', 1)
INSERT [dbo].[User] ([UserId], [UserName], [Password], [RoleId]) VALUES (2, N'Staff', N'Staff', 2)
INSERT [dbo].[User] ([UserId], [UserName], [Password], [RoleId]) VALUES (3, N'User', N'User', 3)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([CustomerId])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_Customers]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([RoomId])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_Room]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_User]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_Invoice] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoice] ([InvoiceId])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_Invoice]
GO
ALTER TABLE [dbo].[Room]  WITH CHECK ADD  CONSTRAINT [FK_Room_Floor] FOREIGN KEY([FloorId])
REFERENCES [dbo].[Floor] ([FloorId])
GO
ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [FK_Room_Floor]
GO
ALTER TABLE [dbo].[Room]  WITH CHECK ADD  CONSTRAINT [FK_Room_RoomType] FOREIGN KEY([TypeId])
REFERENCES [dbo].[RoomType] ([TypeId])
GO
ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [FK_Room_RoomType]
GO
ALTER TABLE [dbo].[Room]  WITH CHECK ADD  CONSTRAINT [FK_Room_Status] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Status] ([StatusId])
GO
ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [FK_Room_Status]
GO
ALTER TABLE [dbo].[Service]  WITH CHECK ADD  CONSTRAINT [FK_Service_Invoice] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoice] ([InvoiceId])
GO
ALTER TABLE [dbo].[Service] CHECK CONSTRAINT [FK_Service_Invoice]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
USE [master]
GO
ALTER DATABASE [DataHotel] SET  READ_WRITE 
GO
