USE [master]
GO
/****** Object:  Database [Practico4]    Script Date: 6/12/2023 23:49:21 ******/
CREATE DATABASE [Practico4]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Practico4', FILENAME = N'/var/opt/mssql/data/Practico4.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Practico4_log', FILENAME = N'/var/opt/mssql/data/Practico4_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Practico4] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Practico4].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Practico4] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Practico4] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Practico4] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Practico4] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Practico4] SET ARITHABORT OFF 
GO
ALTER DATABASE [Practico4] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Practico4] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Practico4] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Practico4] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Practico4] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Practico4] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Practico4] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Practico4] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Practico4] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Practico4] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Practico4] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Practico4] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Practico4] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Practico4] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Practico4] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Practico4] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Practico4] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Practico4] SET RECOVERY FULL 
GO
ALTER DATABASE [Practico4] SET  MULTI_USER 
GO
ALTER DATABASE [Practico4] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Practico4] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Practico4] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Practico4] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Practico4] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Practico4] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Practico4', N'ON'
GO
ALTER DATABASE [Practico4] SET QUERY_STORE = OFF
GO
USE [Practico4]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 6/12/2023 23:49:21 ******/
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
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 6/12/2023 23:49:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 6/12/2023 23:49:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 6/12/2023 23:49:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 6/12/2023 23:49:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 6/12/2023 23:49:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 6/12/2023 23:49:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NULL,
	[LName] [nvarchar](128) NULL,
	[IsAdmin] [bit] NOT NULL,
	[Address] [nvarchar](max) NULL,
	[EmpresaId] [int] NULL,
	[EmpresasId] [int] NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 6/12/2023 23:49:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CarritoProducto]    Script Date: 6/12/2023 23:49:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarritoProducto](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Cantidad] [int] NOT NULL,
	[ProductoId] [int] NOT NULL,
	[OCId] [bigint] NOT NULL,
 CONSTRAINT [PK_CarritoProducto] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categorias]    Script Date: 6/12/2023 23:49:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categorias](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](128) NOT NULL,
	[CategoriaId] [int] NULL,
	[EmpresaId] [int] NOT NULL,
 CONSTRAINT [PK_Categorias] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empresas]    Script Date: 6/12/2023 23:49:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empresas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](128) NOT NULL,
	[RUT] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_Empresas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Facturas]    Script Date: 6/12/2023 23:49:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Facturas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TotalComisiones] [decimal](18, 0) NOT NULL,
	[FechaInicio] [datetime2](7) NOT NULL,
	[FechaFin] [datetime2](7) NOT NULL,
	[EmpresaId] [int] NOT NULL,
 CONSTRAINT [PK_Facturas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OC]    Script Date: 6/12/2023 23:49:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OC](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[MedioDePago] [nvarchar](128) NOT NULL,
	[DireccionDeEnvio] [nvarchar](256) NOT NULL,
	[FechaEstimadaEntrega] [datetime2](7) NOT NULL,
	[Total] [decimal](18, 2) NOT NULL,
	[EstadoOrden] [nvarchar](128) NOT NULL,
	[Fecha] [datetime2](7) NOT NULL,
	[ClienteId] [nvarchar](450) NOT NULL,
	[EmpresaId] [int] NULL,
	[SucursalId] [int] NULL,
 CONSTRAINT [PK_OC] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Opiniones]    Script Date: 6/12/2023 23:49:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Opiniones](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [nvarchar](128) NOT NULL,
	[Descripcion] [nvarchar](256) NOT NULL,
	[Estrellas] [int] NOT NULL,
	[ProductoId] [int] NOT NULL,
	[ClienteId] [nvarchar](450) NULL,
 CONSTRAINT [PK_Opiniones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Productos]    Script Date: 6/12/2023 23:49:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Productos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [nvarchar](128) NOT NULL,
	[Descripcion] [nvarchar](128) NOT NULL,
	[Foto] [nvarchar](128) NOT NULL,
	[Precio] [int] NOT NULL,
	[Tipo_iva] [nvarchar](128) NOT NULL,
	[Pdf] [nvarchar](128) NOT NULL,
	[EmpresaId] [int] NOT NULL,
	[CategoriaId] [int] NOT NULL,
 CONSTRAINT [PK_Productos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reclamos]    Script Date: 6/12/2023 23:49:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reclamos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](128) NOT NULL,
	[Fecha] [datetime2](7) NOT NULL,
	[EmpresaId] [int] NOT NULL,
	[OCId] [bigint] NOT NULL,
 CONSTRAINT [PK_Reclamos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sucursales]    Script Date: 6/12/2023 23:49:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sucursales](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](128) NOT NULL,
	[Ubicacion] [nvarchar](128) NOT NULL,
	[TiempoEntrega] [int] NOT NULL,
	[EmpresaId] [int] NOT NULL,
 CONSTRAINT [PK_Sucursales] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231129001256_Version_1.0', N'7.0.10')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'ADMIN', N'ADMIN', N'ADMIN', N'ADMIN')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'EMPLEADO', N'EMPLEADO', N'EMPLEADO', N'EMPLEADO')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'MANAGER', N'MANAGER', N'MANAGER', N'MANAGER')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'USER', N'USER', N'USER', N'USER')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd8f05f30-c0f6-43e4-b3d6-d3d9f6e959c7', N'EMPLEADO')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'de677495-aba5-44a0-9a78-ae17f88f0f58', N'EMPLEADO')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c981a92d-9c00-4cfd-81b4-0d05ab4a6d58', N'MANAGER')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'21238fa9-1118-4994-b19f-b5b7843fc381', N'USER')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [Password], [Name], [LName], [IsAdmin], [Address], [EmpresaId], [EmpresasId], [UserName], [NormalizedUserName], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'21238fa9-1118-4994-b19f-b5b7843fc381', N'leo@gmail.com', N'Abc*123!', N'Leonardo', N'Sellanes', 0, N'Av. Brasil 297', 1, NULL, N'leo@gmail.com', N'LEO@GMAIL.COM', NULL, 0, N'AQAAAAIAAYagAAAAEJY7hRG0W+m7dR7quHP5sHTJJaWgcTPRCyBj3gtXbpWCZd8pWRTUrUf7osSuCE0sVA==', N'UZYGB4BDUKGE5YIENNEGNCU4DYZ2OZ2U', N'd0ab0cd6-753d-4645-8eba-89a7d54a8a5a', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [Password], [Name], [LName], [IsAdmin], [Address], [EmpresaId], [EmpresasId], [UserName], [NormalizedUserName], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'c981a92d-9c00-4cfd-81b4-0d05ab4a6d58', N'juan@gmail.com', N'Abc*123!', N'juan', N'perez', 1, N'', 1, NULL, N'juan@gmail.com', N'JUAN@GMAIL.COM', NULL, 0, N'AQAAAAIAAYagAAAAEOLD/C0+1HfVhb+8fE+2lOPpjSV4luNHyMD8kIg17vxY7ZcVNJA+jQo84BA5ajMb7w==', N'UHUKXIADVUWABXGET5J6JKIQJJ6TS54P', N'941c97ac-d830-4f04-a075-c4dedd2e7dbf', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [Password], [Name], [LName], [IsAdmin], [Address], [EmpresaId], [EmpresasId], [UserName], [NormalizedUserName], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'd8f05f30-c0f6-43e4-b3d6-d3d9f6e959c7', N'mela@gmail.com', N'Abc*123!', N'Melanie', N'Diaz', 0, N'', 1, NULL, N'mela@gmail.com', N'MELA@GMAIL.COM', NULL, 0, N'AQAAAAIAAYagAAAAEPiWO2gAOOdVFEYFv9Fx4yzcuhG2hcBCE/dZQpBmlexxrjSCJoGKpLmnVcxvB2fYRg==', N'PD3CMTYM4DDMVRJVAG6D5XU2VF3U2OSQ', N'10c90ff6-a8ea-433d-b54e-9058be9578da', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [Password], [Name], [LName], [IsAdmin], [Address], [EmpresaId], [EmpresasId], [UserName], [NormalizedUserName], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'de677495-aba5-44a0-9a78-ae17f88f0f58', N'ale@gmail.com', N'Abc*123!', N'Alejandro', N'Suarez', 0, N'', 1, NULL, N'ale@gmail.com', N'ALE@GMAIL.COM', NULL, 0, N'AQAAAAIAAYagAAAAEHFKgf0cxOX3YN8mM3uszeIAwFip59VkkqJcybYD+TyOOOdRY+tZftAFXmbuIxfw+g==', N'PIYMGTLZCG4OYG2NPF64KDMQGD3HS5EK', N'2fda81f7-5274-4ed1-9f0a-11be64f503f0', NULL, 0, 0, NULL, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[CarritoProducto] ON 

