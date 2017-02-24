
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/24/2017 10:37:46
-- Generated from EDMX file: D:\Data\Real\Apps\GitHub\eJobs\JobsV1\Models\JobDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [aspnet-JobsV1-20160528101923];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_JobMainJobType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobTypes] DROP CONSTRAINT [FK_JobMainJobType];
GO
IF OBJECT_ID(N'[dbo].[FK_JobMainJobSupplier]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobServices] DROP CONSTRAINT [FK_JobMainJobSupplier];
GO
IF OBJECT_ID(N'[dbo].[FK_SupplierJobSupplier]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobServices] DROP CONSTRAINT [FK_SupplierJobSupplier];
GO
IF OBJECT_ID(N'[dbo].[FK_CustomerJobMain]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobMains] DROP CONSTRAINT [FK_CustomerJobMain];
GO
IF OBJECT_ID(N'[dbo].[FK_ServicesJobServices]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobServices] DROP CONSTRAINT [FK_ServicesJobServices];
GO
IF OBJECT_ID(N'[dbo].[FK_JobMainJobItinerary]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobItineraries] DROP CONSTRAINT [FK_JobMainJobItinerary];
GO
IF OBJECT_ID(N'[dbo].[FK_DestinationJobItinerary]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobItineraries] DROP CONSTRAINT [FK_DestinationJobItinerary];
GO
IF OBJECT_ID(N'[dbo].[FK_JobMainJobPickup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobPickups] DROP CONSTRAINT [FK_JobMainJobPickup];
GO
IF OBJECT_ID(N'[dbo].[FK_CityBranch]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Branches] DROP CONSTRAINT [FK_CityBranch];
GO
IF OBJECT_ID(N'[dbo].[FK_CitySupplier]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Suppliers] DROP CONSTRAINT [FK_CitySupplier];
GO
IF OBJECT_ID(N'[dbo].[FK_BranchJobMain]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobMains] DROP CONSTRAINT [FK_BranchJobMain];
GO
IF OBJECT_ID(N'[dbo].[FK_CityDestination]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Destinations] DROP CONSTRAINT [FK_CityDestination];
GO
IF OBJECT_ID(N'[dbo].[FK_SupplierTypeSupplier]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Suppliers] DROP CONSTRAINT [FK_SupplierTypeSupplier];
GO
IF OBJECT_ID(N'[dbo].[FK_SupplierSupplierItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SupplierItems] DROP CONSTRAINT [FK_SupplierSupplierItem];
GO
IF OBJECT_ID(N'[dbo].[FK_SupplierItemJobServices]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobServices] DROP CONSTRAINT [FK_SupplierItemJobServices];
GO
IF OBJECT_ID(N'[dbo].[FK_JobServicesJobServicePickup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobServicePickups] DROP CONSTRAINT [FK_JobServicesJobServicePickup];
GO
IF OBJECT_ID(N'[dbo].[FK_JobStatusJobMain]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobMains] DROP CONSTRAINT [FK_JobStatusJobMain];
GO
IF OBJECT_ID(N'[dbo].[FK_JobThruJobMain]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobMains] DROP CONSTRAINT [FK_JobThruJobMain];
GO
IF OBJECT_ID(N'[dbo].[FK_JobMainJobPayment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobPayments] DROP CONSTRAINT [FK_JobMainJobPayment];
GO
IF OBJECT_ID(N'[dbo].[FK_BankJobPayment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobPayments] DROP CONSTRAINT [FK_BankJobPayment];
GO
IF OBJECT_ID(N'[dbo].[FK_CarCategoryCarUnit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CarUnits] DROP CONSTRAINT [FK_CarCategoryCarUnit];
GO
IF OBJECT_ID(N'[dbo].[FK_CityCarDestination]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CarDestinations] DROP CONSTRAINT [FK_CityCarDestination];
GO
IF OBJECT_ID(N'[dbo].[FK_CarUnitCarRate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CarRates] DROP CONSTRAINT [FK_CarUnitCarRate];
GO
IF OBJECT_ID(N'[dbo].[FK_CarUnitCarReservation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CarReservations] DROP CONSTRAINT [FK_CarUnitCarReservation];
GO
IF OBJECT_ID(N'[dbo].[FK_CarUnitCarImage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CarImages] DROP CONSTRAINT [FK_CarUnitCarImage];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductProductPrice]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductPrices] DROP CONSTRAINT [FK_ProductProductPrice];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductProductImages]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductImages1] DROP CONSTRAINT [FK_ProductProductImages];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductProductCondition]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductConditions] DROP CONSTRAINT [FK_ProductProductCondition];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[JobMains]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JobMains];
GO
IF OBJECT_ID(N'[dbo].[Suppliers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Suppliers];
GO
IF OBJECT_ID(N'[dbo].[Customers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customers];
GO
IF OBJECT_ID(N'[dbo].[JobTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JobTypes];
GO
IF OBJECT_ID(N'[dbo].[Services]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Services];
GO
IF OBJECT_ID(N'[dbo].[JobServices]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JobServices];
GO
IF OBJECT_ID(N'[dbo].[JobItineraries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JobItineraries];
GO
IF OBJECT_ID(N'[dbo].[Destinations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Destinations];
GO
IF OBJECT_ID(N'[dbo].[JobPickups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JobPickups];
GO
IF OBJECT_ID(N'[dbo].[Branches]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Branches];
GO
IF OBJECT_ID(N'[dbo].[Cities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cities];
GO
IF OBJECT_ID(N'[dbo].[SupplierTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SupplierTypes];
GO
IF OBJECT_ID(N'[dbo].[SupplierItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SupplierItems];
GO
IF OBJECT_ID(N'[dbo].[JobServicePickups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JobServicePickups];
GO
IF OBJECT_ID(N'[dbo].[JobStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JobStatus];
GO
IF OBJECT_ID(N'[dbo].[JobThrus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JobThrus];
GO
IF OBJECT_ID(N'[dbo].[Banks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Banks];
GO
IF OBJECT_ID(N'[dbo].[JobPayments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JobPayments];
GO
IF OBJECT_ID(N'[dbo].[CarCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CarCategories];
GO
IF OBJECT_ID(N'[dbo].[CarUnits]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CarUnits];
GO
IF OBJECT_ID(N'[dbo].[CarDestinations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CarDestinations];
GO
IF OBJECT_ID(N'[dbo].[CarRates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CarRates];
GO
IF OBJECT_ID(N'[dbo].[CarReservations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CarReservations];
GO
IF OBJECT_ID(N'[dbo].[CarImages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CarImages];
GO
IF OBJECT_ID(N'[dbo].[JobContacts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JobContacts];
GO
IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO
IF OBJECT_ID(N'[dbo].[ProductPrices]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductPrices];
GO
IF OBJECT_ID(N'[dbo].[ProductImages1]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductImages1];
GO
IF OBJECT_ID(N'[dbo].[ProductConditions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductConditions];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'JobMains'
CREATE TABLE [dbo].[JobMains] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [JobDate] datetime  NOT NULL,
    [CustomerId] int  NOT NULL,
    [Description] nvarchar(180)  NOT NULL,
    [NoOfPax] int  NOT NULL,
    [NoOfDays] int  NOT NULL,
    [JobRemarks] nvarchar(180)  NULL,
    [JobStatusId] int  NOT NULL,
    [StatusRemarks] nvarchar(max)  NULL,
    [BranchId] int  NOT NULL,
    [JobThruId] int  NOT NULL,
    [AgreedAmt] decimal(18,0)  NULL
);
GO

-- Creating table 'Suppliers'
CREATE TABLE [dbo].[Suppliers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Contact1] nvarchar(20)  NULL,
    [Contact2] nvarchar(20)  NULL,
    [Contact3] nvarchar(20)  NULL,
    [Email] nvarchar(50)  NULL,
    [Details] nvarchar(80)  NULL,
    [CityId] int  NOT NULL,
    [SupplierTypeId] int  NOT NULL
);
GO

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(80)  NOT NULL,
    [Email] nvarchar(80)  NULL,
    [Contact1] nvarchar(20)  NULL,
    [Contact2] nvarchar(20)  NULL,
    [Remarks] nvarchar(120)  NULL,
    [Status] nvarchar(3)  NULL
);
GO

-- Creating table 'JobTypes'
CREATE TABLE [dbo].[JobTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [JobMainId] int  NOT NULL,
    [TTicket] int  NULL,
    [TTransport] int  NULL,
    [TTour] int  NULL,
    [THotel] int  NULL,
    [TOthers] int  NULL,
    [Remarks] nvarchar(80)  NULL
);
GO

-- Creating table 'Services'
CREATE TABLE [dbo].[Services] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(30)  NOT NULL,
    [Description] nvarchar(80)  NULL
);
GO

-- Creating table 'JobServices'
CREATE TABLE [dbo].[JobServices] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [JobMainId] int  NOT NULL,
    [ServicesId] int  NOT NULL,
    [SupplierId] int  NOT NULL,
    [Particulars] nvarchar(80)  NULL,
    [QuotedAmt] decimal(18,0)  NULL,
    [SupplierAmt] decimal(18,0)  NULL,
    [ActualAmt] decimal(18,0)  NULL,
    [Remarks] nvarchar(80)  NULL,
    [SupplierItemId] int  NOT NULL,
    [DtStart] datetime  NULL,
    [DtEnd] datetime  NULL
);
GO

-- Creating table 'JobItineraries'
CREATE TABLE [dbo].[JobItineraries] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [JobMainId] int  NOT NULL,
    [DestinationId] int  NOT NULL,
    [ActualRate] decimal(18,0)  NULL,
    [Remarks] nvarchar(80)  NULL,
    [ItiDate] datetime  NULL
);
GO

-- Creating table 'Destinations'
CREATE TABLE [dbo].[Destinations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(80)  NOT NULL,
    [PubRate] decimal(18,0)  NULL,
    [ConRate] decimal(18,0)  NULL,
    [Remarks] nvarchar(150)  NULL,
    [CityId] int  NOT NULL
);
GO

-- Creating table 'JobPickups'
CREATE TABLE [dbo].[JobPickups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [JobMainId] int  NOT NULL,
    [puDate] datetime  NOT NULL,
    [puTime] nvarchar(5)  NOT NULL,
    [puLocation] nvarchar(80)  NOT NULL,
    [ContactName] nvarchar(80)  NOT NULL,
    [ContactNumber] nvarchar(20)  NOT NULL
);
GO

-- Creating table 'Branches'
CREATE TABLE [dbo].[Branches] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(30)  NOT NULL,
    [CityId] int  NOT NULL,
    [Remarks] nvarchar(80)  NULL,
    [Address] nvarchar(180)  NULL,
    [Landline] nvarchar(20)  NULL,
    [Mobile] nvarchar(20)  NULL
);
GO

-- Creating table 'Cities'
CREATE TABLE [dbo].[Cities] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(20)  NOT NULL
);
GO

-- Creating table 'SupplierTypes'
CREATE TABLE [dbo].[SupplierTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SupplierItems'
CREATE TABLE [dbo].[SupplierItems] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(30)  NOT NULL,
    [Remarks] nvarchar(80)  NULL,
    [SupplierId] int  NOT NULL,
    [InCharge] nvarchar(30)  NULL,
    [Tel1] nvarchar(max)  NULL,
    [Tel2] nvarchar(max)  NULL,
    [Tel3] nvarchar(max)  NULL
);
GO

-- Creating table 'JobServicePickups'
CREATE TABLE [dbo].[JobServicePickups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [JobServicesId] int  NOT NULL,
    [JsDate] datetime  NOT NULL,
    [JsTime] nvarchar(10)  NULL,
    [JsLocation] nvarchar(80)  NULL,
    [ClientName] nvarchar(80)  NULL,
    [ClientContact] nvarchar(50)  NULL,
    [ProviderName] nvarchar(80)  NULL,
    [ProviderContact] nvarchar(50)  NULL
);
GO

-- Creating table 'JobStatus'
CREATE TABLE [dbo].[JobStatus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Status] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'JobThrus'
CREATE TABLE [dbo].[JobThrus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Desc] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Banks'
CREATE TABLE [dbo].[Banks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BankName] nvarchar(max)  NOT NULL,
    [BankBranch] nvarchar(max)  NOT NULL,
    [AccntName] nvarchar(max)  NOT NULL,
    [AccntNo] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'JobPayments'
CREATE TABLE [dbo].[JobPayments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [JobMainId] int  NOT NULL,
    [DtPayment] datetime  NOT NULL,
    [PaymentAmt] decimal(18,0)  NOT NULL,
    [Remarks] nvarchar(max)  NULL,
    [BankId] int  NOT NULL
);
GO

-- Creating table 'CarCategories'
CREATE TABLE [dbo].[CarCategories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NULL,
    [Remarks] nvarchar(max)  NULL
);
GO

-- Creating table 'CarUnits'
CREATE TABLE [dbo].[CarUnits] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NULL,
    [Remarks] nvarchar(max)  NULL,
    [CarCategoryId] int  NOT NULL
);
GO

-- Creating table 'CarDestinations'
CREATE TABLE [dbo].[CarDestinations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CityId] int  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Kms] int  NOT NULL
);
GO

-- Creating table 'CarRates'
CREATE TABLE [dbo].[CarRates] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Daily] decimal(18,0)  NOT NULL,
    [Weekly] decimal(18,0)  NOT NULL,
    [Monthly] decimal(18,0)  NOT NULL,
    [KmFree] int  NOT NULL,
    [KmRate] int  NOT NULL,
    [CarUnitId] int  NOT NULL,
    [OtRate] decimal(18,0)  NOT NULL
);
GO

-- Creating table 'CarReservations'
CREATE TABLE [dbo].[CarReservations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DtTrx] datetime  NOT NULL,
    [CarUnitId] int  NOT NULL,
    [DtStart] nvarchar(max)  NOT NULL,
    [LocStart] nvarchar(max)  NULL,
    [DtEnd] nvarchar(max)  NOT NULL,
    [LocEnd] nvarchar(max)  NULL,
    [BaseRate] nvarchar(max)  NOT NULL,
    [Destinations] nvarchar(max)  NOT NULL,
    [UseFor] nvarchar(max)  NOT NULL,
    [RenterName] nvarchar(max)  NOT NULL,
    [RenterCompany] nvarchar(max)  NULL,
    [RenterEmail] nvarchar(max)  NOT NULL,
    [RenterMobile] nvarchar(max)  NOT NULL,
    [RenterAddress] nvarchar(max)  NULL,
    [RenterFbAccnt] nvarchar(max)  NULL,
    [RenterLinkedInAccnt] nvarchar(max)  NULL,
    [EstHrPerDay] int  NULL,
    [EstKmTravel] int  NULL
);
GO

-- Creating table 'CarImages'
CREATE TABLE [dbo].[CarImages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CarUnitId] int  NOT NULL,
    [ImgUrl] nvarchar(max)  NOT NULL,
    [Remarks] nvarchar(max)  NOT NULL,
    [SysCode] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'JobContacts'
CREATE TABLE [dbo].[JobContacts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ShortName] nvarchar(20)  NULL,
    [Company] nvarchar(50)  NULL,
    [Position] nvarchar(50)  NULL,
    [Tel1] nvarchar(50)  NULL,
    [Tel2] nvarchar(50)  NULL,
    [Email] nvarchar(100)  NULL,
    [AddInfo] nvarchar(250)  NULL,
    [Remarks] nvarchar(250)  NULL,
    [ContactType] nvarchar(5)  NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] nvarchar(30)  NOT NULL,
    [TemplateId] int  NULL,
    [Description] nvarchar(80)  NOT NULL,
    [Remarks] nvarchar(250)  NULL,
    [Sort] int  NOT NULL,
    [Status] nvarchar(3)  NOT NULL
);
GO

-- Creating table 'ProductPrices'
CREATE TABLE [dbo].[ProductPrices] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProductId] int  NOT NULL,
    [Uom] nvarchar(20)  NOT NULL,
    [Qty] int  NOT NULL,
    [Rate] decimal(18,0)  NOT NULL,
    [Rate1] decimal(18,0)  NOT NULL
);
GO

-- Creating table 'ProductImages1'
CREATE TABLE [dbo].[ProductImages1] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProductId] int  NOT NULL,
    [Path] nvarchar(250)  NOT NULL
);
GO

-- Creating table 'ProductConditions'
CREATE TABLE [dbo].[ProductConditions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProductId] int  NOT NULL,
    [Description] nvarchar(250)  NOT NULL,
    [Remarks] nvarchar(250)  NULL
);
GO

-- Creating table 'ProductCategories'
CREATE TABLE [dbo].[ProductCategories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Remarks] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ProductProdCats'
CREATE TABLE [dbo].[ProductProdCats] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProductCategoryId] int  NOT NULL,
    [ProductId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'JobMains'
ALTER TABLE [dbo].[JobMains]
ADD CONSTRAINT [PK_JobMains]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Suppliers'
ALTER TABLE [dbo].[Suppliers]
ADD CONSTRAINT [PK_Suppliers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'JobTypes'
ALTER TABLE [dbo].[JobTypes]
ADD CONSTRAINT [PK_JobTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Services'
ALTER TABLE [dbo].[Services]
ADD CONSTRAINT [PK_Services]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'JobServices'
ALTER TABLE [dbo].[JobServices]
ADD CONSTRAINT [PK_JobServices]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'JobItineraries'
ALTER TABLE [dbo].[JobItineraries]
ADD CONSTRAINT [PK_JobItineraries]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Destinations'
ALTER TABLE [dbo].[Destinations]
ADD CONSTRAINT [PK_Destinations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'JobPickups'
ALTER TABLE [dbo].[JobPickups]
ADD CONSTRAINT [PK_JobPickups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Branches'
ALTER TABLE [dbo].[Branches]
ADD CONSTRAINT [PK_Branches]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Cities'
ALTER TABLE [dbo].[Cities]
ADD CONSTRAINT [PK_Cities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SupplierTypes'
ALTER TABLE [dbo].[SupplierTypes]
ADD CONSTRAINT [PK_SupplierTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SupplierItems'
ALTER TABLE [dbo].[SupplierItems]
ADD CONSTRAINT [PK_SupplierItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'JobServicePickups'
ALTER TABLE [dbo].[JobServicePickups]
ADD CONSTRAINT [PK_JobServicePickups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'JobStatus'
ALTER TABLE [dbo].[JobStatus]
ADD CONSTRAINT [PK_JobStatus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'JobThrus'
ALTER TABLE [dbo].[JobThrus]
ADD CONSTRAINT [PK_JobThrus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Banks'
ALTER TABLE [dbo].[Banks]
ADD CONSTRAINT [PK_Banks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'JobPayments'
ALTER TABLE [dbo].[JobPayments]
ADD CONSTRAINT [PK_JobPayments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CarCategories'
ALTER TABLE [dbo].[CarCategories]
ADD CONSTRAINT [PK_CarCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CarUnits'
ALTER TABLE [dbo].[CarUnits]
ADD CONSTRAINT [PK_CarUnits]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CarDestinations'
ALTER TABLE [dbo].[CarDestinations]
ADD CONSTRAINT [PK_CarDestinations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CarRates'
ALTER TABLE [dbo].[CarRates]
ADD CONSTRAINT [PK_CarRates]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CarReservations'
ALTER TABLE [dbo].[CarReservations]
ADD CONSTRAINT [PK_CarReservations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CarImages'
ALTER TABLE [dbo].[CarImages]
ADD CONSTRAINT [PK_CarImages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'JobContacts'
ALTER TABLE [dbo].[JobContacts]
ADD CONSTRAINT [PK_JobContacts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductPrices'
ALTER TABLE [dbo].[ProductPrices]
ADD CONSTRAINT [PK_ProductPrices]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductImages1'
ALTER TABLE [dbo].[ProductImages1]
ADD CONSTRAINT [PK_ProductImages1]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductConditions'
ALTER TABLE [dbo].[ProductConditions]
ADD CONSTRAINT [PK_ProductConditions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductCategories'
ALTER TABLE [dbo].[ProductCategories]
ADD CONSTRAINT [PK_ProductCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductProdCats'
ALTER TABLE [dbo].[ProductProdCats]
ADD CONSTRAINT [PK_ProductProdCats]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [JobMainId] in table 'JobTypes'
ALTER TABLE [dbo].[JobTypes]
ADD CONSTRAINT [FK_JobMainJobType]
    FOREIGN KEY ([JobMainId])
    REFERENCES [dbo].[JobMains]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JobMainJobType'
CREATE INDEX [IX_FK_JobMainJobType]
ON [dbo].[JobTypes]
    ([JobMainId]);
GO

-- Creating foreign key on [JobMainId] in table 'JobServices'
ALTER TABLE [dbo].[JobServices]
ADD CONSTRAINT [FK_JobMainJobSupplier]
    FOREIGN KEY ([JobMainId])
    REFERENCES [dbo].[JobMains]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JobMainJobSupplier'
CREATE INDEX [IX_FK_JobMainJobSupplier]
ON [dbo].[JobServices]
    ([JobMainId]);
GO

-- Creating foreign key on [SupplierId] in table 'JobServices'
ALTER TABLE [dbo].[JobServices]
ADD CONSTRAINT [FK_SupplierJobSupplier]
    FOREIGN KEY ([SupplierId])
    REFERENCES [dbo].[Suppliers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SupplierJobSupplier'
CREATE INDEX [IX_FK_SupplierJobSupplier]
ON [dbo].[JobServices]
    ([SupplierId]);
GO

-- Creating foreign key on [CustomerId] in table 'JobMains'
ALTER TABLE [dbo].[JobMains]
ADD CONSTRAINT [FK_CustomerJobMain]
    FOREIGN KEY ([CustomerId])
    REFERENCES [dbo].[Customers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerJobMain'
CREATE INDEX [IX_FK_CustomerJobMain]
ON [dbo].[JobMains]
    ([CustomerId]);
GO

-- Creating foreign key on [ServicesId] in table 'JobServices'
ALTER TABLE [dbo].[JobServices]
ADD CONSTRAINT [FK_ServicesJobServices]
    FOREIGN KEY ([ServicesId])
    REFERENCES [dbo].[Services]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ServicesJobServices'
CREATE INDEX [IX_FK_ServicesJobServices]
ON [dbo].[JobServices]
    ([ServicesId]);
GO

-- Creating foreign key on [JobMainId] in table 'JobItineraries'
ALTER TABLE [dbo].[JobItineraries]
ADD CONSTRAINT [FK_JobMainJobItinerary]
    FOREIGN KEY ([JobMainId])
    REFERENCES [dbo].[JobMains]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JobMainJobItinerary'
CREATE INDEX [IX_FK_JobMainJobItinerary]
ON [dbo].[JobItineraries]
    ([JobMainId]);
GO

-- Creating foreign key on [DestinationId] in table 'JobItineraries'
ALTER TABLE [dbo].[JobItineraries]
ADD CONSTRAINT [FK_DestinationJobItinerary]
    FOREIGN KEY ([DestinationId])
    REFERENCES [dbo].[Destinations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DestinationJobItinerary'
CREATE INDEX [IX_FK_DestinationJobItinerary]
ON [dbo].[JobItineraries]
    ([DestinationId]);
GO

-- Creating foreign key on [JobMainId] in table 'JobPickups'
ALTER TABLE [dbo].[JobPickups]
ADD CONSTRAINT [FK_JobMainJobPickup]
    FOREIGN KEY ([JobMainId])
    REFERENCES [dbo].[JobMains]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JobMainJobPickup'
CREATE INDEX [IX_FK_JobMainJobPickup]
ON [dbo].[JobPickups]
    ([JobMainId]);
GO

-- Creating foreign key on [CityId] in table 'Branches'
ALTER TABLE [dbo].[Branches]
ADD CONSTRAINT [FK_CityBranch]
    FOREIGN KEY ([CityId])
    REFERENCES [dbo].[Cities]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CityBranch'
CREATE INDEX [IX_FK_CityBranch]
ON [dbo].[Branches]
    ([CityId]);
GO

-- Creating foreign key on [CityId] in table 'Suppliers'
ALTER TABLE [dbo].[Suppliers]
ADD CONSTRAINT [FK_CitySupplier]
    FOREIGN KEY ([CityId])
    REFERENCES [dbo].[Cities]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CitySupplier'
CREATE INDEX [IX_FK_CitySupplier]
ON [dbo].[Suppliers]
    ([CityId]);
GO

-- Creating foreign key on [BranchId] in table 'JobMains'
ALTER TABLE [dbo].[JobMains]
ADD CONSTRAINT [FK_BranchJobMain]
    FOREIGN KEY ([BranchId])
    REFERENCES [dbo].[Branches]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BranchJobMain'
CREATE INDEX [IX_FK_BranchJobMain]
ON [dbo].[JobMains]
    ([BranchId]);
GO

-- Creating foreign key on [CityId] in table 'Destinations'
ALTER TABLE [dbo].[Destinations]
ADD CONSTRAINT [FK_CityDestination]
    FOREIGN KEY ([CityId])
    REFERENCES [dbo].[Cities]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CityDestination'
CREATE INDEX [IX_FK_CityDestination]
ON [dbo].[Destinations]
    ([CityId]);
GO

-- Creating foreign key on [SupplierTypeId] in table 'Suppliers'
ALTER TABLE [dbo].[Suppliers]
ADD CONSTRAINT [FK_SupplierTypeSupplier]
    FOREIGN KEY ([SupplierTypeId])
    REFERENCES [dbo].[SupplierTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SupplierTypeSupplier'
CREATE INDEX [IX_FK_SupplierTypeSupplier]
ON [dbo].[Suppliers]
    ([SupplierTypeId]);
GO

-- Creating foreign key on [SupplierId] in table 'SupplierItems'
ALTER TABLE [dbo].[SupplierItems]
ADD CONSTRAINT [FK_SupplierSupplierItem]
    FOREIGN KEY ([SupplierId])
    REFERENCES [dbo].[Suppliers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SupplierSupplierItem'
CREATE INDEX [IX_FK_SupplierSupplierItem]
ON [dbo].[SupplierItems]
    ([SupplierId]);
GO

-- Creating foreign key on [SupplierItemId] in table 'JobServices'
ALTER TABLE [dbo].[JobServices]
ADD CONSTRAINT [FK_SupplierItemJobServices]
    FOREIGN KEY ([SupplierItemId])
    REFERENCES [dbo].[SupplierItems]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SupplierItemJobServices'
CREATE INDEX [IX_FK_SupplierItemJobServices]
ON [dbo].[JobServices]
    ([SupplierItemId]);
GO

-- Creating foreign key on [JobServicesId] in table 'JobServicePickups'
ALTER TABLE [dbo].[JobServicePickups]
ADD CONSTRAINT [FK_JobServicesJobServicePickup]
    FOREIGN KEY ([JobServicesId])
    REFERENCES [dbo].[JobServices]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JobServicesJobServicePickup'
CREATE INDEX [IX_FK_JobServicesJobServicePickup]
ON [dbo].[JobServicePickups]
    ([JobServicesId]);
GO

-- Creating foreign key on [JobStatusId] in table 'JobMains'
ALTER TABLE [dbo].[JobMains]
ADD CONSTRAINT [FK_JobStatusJobMain]
    FOREIGN KEY ([JobStatusId])
    REFERENCES [dbo].[JobStatus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JobStatusJobMain'
CREATE INDEX [IX_FK_JobStatusJobMain]
ON [dbo].[JobMains]
    ([JobStatusId]);
GO

-- Creating foreign key on [JobThruId] in table 'JobMains'
ALTER TABLE [dbo].[JobMains]
ADD CONSTRAINT [FK_JobThruJobMain]
    FOREIGN KEY ([JobThruId])
    REFERENCES [dbo].[JobThrus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JobThruJobMain'
CREATE INDEX [IX_FK_JobThruJobMain]
ON [dbo].[JobMains]
    ([JobThruId]);
GO

-- Creating foreign key on [JobMainId] in table 'JobPayments'
ALTER TABLE [dbo].[JobPayments]
ADD CONSTRAINT [FK_JobMainJobPayment]
    FOREIGN KEY ([JobMainId])
    REFERENCES [dbo].[JobMains]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JobMainJobPayment'
CREATE INDEX [IX_FK_JobMainJobPayment]
ON [dbo].[JobPayments]
    ([JobMainId]);
GO

-- Creating foreign key on [BankId] in table 'JobPayments'
ALTER TABLE [dbo].[JobPayments]
ADD CONSTRAINT [FK_BankJobPayment]
    FOREIGN KEY ([BankId])
    REFERENCES [dbo].[Banks]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BankJobPayment'
CREATE INDEX [IX_FK_BankJobPayment]
ON [dbo].[JobPayments]
    ([BankId]);
GO

-- Creating foreign key on [CarCategoryId] in table 'CarUnits'
ALTER TABLE [dbo].[CarUnits]
ADD CONSTRAINT [FK_CarCategoryCarUnit]
    FOREIGN KEY ([CarCategoryId])
    REFERENCES [dbo].[CarCategories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CarCategoryCarUnit'
CREATE INDEX [IX_FK_CarCategoryCarUnit]
ON [dbo].[CarUnits]
    ([CarCategoryId]);
GO

-- Creating foreign key on [CityId] in table 'CarDestinations'
ALTER TABLE [dbo].[CarDestinations]
ADD CONSTRAINT [FK_CityCarDestination]
    FOREIGN KEY ([CityId])
    REFERENCES [dbo].[Cities]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CityCarDestination'
CREATE INDEX [IX_FK_CityCarDestination]
ON [dbo].[CarDestinations]
    ([CityId]);
GO

-- Creating foreign key on [CarUnitId] in table 'CarRates'
ALTER TABLE [dbo].[CarRates]
ADD CONSTRAINT [FK_CarUnitCarRate]
    FOREIGN KEY ([CarUnitId])
    REFERENCES [dbo].[CarUnits]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CarUnitCarRate'
CREATE INDEX [IX_FK_CarUnitCarRate]
ON [dbo].[CarRates]
    ([CarUnitId]);
GO

-- Creating foreign key on [CarUnitId] in table 'CarReservations'
ALTER TABLE [dbo].[CarReservations]
ADD CONSTRAINT [FK_CarUnitCarReservation]
    FOREIGN KEY ([CarUnitId])
    REFERENCES [dbo].[CarUnits]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CarUnitCarReservation'
CREATE INDEX [IX_FK_CarUnitCarReservation]
ON [dbo].[CarReservations]
    ([CarUnitId]);
GO

-- Creating foreign key on [CarUnitId] in table 'CarImages'
ALTER TABLE [dbo].[CarImages]
ADD CONSTRAINT [FK_CarUnitCarImage]
    FOREIGN KEY ([CarUnitId])
    REFERENCES [dbo].[CarUnits]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CarUnitCarImage'
CREATE INDEX [IX_FK_CarUnitCarImage]
ON [dbo].[CarImages]
    ([CarUnitId]);
GO

-- Creating foreign key on [ProductId] in table 'ProductPrices'
ALTER TABLE [dbo].[ProductPrices]
ADD CONSTRAINT [FK_ProductProductPrice]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductProductPrice'
CREATE INDEX [IX_FK_ProductProductPrice]
ON [dbo].[ProductPrices]
    ([ProductId]);
GO

-- Creating foreign key on [ProductId] in table 'ProductImages1'
ALTER TABLE [dbo].[ProductImages1]
ADD CONSTRAINT [FK_ProductProductImages]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductProductImages'
CREATE INDEX [IX_FK_ProductProductImages]
ON [dbo].[ProductImages1]
    ([ProductId]);
GO

-- Creating foreign key on [ProductId] in table 'ProductConditions'
ALTER TABLE [dbo].[ProductConditions]
ADD CONSTRAINT [FK_ProductProductCondition]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductProductCondition'
CREATE INDEX [IX_FK_ProductProductCondition]
ON [dbo].[ProductConditions]
    ([ProductId]);
GO

-- Creating foreign key on [ProductCategoryId] in table 'ProductProdCats'
ALTER TABLE [dbo].[ProductProdCats]
ADD CONSTRAINT [FK_ProductCategoryProductProdCat]
    FOREIGN KEY ([ProductCategoryId])
    REFERENCES [dbo].[ProductCategories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductCategoryProductProdCat'
CREATE INDEX [IX_FK_ProductCategoryProductProdCat]
ON [dbo].[ProductProdCats]
    ([ProductCategoryId]);
GO

-- Creating foreign key on [ProductId] in table 'ProductProdCats'
ALTER TABLE [dbo].[ProductProdCats]
ADD CONSTRAINT [FK_ProductProductProdCat]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductProductProdCat'
CREATE INDEX [IX_FK_ProductProductProdCat]
ON [dbo].[ProductProdCats]
    ([ProductId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------