INSERT [dbo].[CarritoProducto] ([Id], [Cantidad], [ProductoId], [OCId]) VALUES (1, 1, 3, 1)
INSERT [dbo].[CarritoProducto] ([Id], [Cantidad], [ProductoId], [OCId]) VALUES (2, 1, 2, 1)
SET IDENTITY_INSERT [dbo].[CarritoProducto] OFF
GO
SET IDENTITY_INSERT [dbo].[Categorias] ON 

INSERT [dbo].[Categorias] ([Id], [Nombre], [CategoriaId], [EmpresaId]) VALUES (1, N'Ropa', NULL, 1)
INSERT [dbo].[Categorias] ([Id], [Nombre], [CategoriaId], [EmpresaId]) VALUES (2, N'Electrodomesticos', NULL, 1)
INSERT [dbo].[Categorias] ([Id], [Nombre], [CategoriaId], [EmpresaId]) VALUES (3, N'Tecnologia', 2, 1)
INSERT [dbo].[Categorias] ([Id], [Nombre], [CategoriaId], [EmpresaId]) VALUES (4, N'Celulares', 3, 1)
INSERT [dbo].[Categorias] ([Id], [Nombre], [CategoriaId], [EmpresaId]) VALUES (5, N'Ropa', NULL, 1)
INSERT [dbo].[Categorias] ([Id], [Nombre], [CategoriaId], [EmpresaId]) VALUES (6, N'Electrodomesticos', NULL, 1)
INSERT [dbo].[Categorias] ([Id], [Nombre], [CategoriaId], [EmpresaId]) VALUES (7, N'Tecnologia', 2, 1)
INSERT [dbo].[Categorias] ([Id], [Nombre], [CategoriaId], [EmpresaId]) VALUES (8, N'Celulares', 3, 1)
INSERT [dbo].[Categorias] ([Id], [Nombre], [CategoriaId], [EmpresaId]) VALUES (9, N'Ropa', NULL, 1)
INSERT [dbo].[Categorias] ([Id], [Nombre], [CategoriaId], [EmpresaId]) VALUES (10, N'Electrodomesticos', NULL, 1)
INSERT [dbo].[Categorias] ([Id], [Nombre], [CategoriaId], [EmpresaId]) VALUES (11, N'Tecnologia', 2, 1)
INSERT [dbo].[Categorias] ([Id], [Nombre], [CategoriaId], [EmpresaId]) VALUES (12, N'Celulares', 3, 1)
INSERT [dbo].[Categorias] ([Id], [Nombre], [CategoriaId], [EmpresaId]) VALUES (13, N'Ropa', NULL, 1)
INSERT [dbo].[Categorias] ([Id], [Nombre], [CategoriaId], [EmpresaId]) VALUES (14, N'Electrodomesticos', NULL, 1)
INSERT [dbo].[Categorias] ([Id], [Nombre], [CategoriaId], [EmpresaId]) VALUES (15, N'Tecnologia', 2, 1)
INSERT [dbo].[Categorias] ([Id], [Nombre], [CategoriaId], [EmpresaId]) VALUES (16, N'Celulares', 3, 1)
SET IDENTITY_INSERT [dbo].[Categorias] OFF
GO
SET IDENTITY_INSERT [dbo].[Empresas] ON 

INSERT [dbo].[Empresas] ([Id], [Nombre], [RUT]) VALUES (1, N'san jose solutions', N'1234')
INSERT [dbo].[Empresas] ([Id], [Nombre], [RUT]) VALUES (2, N'san jose solutions', N'1234')
INSERT [dbo].[Empresas] ([Id], [Nombre], [RUT]) VALUES (3, N'san jose solutions', N'1234')
INSERT [dbo].[Empresas] ([Id], [Nombre], [RUT]) VALUES (4, N'san jose solutions', N'1234')
INSERT [dbo].[Empresas] ([Id], [Nombre], [RUT]) VALUES (5, N'san jose solutions', N'1234')
INSERT [dbo].[Empresas] ([Id], [Nombre], [RUT]) VALUES (6, N'san jose solutions', N'1234')
INSERT [dbo].[Empresas] ([Id], [Nombre], [RUT]) VALUES (7, N'san jose solutions', N'1234')
INSERT [dbo].[Empresas] ([Id], [Nombre], [RUT]) VALUES (8, N'san jose solutions', N'1234')
SET IDENTITY_INSERT [dbo].[Empresas] OFF
GO
SET IDENTITY_INSERT [dbo].[OC] ON 

INSERT [dbo].[OC] ([Id], [MedioDePago], [DireccionDeEnvio], [FechaEstimadaEntrega], [Total], [EstadoOrden], [Fecha], [ClienteId], [EmpresaId], [SucursalId]) VALUES (1, N'Paypal', N'', CAST(N'2023-12-12T02:19:35.9730000' AS DateTime2), CAST(6000.00 AS Decimal(18, 2)), N'Entregado', CAST(N'2023-12-07T02:20:32.1960000' AS DateTime2), N'21238fa9-1118-4994-b19f-b5b7843fc381', 1, 2)
SET IDENTITY_INSERT [dbo].[OC] OFF
GO
SET IDENTITY_INSERT [dbo].[Opiniones] ON 

INSERT [dbo].[Opiniones] ([Id], [Titulo], [Descripcion], [Estrellas], [ProductoId], [ClienteId]) VALUES (1, N'Muy buena tele', N'Excelente calidad', 4, 2, NULL)
SET IDENTITY_INSERT [dbo].[Opiniones] OFF
GO
SET IDENTITY_INSERT [dbo].[Productos] ON 

INSERT [dbo].[Productos] ([Id], [Titulo], [Descripcion], [Foto], [Precio], [Tipo_iva], [Pdf], [EmpresaId], [CategoriaId]) VALUES (1, N'Remera', N'remera 100% algodon', N'18a3daa0-0ab3-4b10-b879-9d9f037c3c29.png', 200, N'General', N'1d8d3a90-1843-403d-bdcf-d7aa21517cd1.pdf', 1, 1)
INSERT [dbo].[Productos] ([Id], [Titulo], [Descripcion], [Foto], [Precio], [Tipo_iva], [Pdf], [EmpresaId], [CategoriaId]) VALUES (2, N'Televisor philips', N'Telivisor 4k uhd', N'7b99307f-8aba-4e14-a647-925992626b44.webp', 4000, N'Reducido', N'fc9c1e84-1999-4d18-b6eb-c5235d372e98.pdf', 1, 3)
INSERT [dbo].[Productos] ([Id], [Titulo], [Descripcion], [Foto], [Precio], [Tipo_iva], [Pdf], [EmpresaId], [CategoriaId]) VALUES (3, N'Iphone 13', N'Iphone 3 de 256gb', N'12bcf4f8-8075-4f74-b650-939a3c9e1bf9.jpg', 2000, N'Exonerado', N'3709033e-c69b-4c56-b921-875e1d56f0ba.pdf', 1, 4)
SET IDENTITY_INSERT [dbo].[Productos] OFF
GO
SET IDENTITY_INSERT [dbo].[Reclamos] ON 

INSERT [dbo].[Reclamos] ([Id], [Descripcion], [Fecha], [EmpresaId], [OCId]) VALUES (1, N'Che todavia no me llegan mis productos', CAST(N'2023-12-07T02:21:15.2670000' AS DateTime2), 1, 1)
SET IDENTITY_INSERT [dbo].[Reclamos] OFF
GO
SET IDENTITY_INSERT [dbo].[Sucursales] ON 

INSERT [dbo].[Sucursales] ([Id], [Nombre], [Ubicacion], [TiempoEntrega], [EmpresaId]) VALUES (1, N'Sucursal montevideo', N'lat: -34.901336955271496, lng: -56.164699828871626', 3, 1)
INSERT [dbo].[Sucursales] ([Id], [Nombre], [Ubicacion], [TiempoEntrega], [EmpresaId]) VALUES (2, N'Sucursal San Jose', N'lat: -34.34231817135191, lng: -56.71076858988371', 5, 1)
INSERT [dbo].[Sucursales] ([Id], [Nombre], [Ubicacion], [TiempoEntrega], [EmpresaId]) VALUES (3, N'Sucursal montevideo', N'lat: -34.901336955271496, lng: -56.164699828871626', 3, 1)
INSERT [dbo].[Sucursales] ([Id], [Nombre], [Ubicacion], [TiempoEntrega], [EmpresaId]) VALUES (4, N'Sucursal San Jose', N'lat: -34.34231817135191, lng: -56.71076858988371', 5, 1)
SET IDENTITY_INSERT [dbo].[Sucursales] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 6/12/2023 23:49:21 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 6/12/2023 23:49:21 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 6/12/2023 23:49:21 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 6/12/2023 23:49:21 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 6/12/2023 23:49:21 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 6/12/2023 23:49:21 ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_AspNetUsers_EmpresasId]    Script Date: 6/12/2023 23:49:21 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUsers_EmpresasId] ON [dbo].[AspNetUsers]
(
	[EmpresasId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 6/12/2023 23:49:21 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CarritoProducto_OCId]    Script Date: 6/12/2023 23:49:21 ******/
CREATE NONCLUSTERED INDEX [IX_CarritoProducto_OCId] ON [dbo].[CarritoProducto]
(
	[OCId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CarritoProducto_ProductoId]    Script Date: 6/12/2023 23:49:21 ******/
CREATE NONCLUSTERED INDEX [IX_CarritoProducto_ProductoId] ON [dbo].[CarritoProducto]
(
	[ProductoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Categorias_CategoriaId]    Script Date: 6/12/2023 23:49:21 ******/
CREATE NONCLUSTERED INDEX [IX_Categorias_CategoriaId] ON [dbo].[Categorias]
(
	[CategoriaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Categorias_EmpresaId]    Script Date: 6/12/2023 23:49:21 ******/
CREATE NONCLUSTERED INDEX [IX_Categorias_EmpresaId] ON [dbo].[Categorias]
(
	[EmpresaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Facturas_EmpresaId]    Script Date: 6/12/2023 23:49:21 ******/
CREATE NONCLUSTERED INDEX [IX_Facturas_EmpresaId] ON [dbo].[Facturas]
(
	[EmpresaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_OC_ClienteId]    Script Date: 6/12/2023 23:49:21 ******/
CREATE NONCLUSTERED INDEX [IX_OC_ClienteId] ON [dbo].[OC]
(
	[ClienteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_OC_EmpresaId]    Script Date: 6/12/2023 23:49:21 ******/
CREATE NONCLUSTERED INDEX [IX_OC_EmpresaId] ON [dbo].[OC]
(
	[EmpresaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_OC_SucursalId]    Script Date: 6/12/2023 23:49:21 ******/
CREATE NONCLUSTERED INDEX [IX_OC_SucursalId] ON [dbo].[OC]
(
	[SucursalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Opiniones_ClienteId]    Script Date: 6/12/2023 23:49:21 ******/
CREATE NONCLUSTERED INDEX [IX_Opiniones_ClienteId] ON [dbo].[Opiniones]
(
	[ClienteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Opiniones_ProductoId]    Script Date: 6/12/2023 23:49:21 ******/
CREATE NONCLUSTERED INDEX [IX_Opiniones_ProductoId] ON [dbo].[Opiniones]
(
	[ProductoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Productos_CategoriaId]    Script Date: 6/12/2023 23:49:21 ******/
CREATE NONCLUSTERED INDEX [IX_Productos_CategoriaId] ON [dbo].[Productos]
(
	[CategoriaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Productos_EmpresaId]    Script Date: 6/12/2023 23:49:21 ******/
CREATE NONCLUSTERED INDEX [IX_Productos_EmpresaId] ON [dbo].[Productos]
(
	[EmpresaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Reclamos_EmpresaId]    Script Date: 6/12/2023 23:49:21 ******/
CREATE NONCLUSTERED INDEX [IX_Reclamos_EmpresaId] ON [dbo].[Reclamos]
(
	[EmpresaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Reclamos_OCId]    Script Date: 6/12/2023 23:49:21 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Reclamos_OCId] ON [dbo].[Reclamos]
(
	[OCId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Sucursales_EmpresaId]    Script Date: 6/12/2023 23:49:21 ******/
CREATE NONCLUSTERED INDEX [IX_Sucursales_EmpresaId] ON [dbo].[Sucursales]
(
	[EmpresaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUsers]  WITH NOCHECK ADD  CONSTRAINT [FK_AspNetUsers_Empresas_EmpresasId] FOREIGN KEY([EmpresasId])
REFERENCES [dbo].[Empresas] ([Id])
GO
ALTER TABLE [dbo].[AspNetUsers] CHECK CONSTRAINT [FK_AspNetUsers_Empresas_EmpresasId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[CarritoProducto]  WITH CHECK ADD  CONSTRAINT [FK_CarritoProducto_OC_OCId] FOREIGN KEY([OCId])
REFERENCES [dbo].[OC] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CarritoProducto] CHECK CONSTRAINT [FK_CarritoProducto_OC_OCId]
GO
ALTER TABLE [dbo].[CarritoProducto]  WITH CHECK ADD  CONSTRAINT [FK_CarritoProducto_Productos_ProductoId] FOREIGN KEY([ProductoId])
REFERENCES [dbo].[Productos] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CarritoProducto] CHECK CONSTRAINT [FK_CarritoProducto_Productos_ProductoId]
GO
ALTER TABLE [dbo].[Categorias]  WITH NOCHECK ADD  CONSTRAINT [FK_Categorias_Categorias_CategoriaId] FOREIGN KEY([CategoriaId])
REFERENCES [dbo].[Categorias] ([Id])
GO
ALTER TABLE [dbo].[Categorias] CHECK CONSTRAINT [FK_Categorias_Categorias_CategoriaId]
GO
ALTER TABLE [dbo].[Categorias]  WITH NOCHECK ADD  CONSTRAINT [FK_Categorias_Empresas_EmpresaId] FOREIGN KEY([EmpresaId])
REFERENCES [dbo].[Empresas] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Categorias] CHECK CONSTRAINT [FK_Categorias_Empresas_EmpresaId]
GO
ALTER TABLE [dbo].[Facturas]  WITH CHECK ADD  CONSTRAINT [FK_Facturas_Empresas_EmpresaId] FOREIGN KEY([EmpresaId])
REFERENCES [dbo].[Empresas] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Facturas] CHECK CONSTRAINT [FK_Facturas_Empresas_EmpresaId]
GO
ALTER TABLE [dbo].[OC]  WITH CHECK ADD  CONSTRAINT [FK_OC_AspNetUsers_ClienteId] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OC] CHECK CONSTRAINT [FK_OC_AspNetUsers_ClienteId]
GO
ALTER TABLE [dbo].[OC]  WITH CHECK ADD  CONSTRAINT [FK_OC_Empresas_EmpresaId] FOREIGN KEY([EmpresaId])
REFERENCES [dbo].[Empresas] ([Id])
GO
ALTER TABLE [dbo].[OC] CHECK CONSTRAINT [FK_OC_Empresas_EmpresaId]
GO
ALTER TABLE [dbo].[OC]  WITH CHECK ADD  CONSTRAINT [FK_OC_Sucursales_SucursalId] FOREIGN KEY([SucursalId])
REFERENCES [dbo].[Sucursales] ([Id])
GO
ALTER TABLE [dbo].[OC] CHECK CONSTRAINT [FK_OC_Sucursales_SucursalId]
GO
ALTER TABLE [dbo].[Opiniones]  WITH CHECK ADD  CONSTRAINT [FK_Opiniones_AspNetUsers_ClienteId] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Opiniones] CHECK CONSTRAINT [FK_Opiniones_AspNetUsers_ClienteId]
GO
ALTER TABLE [dbo].[Opiniones]  WITH CHECK ADD  CONSTRAINT [FK_Opiniones_Productos_ProductoId] FOREIGN KEY([ProductoId])
REFERENCES [dbo].[Productos] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Opiniones] CHECK CONSTRAINT [FK_Opiniones_Productos_ProductoId]
GO
ALTER TABLE [dbo].[Productos]  WITH CHECK ADD  CONSTRAINT [FK_Productos_Categorias_CategoriaId] FOREIGN KEY([CategoriaId])
REFERENCES [dbo].[Categorias] ([Id])
GO
ALTER TABLE [dbo].[Productos] CHECK CONSTRAINT [FK_Productos_Categorias_CategoriaId]
GO
ALTER TABLE [dbo].[Productos]  WITH CHECK ADD  CONSTRAINT [FK_Productos_Empresas_EmpresaId] FOREIGN KEY([EmpresaId])
REFERENCES [dbo].[Empresas] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Productos] CHECK CONSTRAINT [FK_Productos_Empresas_EmpresaId]
GO
ALTER TABLE [dbo].[Reclamos]  WITH CHECK ADD  CONSTRAINT [FK_Reclamos_Empresas_EmpresaId] FOREIGN KEY([EmpresaId])
REFERENCES [dbo].[Empresas] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Reclamos] CHECK CONSTRAINT [FK_Reclamos_Empresas_EmpresaId]
GO
ALTER TABLE [dbo].[Reclamos]  WITH CHECK ADD  CONSTRAINT [FK_Reclamos_OC_OCId] FOREIGN KEY([OCId])
REFERENCES [dbo].[OC] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Reclamos] CHECK CONSTRAINT [FK_Reclamos_OC_OCId]
GO
ALTER TABLE [dbo].[Sucursales]  WITH NOCHECK ADD  CONSTRAINT [FK_Sucursales_Empresas_EmpresaId] FOREIGN KEY([EmpresaId])
REFERENCES [dbo].[Empresas] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Sucursales] CHECK CONSTRAINT [FK_Sucursales_Empresas_EmpresaId]
GO
USE [master]
GO
ALTER DATABASE [Practico4] SET  READ_WRITE 
GO
