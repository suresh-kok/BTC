
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/29/2018 17:45:40
-- Generated from EDMX file: C:\Users\skanniyappan\Documents\GitHub\BTC\Travel_Request_System\Travel_Request_System_EF\Models\HRWorksEntities.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [HRWorks];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__TravelReq__ApprovalBy]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TravelRequests] DROP CONSTRAINT [FK__TravelReq__ApprovalBy];
GO
IF OBJECT_ID(N'[dbo].[FK__TravelReq_Currency]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TravelRequests] DROP CONSTRAINT [FK__TravelReq_Currency];
GO
IF OBJECT_ID(N'[dbo].[FK__TravelReq_PortOfD]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TravelRequests] DROP CONSTRAINT [FK__TravelReq_PortOfD];
GO
IF OBJECT_ID(N'[dbo].[FK__TravelReq_PortOfO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TravelRequests] DROP CONSTRAINT [FK__TravelReq_PortOfO];
GO
IF OBJECT_ID(N'[dbo].[FK__TravelReq_UserID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TravelRequests] DROP CONSTRAINT [FK__TravelReq_UserID];
GO
IF OBJECT_ID(N'[dbo].[FK__UserRoles__RoleI__6D6238AF]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRoles] DROP CONSTRAINT [FK__UserRoles__RoleI__6D6238AF];
GO
IF OBJECT_ID(N'[dbo].[FK__UserRoles__UserI__6E565CE8]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRoles] DROP CONSTRAINT [FK__UserRoles__UserI__6E565CE8];
GO
IF OBJECT_ID(N'[dbo].[fk_ATQuotation_DestinationID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ATQuotation] DROP CONSTRAINT [fk_ATQuotation_DestinationID];
GO
IF OBJECT_ID(N'[dbo].[fk_ATQuotation_OriginID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ATQuotation] DROP CONSTRAINT [fk_ATQuotation_OriginID];
GO
IF OBJECT_ID(N'[dbo].[fk_ATQuotation_QuotationID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ATQuotation] DROP CONSTRAINT [fk_ATQuotation_QuotationID];
GO
IF OBJECT_ID(N'[dbo].[FK_HRW_EmpEntityParamValues_HRW_EmpEntityParamValues]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HRW_EmpEntityParamValues] DROP CONSTRAINT [FK_HRW_EmpEntityParamValues_HRW_EmpEntityParamValues];
GO
IF OBJECT_ID(N'[dbo].[FK_HRW_EmpEntityParamValues_ORG_EntityMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HRW_EmpEntityParamValues] DROP CONSTRAINT [FK_HRW_EmpEntityParamValues_ORG_EntityMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_HRW_EmpEntityParamValues_ORG_EntityMaster1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HRW_EmpEntityParamValues] DROP CONSTRAINT [FK_HRW_EmpEntityParamValues_ORG_EntityMaster1];
GO
IF OBJECT_ID(N'[dbo].[FK_HRW_EmpEntityParamValues_ORG_EntityTypeParam]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HRW_EmpEntityParamValues] DROP CONSTRAINT [FK_HRW_EmpEntityParamValues_ORG_EntityTypeParam];
GO
IF OBJECT_ID(N'[dbo].[FK_HRW_Employee_HRW_Company]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HRW_Employee] DROP CONSTRAINT [FK_HRW_Employee_HRW_Company];
GO
IF OBJECT_ID(N'[dbo].[fk_HSQuotation_QuotationID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HSQuotation] DROP CONSTRAINT [fk_HSQuotation_QuotationID];
GO
IF OBJECT_ID(N'[dbo].[fk_LPO_ATQuotationID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LPO] DROP CONSTRAINT [fk_LPO_ATQuotationID];
GO
IF OBJECT_ID(N'[dbo].[fk_LPO_HSQuotationID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LPO] DROP CONSTRAINT [fk_LPO_HSQuotationID];
GO
IF OBJECT_ID(N'[dbo].[fk_LPO_PCQuotationID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LPO] DROP CONSTRAINT [fk_LPO_PCQuotationID];
GO
IF OBJECT_ID(N'[dbo].[fk_LPO_QuotationID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LPO] DROP CONSTRAINT [fk_LPO_QuotationID];
GO
IF OBJECT_ID(N'[dbo].[fk_LPO_RFQID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LPO] DROP CONSTRAINT [fk_LPO_RFQID];
GO
IF OBJECT_ID(N'[dbo].[FK_ORG_ChartMaster_HRW_Company]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ORG_ChartMaster] DROP CONSTRAINT [FK_ORG_ChartMaster_HRW_Company];
GO
IF OBJECT_ID(N'[dbo].[FK_ORG_EmpEntityLink_HRW_Employee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ORG_EmpEntityLink] DROP CONSTRAINT [FK_ORG_EmpEntityLink_HRW_Employee];
GO
IF OBJECT_ID(N'[dbo].[FK_ORG_EmpEntityLink_ORG_EntityMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ORG_EmpEntityLink] DROP CONSTRAINT [FK_ORG_EmpEntityLink_ORG_EntityMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_ORG_EntityMaster_ORG_EntityType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ORG_EntityMaster] DROP CONSTRAINT [FK_ORG_EntityMaster_ORG_EntityType];
GO
IF OBJECT_ID(N'[dbo].[FK_ORG_EntityType_ORG_ChartMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ORG_EntityType] DROP CONSTRAINT [FK_ORG_EntityType_ORG_ChartMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_ORG_EntityTypeParam_ORG_EntityType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ORG_EntityTypeParam] DROP CONSTRAINT [FK_ORG_EntityTypeParam_ORG_EntityType];
GO
IF OBJECT_ID(N'[dbo].[fk_PCQuotation_QuotationID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PCQuotation] DROP CONSTRAINT [fk_PCQuotation_QuotationID];
GO
IF OBJECT_ID(N'[HRWorksModelStoreContainer].[fk_PFQ_AttachmentID]', 'F') IS NOT NULL
    ALTER TABLE [HRWorksModelStoreContainer].[AttachmentsLinks] DROP CONSTRAINT [fk_PFQ_AttachmentID];
GO
IF OBJECT_ID(N'[dbo].[fk_PFQ_TravelAgencyID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RFQ] DROP CONSTRAINT [fk_PFQ_TravelAgencyID];
GO
IF OBJECT_ID(N'[dbo].[fk_PFQ_TravelRequestID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RFQ] DROP CONSTRAINT [fk_PFQ_TravelRequestID];
GO
IF OBJECT_ID(N'[dbo].[fk_PFQ_UserID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RFQ] DROP CONSTRAINT [fk_PFQ_UserID];
GO
IF OBJECT_ID(N'[dbo].[fk_Quotation_TravelAgencyID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Quotation] DROP CONSTRAINT [fk_Quotation_TravelAgencyID];
GO
IF OBJECT_ID(N'[dbo].[fk_Quotation_TravelRequestID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Quotation] DROP CONSTRAINT [fk_Quotation_TravelRequestID];
GO
IF OBJECT_ID(N'[dbo].[fk_Quotation_UserID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Quotation] DROP CONSTRAINT [fk_Quotation_UserID];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ATQuotation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ATQuotation];
GO
IF OBJECT_ID(N'[dbo].[Attachments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Attachments];
GO
IF OBJECT_ID(N'[dbo].[Cities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cities];
GO
IF OBJECT_ID(N'[dbo].[Currencies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Currencies];
GO
IF OBJECT_ID(N'[dbo].[HRW_Company]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HRW_Company];
GO
IF OBJECT_ID(N'[dbo].[HRW_EmpEntityParamValues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HRW_EmpEntityParamValues];
GO
IF OBJECT_ID(N'[dbo].[HRW_Employee]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HRW_Employee];
GO
IF OBJECT_ID(N'[dbo].[HSQuotation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HSQuotation];
GO
IF OBJECT_ID(N'[dbo].[LPO]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LPO];
GO
IF OBJECT_ID(N'[dbo].[ORG_ChartMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ORG_ChartMaster];
GO
IF OBJECT_ID(N'[dbo].[ORG_EmpEntityLink]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ORG_EmpEntityLink];
GO
IF OBJECT_ID(N'[dbo].[ORG_EntityMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ORG_EntityMaster];
GO
IF OBJECT_ID(N'[dbo].[ORG_EntityType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ORG_EntityType];
GO
IF OBJECT_ID(N'[dbo].[ORG_EntityTypeParam]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ORG_EntityTypeParam];
GO
IF OBJECT_ID(N'[dbo].[PCQuotation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PCQuotation];
GO
IF OBJECT_ID(N'[dbo].[Quotation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Quotation];
GO
IF OBJECT_ID(N'[dbo].[RFQ]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RFQ];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[TravelAgency]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TravelAgency];
GO
IF OBJECT_ID(N'[dbo].[TravelRequests]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TravelRequests];
GO
IF OBJECT_ID(N'[dbo].[UserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserRoles];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[HRWorksModelStoreContainer].[AttachmentsLinks]', 'U') IS NOT NULL
    DROP TABLE [HRWorksModelStoreContainer].[AttachmentsLinks];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ATQuotations'
CREATE TABLE [dbo].[ATQuotations] (
    [ATQuotationID] int IDENTITY(1,1) NOT NULL,
    [QuotationID] int  NOT NULL,
    [TicketClass] nvarchar(100)  NULL,
    [OriginID] int  NULL,
    [DestinationID] int  NULL,
    [DepartureDate] datetime  NULL,
    [DepartureTime] time  NULL,
    [ReturnDate] datetime  NULL,
    [ReturnTime] time  NULL,
    [Airlines] nvarchar(100)  NULL,
    [TicketNo] nvarchar(100)  NULL,
    [Amount] decimal(10,2)  NULL,
    [IsDeleted] bit  NULL
);
GO

-- Creating table 'Attachments'
CREATE TABLE [dbo].[Attachments] (
    [AttachmentID] int IDENTITY(1,1) NOT NULL,
    [FilePath] nvarchar(250)  NOT NULL,
    [FileType] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Cities'
CREATE TABLE [dbo].[Cities] (
    [CityID] int  NOT NULL,
    [CityDesc] nvarchar(100)  NOT NULL,
    [IsActive] bit  NULL
);
GO

-- Creating table 'Currencies'
CREATE TABLE [dbo].[Currencies] (
    [CurrencyID] int  NOT NULL,
    [CurrencyDesc] nvarchar(100)  NOT NULL,
    [IsActive] bit  NULL
);
GO

-- Creating table 'HRW_Company'
CREATE TABLE [dbo].[HRW_Company] (
    [TransactType] int  NOT NULL,
    [TransactUserID] bigint  NOT NULL,
    [TransactDateTime] datetime  NOT NULL,
    [MenuID] varchar(50)  NOT NULL,
    [CompanyCode] varchar(10)  NOT NULL,
    [DisplayName] nvarchar(50)  NULL,
    [ReportName] nvarchar(50)  NULL,
    [Address] nvarchar(1000)  NULL
);
GO

-- Creating table 'HRW_EmpEntityParamValues'
CREATE TABLE [dbo].[HRW_EmpEntityParamValues] (
    [TransactType] int  NOT NULL,
    [TransactUserID] bigint  NOT NULL,
    [TransactDateTime] datetime  NOT NULL,
    [MenuID] varchar(50)  NOT NULL,
    [EmpEntityParamID] bigint IDENTITY(1,1) NOT NULL,
    [EmployeeId] bigint  NULL,
    [EntityID] bigint  NULL,
    [EntityTypeParamID] bigint  NOT NULL,
    [ParamValue] nvarchar(500)  NULL,
    [ParamValueEntityID] bigint  NULL,
    [EmpEntityParamIDLink] bigint  NULL,
    [ESSID] bigint  NULL,
    [CompanyCode] varchar(10)  NULL
);
GO

-- Creating table 'HRW_Employee'
CREATE TABLE [dbo].[HRW_Employee] (
    [TransactType] int  NOT NULL,
    [TransactUserID] bigint  NOT NULL,
    [TransactDateTime] datetime  NOT NULL,
    [MenuID] varchar(50)  NOT NULL,
    [EmployeeID] bigint IDENTITY(1,1) NOT NULL,
    [CompanyCode] varchar(10)  NOT NULL,
    [EmployeeCode] varchar(20)  NOT NULL,
    [FirstName] nvarchar(50)  NULL,
    [MiddleName] nvarchar(50)  NULL,
    [LastName] nvarchar(50)  NULL,
    [FullName] nvarchar(150)  NULL,
    [Salutation] int  NULL,
    [DateOfBirth] datetime  NULL,
    [HireDate] datetime  NULL,
    [TerminationDate] datetime  NULL,
    [PrevEmpID] varchar(20)  NULL,
    [Remarks] nvarchar(100)  NULL,
    [RecordType] varchar(5)  NOT NULL,
    [PAFRecordID] bigint  NULL,
    [EmpStatus] varchar(100)  NULL
);
GO

-- Creating table 'HSQuotations'
CREATE TABLE [dbo].[HSQuotations] (
    [HSQuotationID] int IDENTITY(1,1) NOT NULL,
    [QuotationID] int  NOT NULL,
    [TravelSector] nvarchar(250)  NULL,
    [HotelName] nvarchar(250)  NULL,
    [HotelCategory] nvarchar(100)  NULL,
    [RoomCategory] nvarchar(100)  NULL,
    [RoomType] nvarchar(100)  NULL,
    [CheckInDate] datetime  NULL,
    [CheckInTime] time  NULL,
    [CheckOutDate] datetime  NULL,
    [CheckOutTime] time  NULL,
    [Amount] decimal(10,2)  NULL,
    [IsDeleted] bit  NULL
);
GO

-- Creating table 'LPOes'
CREATE TABLE [dbo].[LPOes] (
    [LPOID] int IDENTITY(1,1) NOT NULL,
    [LPONo] nvarchar(100)  NULL,
    [RFQID] int  NOT NULL,
    [QuotationID] int  NOT NULL,
    [ATQuotationID] int  NOT NULL,
    [HSQuotationID] int  NOT NULL,
    [PCQuotationID] int  NOT NULL,
    [Remarks] nvarchar(500)  NULL,
    [IsDeleted] bit  NULL
);
GO

-- Creating table 'ORG_ChartMaster'
CREATE TABLE [dbo].[ORG_ChartMaster] (
    [TransactType] int  NOT NULL,
    [TransactUserID] bigint  NOT NULL,
    [TransactDateTime] datetime  NOT NULL,
    [MenuID] varchar(50)  NOT NULL,
    [OrgChartId] bigint IDENTITY(1,1) NOT NULL,
    [CompanyCode] varchar(10)  NOT NULL,
    [Description] nvarchar(50)  NOT NULL,
    [ChartRecordType] varchar(1)  NULL,
    [ManagerEmployeeID] bigint  NULL,
    [IsChartDataReady] bit  NULL,
    [OrgChartBy] bigint  NULL
);
GO

-- Creating table 'ORG_EmpEntityLink'
CREATE TABLE [dbo].[ORG_EmpEntityLink] (
    [TransactType] int  NOT NULL,
    [TransactUserID] bigint  NOT NULL,
    [TransactDateTime] datetime  NOT NULL,
    [MenuID] varchar(50)  NOT NULL,
    [EmpEntityId] bigint IDENTITY(1,1) NOT NULL,
    [EmployeeId] bigint  NOT NULL,
    [EntityId] bigint  NOT NULL,
    [EffectiveFrom] datetime  NOT NULL,
    [EffectiveTo] datetime  NULL,
    [RecordStatus] varchar(50)  NOT NULL
);
GO

-- Creating table 'ORG_EntityMaster'
CREATE TABLE [dbo].[ORG_EntityMaster] (
    [TransactType] int  NOT NULL,
    [TransactUserID] bigint  NULL,
    [TransactDateTime] datetime  NOT NULL,
    [MenuID] varchar(50)  NOT NULL,
    [EntityId] bigint IDENTITY(1,1) NOT NULL,
    [EntityTypeId] bigint  NULL,
    [EntityCode] varchar(20)  NULL,
    [Description] nvarchar(200)  NOT NULL,
    [Parent] bigint  NULL,
    [EffectiveFrom] datetime  NULL,
    [EffectiveTo] datetime  NULL,
    [IsActive] bit  NOT NULL,
    [IncludeEmployees] bit  NOT NULL,
    [SortOrder] int  NOT NULL
);
GO

-- Creating table 'ORG_EntityType'
CREATE TABLE [dbo].[ORG_EntityType] (
    [TransactType] int  NOT NULL,
    [TransactUserID] bigint  NOT NULL,
    [TransactDateTime] datetime  NOT NULL,
    [MenuID] varchar(50)  NOT NULL,
    [EntityTypeId] bigint IDENTITY(1,1) NOT NULL,
    [OrgChartId] bigint  NOT NULL,
    [Description] nvarchar(50)  NOT NULL,
    [IsMandatory] bit  NOT NULL,
    [IsCodeRequired] bit  NOT NULL,
    [SortOrder] int  NOT NULL,
    [EntityGroup] int  NOT NULL,
    [MasterEntityTypeId] bigint  NULL,
    [HaveDataSecurity] bit  NOT NULL,
    [ShowInReportFilter] bit  NOT NULL,
    [RestrictDuplicate] bit  NOT NULL
);
GO

-- Creating table 'ORG_EntityTypeParam'
CREATE TABLE [dbo].[ORG_EntityTypeParam] (
    [TransactType] int  NOT NULL,
    [TransactUserID] bigint  NOT NULL,
    [TransactDateTime] datetime  NOT NULL,
    [MenuID] varchar(50)  NOT NULL,
    [EntityTypeParamID] bigint IDENTITY(1,1) NOT NULL,
    [EntityTypeId] bigint  NOT NULL,
    [Description] nvarchar(500)  NULL,
    [DataType] varchar(30)  NULL,
    [DataTypeEntityID] bigint  NULL,
    [SortOrder] int  NOT NULL,
    [ParamLinkLevel] varchar(10)  NOT NULL,
    [IsMandatory] bit  NOT NULL,
    [CheckDuplicate] int  NULL,
    [Validations] varchar(50)  NULL,
    [ESSFieldAccessRight] varchar(1)  NULL,
    [DataTypeProperty] varchar(50)  NULL,
    [DataTypeProperty1] varchar(300)  NULL
);
GO

-- Creating table 'PCQuotations'
CREATE TABLE [dbo].[PCQuotations] (
    [PCQuotationID] int IDENTITY(1,1) NOT NULL,
    [QuotationID] int  NOT NULL,
    [TravelSector] nvarchar(250)  NULL,
    [PickupLocation] nvarchar(250)  NULL,
    [DropoffLocation] nvarchar(250)  NULL,
    [PreferredVehicle] nvarchar(250)  NULL,
    [PickUpDate] datetime  NULL,
    [PickUpTime] time  NULL,
    [DropOffDate] datetime  NULL,
    [DropOffTime] time  NULL,
    [Amount] decimal(10,2)  NULL,
    [IsDeleted] bit  NULL
);
GO

-- Creating table 'Quotations'
CREATE TABLE [dbo].[Quotations] (
    [QuotationID] int IDENTITY(1,1) NOT NULL,
    [TravelAgencyID] int  NOT NULL,
    [TravelRequestID] int  NOT NULL,
    [UserID] int  NOT NULL,
    [Remarks] nvarchar(max)  NULL,
    [IsDeleted] bit  NULL
);
GO

-- Creating table 'RFQs'
CREATE TABLE [dbo].[RFQs] (
    [RFQID] int IDENTITY(1,1) NOT NULL,
    [TravelAgencyID] int  NOT NULL,
    [TravelRequestID] int  NOT NULL,
    [UserID] int  NOT NULL,
    [Remarks] nvarchar(max)  NULL,
    [Processing] int  NOT NULL,
    [ProcessingSection] int  NOT NULL,
    [IsDeleted] bit  NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [RoleId] int IDENTITY(1,1) NOT NULL,
    [RoleName] nvarchar(max)  NOT NULL,
    [IsDeleted] bit  NULL
);
GO

-- Creating table 'TravelAgencies'
CREATE TABLE [dbo].[TravelAgencies] (
    [AgencyID] int IDENTITY(1,1) NOT NULL,
    [AgencyCode] nvarchar(25)  NULL,
    [CompanyName] nvarchar(100)  NULL,
    [Address] varchar(max)  NULL,
    [Telephone] nvarchar(20)  NULL,
    [Fax] nvarchar(20)  NULL,
    [Mobile] nvarchar(20)  NULL,
    [Landline] nvarchar(20)  NULL,
    [ContactPerson] nvarchar(100)  NULL,
    [Email] nvarchar(100)  NULL
);
GO

-- Creating table 'TravelRequests'
CREATE TABLE [dbo].[TravelRequests] (
    [TravelRequestID] int  NOT NULL,
    [UserID] int  NOT NULL,
    [ApplicationNumber] nvarchar(50)  NOT NULL,
    [PortOfOriginID] int  NULL,
    [PortOfDestinationID] int  NULL,
    [TicketClass] nvarchar(100)  NULL,
    [DailyAllowance] decimal(10,2)  NULL,
    [CurrencyID] int  NULL,
    [TravelDays] int  NULL,
    [TravelRemarks] nvarchar(max)  NULL,
    [PurposeOfVisit] nvarchar(max)  NULL,
    [DepartureDate] datetime  NULL,
    [DepartureTime] time  NULL,
    [ReturnDate] datetime  NULL,
    [ReturnTime] time  NULL,
    [FirstBusinessDay] datetime  NULL,
    [LastBusinessDay] datetime  NULL,
    [Remarks] nvarchar(500)  NULL,
    [AirTicketManagement] nvarchar(50)  NULL,
    [HotelName] nvarchar(50)  NULL,
    [TravelAllowance] nvarchar(50)  NULL,
    [HotelStay] nvarchar(50)  NULL,
    [HotelCategory] nvarchar(50)  NULL,
    [RoomCategory] nvarchar(50)  NULL,
    [RoomType] nvarchar(50)  NULL,
    [AdditionalAllowance] decimal(10,2)  NULL,
    [AirportPickUp] nvarchar(50)  NULL,
    [PickUpLocation] nvarchar(500)  NULL,
    [PickUpDate] datetime  NULL,
    [PickUpTime] time  NULL,
    [DropOffLocation] nvarchar(500)  NULL,
    [DropOffDate] datetime  NULL,
    [DropOffTime] time  NULL,
    [PreferredVehicle] nvarchar(100)  NULL,
    [CheckInDate] datetime  NULL,
    [CheckOutDate] datetime  NULL,
    [CheckInTime] time  NULL,
    [CheckOutTime] time  NULL,
    [ApprovalLevel] int  NOT NULL,
    [ApprovalBy] int  NULL,
    [ApprovalRemarks] varchar(max)  NULL,
    [CreateOn] datetime  NULL,
    [CreatedBy] int  NULL,
    [ModifiedOn] datetime  NULL,
    [ModifiedBy] int  NULL,
    [IsDeleted] bit  NULL,
    [IsSubmitted] bit  NOT NULL,
    [CreatedByUser_UserId] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [ActivationCode] uniqueidentifier  NOT NULL,
    [HREmployeeID] int  NULL,
    [IsDeleted] bit  NULL
);
GO

-- Creating table 'AttachmentsLinks'
CREATE TABLE [dbo].[AttachmentsLinks] (
    [AttachmentID] int  NOT NULL,
    [AttachmentFor] nvarchar(100)  NOT NULL,
    [AttachmentForID] int  NOT NULL
);
GO

-- Creating table 'UserRoles'
CREATE TABLE [dbo].[UserRoles] (
    [Roles_RoleId] int  NOT NULL,
    [Users_UserId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ATQuotationID] in table 'ATQuotations'
ALTER TABLE [dbo].[ATQuotations]
ADD CONSTRAINT [PK_ATQuotations]
    PRIMARY KEY CLUSTERED ([ATQuotationID] ASC);
GO

-- Creating primary key on [AttachmentID] in table 'Attachments'
ALTER TABLE [dbo].[Attachments]
ADD CONSTRAINT [PK_Attachments]
    PRIMARY KEY CLUSTERED ([AttachmentID] ASC);
GO

-- Creating primary key on [CityID] in table 'Cities'
ALTER TABLE [dbo].[Cities]
ADD CONSTRAINT [PK_Cities]
    PRIMARY KEY CLUSTERED ([CityID] ASC);
GO

-- Creating primary key on [CurrencyID] in table 'Currencies'
ALTER TABLE [dbo].[Currencies]
ADD CONSTRAINT [PK_Currencies]
    PRIMARY KEY CLUSTERED ([CurrencyID] ASC);
GO

-- Creating primary key on [CompanyCode] in table 'HRW_Company'
ALTER TABLE [dbo].[HRW_Company]
ADD CONSTRAINT [PK_HRW_Company]
    PRIMARY KEY CLUSTERED ([CompanyCode] ASC);
GO

-- Creating primary key on [EmpEntityParamID] in table 'HRW_EmpEntityParamValues'
ALTER TABLE [dbo].[HRW_EmpEntityParamValues]
ADD CONSTRAINT [PK_HRW_EmpEntityParamValues]
    PRIMARY KEY CLUSTERED ([EmpEntityParamID] ASC);
GO

-- Creating primary key on [EmployeeID] in table 'HRW_Employee'
ALTER TABLE [dbo].[HRW_Employee]
ADD CONSTRAINT [PK_HRW_Employee]
    PRIMARY KEY CLUSTERED ([EmployeeID] ASC);
GO

-- Creating primary key on [HSQuotationID] in table 'HSQuotations'
ALTER TABLE [dbo].[HSQuotations]
ADD CONSTRAINT [PK_HSQuotations]
    PRIMARY KEY CLUSTERED ([HSQuotationID] ASC);
GO

-- Creating primary key on [LPOID] in table 'LPOes'
ALTER TABLE [dbo].[LPOes]
ADD CONSTRAINT [PK_LPOes]
    PRIMARY KEY CLUSTERED ([LPOID] ASC);
GO

-- Creating primary key on [OrgChartId] in table 'ORG_ChartMaster'
ALTER TABLE [dbo].[ORG_ChartMaster]
ADD CONSTRAINT [PK_ORG_ChartMaster]
    PRIMARY KEY CLUSTERED ([OrgChartId] ASC);
GO

-- Creating primary key on [EmpEntityId] in table 'ORG_EmpEntityLink'
ALTER TABLE [dbo].[ORG_EmpEntityLink]
ADD CONSTRAINT [PK_ORG_EmpEntityLink]
    PRIMARY KEY CLUSTERED ([EmpEntityId] ASC);
GO

-- Creating primary key on [EntityId] in table 'ORG_EntityMaster'
ALTER TABLE [dbo].[ORG_EntityMaster]
ADD CONSTRAINT [PK_ORG_EntityMaster]
    PRIMARY KEY CLUSTERED ([EntityId] ASC);
GO

-- Creating primary key on [EntityTypeId] in table 'ORG_EntityType'
ALTER TABLE [dbo].[ORG_EntityType]
ADD CONSTRAINT [PK_ORG_EntityType]
    PRIMARY KEY CLUSTERED ([EntityTypeId] ASC);
GO

-- Creating primary key on [EntityTypeParamID] in table 'ORG_EntityTypeParam'
ALTER TABLE [dbo].[ORG_EntityTypeParam]
ADD CONSTRAINT [PK_ORG_EntityTypeParam]
    PRIMARY KEY CLUSTERED ([EntityTypeParamID] ASC);
GO

-- Creating primary key on [PCQuotationID] in table 'PCQuotations'
ALTER TABLE [dbo].[PCQuotations]
ADD CONSTRAINT [PK_PCQuotations]
    PRIMARY KEY CLUSTERED ([PCQuotationID] ASC);
GO

-- Creating primary key on [QuotationID] in table 'Quotations'
ALTER TABLE [dbo].[Quotations]
ADD CONSTRAINT [PK_Quotations]
    PRIMARY KEY CLUSTERED ([QuotationID] ASC);
GO

-- Creating primary key on [RFQID] in table 'RFQs'
ALTER TABLE [dbo].[RFQs]
ADD CONSTRAINT [PK_RFQs]
    PRIMARY KEY CLUSTERED ([RFQID] ASC);
GO

-- Creating primary key on [RoleId] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([RoleId] ASC);
GO

-- Creating primary key on [AgencyID] in table 'TravelAgencies'
ALTER TABLE [dbo].[TravelAgencies]
ADD CONSTRAINT [PK_TravelAgencies]
    PRIMARY KEY CLUSTERED ([AgencyID] ASC);
GO

-- Creating primary key on [TravelRequestID] in table 'TravelRequests'
ALTER TABLE [dbo].[TravelRequests]
ADD CONSTRAINT [PK_TravelRequests]
    PRIMARY KEY CLUSTERED ([TravelRequestID] ASC);
GO

-- Creating primary key on [UserId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [AttachmentID], [AttachmentFor], [AttachmentForID] in table 'AttachmentsLinks'
ALTER TABLE [dbo].[AttachmentsLinks]
ADD CONSTRAINT [PK_AttachmentsLinks]
    PRIMARY KEY CLUSTERED ([AttachmentID], [AttachmentFor], [AttachmentForID] ASC);
GO

-- Creating primary key on [Roles_RoleId], [Users_UserId] in table 'UserRoles'
ALTER TABLE [dbo].[UserRoles]
ADD CONSTRAINT [PK_UserRoles]
    PRIMARY KEY CLUSTERED ([Roles_RoleId], [Users_UserId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [DestinationID] in table 'ATQuotations'
ALTER TABLE [dbo].[ATQuotations]
ADD CONSTRAINT [fk_ATQuotation_DestinationID]
    FOREIGN KEY ([DestinationID])
    REFERENCES [dbo].[Cities]
        ([CityID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_ATQuotation_DestinationID'
CREATE INDEX [IX_fk_ATQuotation_DestinationID]
ON [dbo].[ATQuotations]
    ([DestinationID]);
GO

-- Creating foreign key on [OriginID] in table 'ATQuotations'
ALTER TABLE [dbo].[ATQuotations]
ADD CONSTRAINT [fk_ATQuotation_OriginID]
    FOREIGN KEY ([OriginID])
    REFERENCES [dbo].[Cities]
        ([CityID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_ATQuotation_OriginID'
CREATE INDEX [IX_fk_ATQuotation_OriginID]
ON [dbo].[ATQuotations]
    ([OriginID]);
GO

-- Creating foreign key on [QuotationID] in table 'ATQuotations'
ALTER TABLE [dbo].[ATQuotations]
ADD CONSTRAINT [fk_ATQuotation_QuotationID]
    FOREIGN KEY ([QuotationID])
    REFERENCES [dbo].[Quotations]
        ([QuotationID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_ATQuotation_QuotationID'
CREATE INDEX [IX_fk_ATQuotation_QuotationID]
ON [dbo].[ATQuotations]
    ([QuotationID]);
GO

-- Creating foreign key on [ATQuotationID] in table 'LPOes'
ALTER TABLE [dbo].[LPOes]
ADD CONSTRAINT [fk_LPO_ATQuotationID]
    FOREIGN KEY ([ATQuotationID])
    REFERENCES [dbo].[ATQuotations]
        ([ATQuotationID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_LPO_ATQuotationID'
CREATE INDEX [IX_fk_LPO_ATQuotationID]
ON [dbo].[LPOes]
    ([ATQuotationID]);
GO

-- Creating foreign key on [AttachmentID] in table 'AttachmentsLinks'
ALTER TABLE [dbo].[AttachmentsLinks]
ADD CONSTRAINT [fk_PFQ_AttachmentID]
    FOREIGN KEY ([AttachmentID])
    REFERENCES [dbo].[Attachments]
        ([AttachmentID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [PortOfDestinationID] in table 'TravelRequests'
ALTER TABLE [dbo].[TravelRequests]
ADD CONSTRAINT [FK__TravelReq_PortOfD]
    FOREIGN KEY ([PortOfDestinationID])
    REFERENCES [dbo].[Cities]
        ([CityID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__TravelReq_PortOfD'
CREATE INDEX [IX_FK__TravelReq_PortOfD]
ON [dbo].[TravelRequests]
    ([PortOfDestinationID]);
GO

-- Creating foreign key on [PortOfOriginID] in table 'TravelRequests'
ALTER TABLE [dbo].[TravelRequests]
ADD CONSTRAINT [FK__TravelReq_PortOfO]
    FOREIGN KEY ([PortOfOriginID])
    REFERENCES [dbo].[Cities]
        ([CityID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__TravelReq_PortOfO'
CREATE INDEX [IX_FK__TravelReq_PortOfO]
ON [dbo].[TravelRequests]
    ([PortOfOriginID]);
GO

-- Creating foreign key on [CurrencyID] in table 'TravelRequests'
ALTER TABLE [dbo].[TravelRequests]
ADD CONSTRAINT [FK__TravelReq_Currency]
    FOREIGN KEY ([CurrencyID])
    REFERENCES [dbo].[Currencies]
        ([CurrencyID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__TravelReq_Currency'
CREATE INDEX [IX_FK__TravelReq_Currency]
ON [dbo].[TravelRequests]
    ([CurrencyID]);
GO

-- Creating foreign key on [CompanyCode] in table 'HRW_Employee'
ALTER TABLE [dbo].[HRW_Employee]
ADD CONSTRAINT [FK_HRW_Employee_HRW_Company]
    FOREIGN KEY ([CompanyCode])
    REFERENCES [dbo].[HRW_Company]
        ([CompanyCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HRW_Employee_HRW_Company'
CREATE INDEX [IX_FK_HRW_Employee_HRW_Company]
ON [dbo].[HRW_Employee]
    ([CompanyCode]);
GO

-- Creating foreign key on [CompanyCode] in table 'ORG_ChartMaster'
ALTER TABLE [dbo].[ORG_ChartMaster]
ADD CONSTRAINT [FK_ORG_ChartMaster_HRW_Company]
    FOREIGN KEY ([CompanyCode])
    REFERENCES [dbo].[HRW_Company]
        ([CompanyCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ORG_ChartMaster_HRW_Company'
CREATE INDEX [IX_FK_ORG_ChartMaster_HRW_Company]
ON [dbo].[ORG_ChartMaster]
    ([CompanyCode]);
GO

-- Creating foreign key on [EmpEntityParamIDLink] in table 'HRW_EmpEntityParamValues'
ALTER TABLE [dbo].[HRW_EmpEntityParamValues]
ADD CONSTRAINT [FK_HRW_EmpEntityParamValues_HRW_EmpEntityParamValues]
    FOREIGN KEY ([EmpEntityParamIDLink])
    REFERENCES [dbo].[HRW_EmpEntityParamValues]
        ([EmpEntityParamID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HRW_EmpEntityParamValues_HRW_EmpEntityParamValues'
CREATE INDEX [IX_FK_HRW_EmpEntityParamValues_HRW_EmpEntityParamValues]
ON [dbo].[HRW_EmpEntityParamValues]
    ([EmpEntityParamIDLink]);
GO

-- Creating foreign key on [EntityID] in table 'HRW_EmpEntityParamValues'
ALTER TABLE [dbo].[HRW_EmpEntityParamValues]
ADD CONSTRAINT [FK_HRW_EmpEntityParamValues_ORG_EntityMaster]
    FOREIGN KEY ([EntityID])
    REFERENCES [dbo].[ORG_EntityMaster]
        ([EntityId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HRW_EmpEntityParamValues_ORG_EntityMaster'
CREATE INDEX [IX_FK_HRW_EmpEntityParamValues_ORG_EntityMaster]
ON [dbo].[HRW_EmpEntityParamValues]
    ([EntityID]);
GO

-- Creating foreign key on [ParamValueEntityID] in table 'HRW_EmpEntityParamValues'
ALTER TABLE [dbo].[HRW_EmpEntityParamValues]
ADD CONSTRAINT [FK_HRW_EmpEntityParamValues_ORG_EntityMaster1]
    FOREIGN KEY ([ParamValueEntityID])
    REFERENCES [dbo].[ORG_EntityMaster]
        ([EntityId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HRW_EmpEntityParamValues_ORG_EntityMaster1'
CREATE INDEX [IX_FK_HRW_EmpEntityParamValues_ORG_EntityMaster1]
ON [dbo].[HRW_EmpEntityParamValues]
    ([ParamValueEntityID]);
GO

-- Creating foreign key on [EntityTypeParamID] in table 'HRW_EmpEntityParamValues'
ALTER TABLE [dbo].[HRW_EmpEntityParamValues]
ADD CONSTRAINT [FK_HRW_EmpEntityParamValues_ORG_EntityTypeParam]
    FOREIGN KEY ([EntityTypeParamID])
    REFERENCES [dbo].[ORG_EntityTypeParam]
        ([EntityTypeParamID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HRW_EmpEntityParamValues_ORG_EntityTypeParam'
CREATE INDEX [IX_FK_HRW_EmpEntityParamValues_ORG_EntityTypeParam]
ON [dbo].[HRW_EmpEntityParamValues]
    ([EntityTypeParamID]);
GO

-- Creating foreign key on [EmployeeId] in table 'ORG_EmpEntityLink'
ALTER TABLE [dbo].[ORG_EmpEntityLink]
ADD CONSTRAINT [FK_ORG_EmpEntityLink_HRW_Employee]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[HRW_Employee]
        ([EmployeeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ORG_EmpEntityLink_HRW_Employee'
CREATE INDEX [IX_FK_ORG_EmpEntityLink_HRW_Employee]
ON [dbo].[ORG_EmpEntityLink]
    ([EmployeeId]);
GO

-- Creating foreign key on [QuotationID] in table 'HSQuotations'
ALTER TABLE [dbo].[HSQuotations]
ADD CONSTRAINT [fk_HSQuotation_QuotationID]
    FOREIGN KEY ([QuotationID])
    REFERENCES [dbo].[Quotations]
        ([QuotationID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_HSQuotation_QuotationID'
CREATE INDEX [IX_fk_HSQuotation_QuotationID]
ON [dbo].[HSQuotations]
    ([QuotationID]);
GO

-- Creating foreign key on [HSQuotationID] in table 'LPOes'
ALTER TABLE [dbo].[LPOes]
ADD CONSTRAINT [fk_LPO_HSQuotationID]
    FOREIGN KEY ([HSQuotationID])
    REFERENCES [dbo].[HSQuotations]
        ([HSQuotationID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_LPO_HSQuotationID'
CREATE INDEX [IX_fk_LPO_HSQuotationID]
ON [dbo].[LPOes]
    ([HSQuotationID]);
GO

-- Creating foreign key on [PCQuotationID] in table 'LPOes'
ALTER TABLE [dbo].[LPOes]
ADD CONSTRAINT [fk_LPO_PCQuotationID]
    FOREIGN KEY ([PCQuotationID])
    REFERENCES [dbo].[PCQuotations]
        ([PCQuotationID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_LPO_PCQuotationID'
CREATE INDEX [IX_fk_LPO_PCQuotationID]
ON [dbo].[LPOes]
    ([PCQuotationID]);
GO

-- Creating foreign key on [QuotationID] in table 'LPOes'
ALTER TABLE [dbo].[LPOes]
ADD CONSTRAINT [fk_LPO_QuotationID]
    FOREIGN KEY ([QuotationID])
    REFERENCES [dbo].[Quotations]
        ([QuotationID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_LPO_QuotationID'
CREATE INDEX [IX_fk_LPO_QuotationID]
ON [dbo].[LPOes]
    ([QuotationID]);
GO

-- Creating foreign key on [RFQID] in table 'LPOes'
ALTER TABLE [dbo].[LPOes]
ADD CONSTRAINT [fk_LPO_RFQID]
    FOREIGN KEY ([RFQID])
    REFERENCES [dbo].[RFQs]
        ([RFQID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_LPO_RFQID'
CREATE INDEX [IX_fk_LPO_RFQID]
ON [dbo].[LPOes]
    ([RFQID]);
GO

-- Creating foreign key on [OrgChartId] in table 'ORG_EntityType'
ALTER TABLE [dbo].[ORG_EntityType]
ADD CONSTRAINT [FK_ORG_EntityType_ORG_ChartMaster]
    FOREIGN KEY ([OrgChartId])
    REFERENCES [dbo].[ORG_ChartMaster]
        ([OrgChartId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ORG_EntityType_ORG_ChartMaster'
CREATE INDEX [IX_FK_ORG_EntityType_ORG_ChartMaster]
ON [dbo].[ORG_EntityType]
    ([OrgChartId]);
GO

-- Creating foreign key on [EntityId] in table 'ORG_EmpEntityLink'
ALTER TABLE [dbo].[ORG_EmpEntityLink]
ADD CONSTRAINT [FK_ORG_EmpEntityLink_ORG_EntityMaster]
    FOREIGN KEY ([EntityId])
    REFERENCES [dbo].[ORG_EntityMaster]
        ([EntityId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ORG_EmpEntityLink_ORG_EntityMaster'
CREATE INDEX [IX_FK_ORG_EmpEntityLink_ORG_EntityMaster]
ON [dbo].[ORG_EmpEntityLink]
    ([EntityId]);
GO

-- Creating foreign key on [EntityTypeId] in table 'ORG_EntityMaster'
ALTER TABLE [dbo].[ORG_EntityMaster]
ADD CONSTRAINT [FK_ORG_EntityMaster_ORG_EntityType]
    FOREIGN KEY ([EntityTypeId])
    REFERENCES [dbo].[ORG_EntityType]
        ([EntityTypeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ORG_EntityMaster_ORG_EntityType'
CREATE INDEX [IX_FK_ORG_EntityMaster_ORG_EntityType]
ON [dbo].[ORG_EntityMaster]
    ([EntityTypeId]);
GO

-- Creating foreign key on [EntityTypeId] in table 'ORG_EntityTypeParam'
ALTER TABLE [dbo].[ORG_EntityTypeParam]
ADD CONSTRAINT [FK_ORG_EntityTypeParam_ORG_EntityType]
    FOREIGN KEY ([EntityTypeId])
    REFERENCES [dbo].[ORG_EntityType]
        ([EntityTypeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ORG_EntityTypeParam_ORG_EntityType'
CREATE INDEX [IX_FK_ORG_EntityTypeParam_ORG_EntityType]
ON [dbo].[ORG_EntityTypeParam]
    ([EntityTypeId]);
GO

-- Creating foreign key on [QuotationID] in table 'PCQuotations'
ALTER TABLE [dbo].[PCQuotations]
ADD CONSTRAINT [fk_PCQuotation_QuotationID]
    FOREIGN KEY ([QuotationID])
    REFERENCES [dbo].[Quotations]
        ([QuotationID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_PCQuotation_QuotationID'
CREATE INDEX [IX_fk_PCQuotation_QuotationID]
ON [dbo].[PCQuotations]
    ([QuotationID]);
GO

-- Creating foreign key on [TravelAgencyID] in table 'Quotations'
ALTER TABLE [dbo].[Quotations]
ADD CONSTRAINT [fk_Quotation_TravelAgencyID]
    FOREIGN KEY ([TravelAgencyID])
    REFERENCES [dbo].[TravelAgencies]
        ([AgencyID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_Quotation_TravelAgencyID'
CREATE INDEX [IX_fk_Quotation_TravelAgencyID]
ON [dbo].[Quotations]
    ([TravelAgencyID]);
GO

-- Creating foreign key on [TravelRequestID] in table 'Quotations'
ALTER TABLE [dbo].[Quotations]
ADD CONSTRAINT [fk_Quotation_TravelRequestID]
    FOREIGN KEY ([TravelRequestID])
    REFERENCES [dbo].[TravelRequests]
        ([TravelRequestID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_Quotation_TravelRequestID'
CREATE INDEX [IX_fk_Quotation_TravelRequestID]
ON [dbo].[Quotations]
    ([TravelRequestID]);
GO

-- Creating foreign key on [UserID] in table 'Quotations'
ALTER TABLE [dbo].[Quotations]
ADD CONSTRAINT [fk_Quotation_UserID]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_Quotation_UserID'
CREATE INDEX [IX_fk_Quotation_UserID]
ON [dbo].[Quotations]
    ([UserID]);
GO

-- Creating foreign key on [TravelAgencyID] in table 'RFQs'
ALTER TABLE [dbo].[RFQs]
ADD CONSTRAINT [fk_PFQ_TravelAgencyID]
    FOREIGN KEY ([TravelAgencyID])
    REFERENCES [dbo].[TravelAgencies]
        ([AgencyID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_PFQ_TravelAgencyID'
CREATE INDEX [IX_fk_PFQ_TravelAgencyID]
ON [dbo].[RFQs]
    ([TravelAgencyID]);
GO

-- Creating foreign key on [TravelRequestID] in table 'RFQs'
ALTER TABLE [dbo].[RFQs]
ADD CONSTRAINT [fk_PFQ_TravelRequestID]
    FOREIGN KEY ([TravelRequestID])
    REFERENCES [dbo].[TravelRequests]
        ([TravelRequestID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_PFQ_TravelRequestID'
CREATE INDEX [IX_fk_PFQ_TravelRequestID]
ON [dbo].[RFQs]
    ([TravelRequestID]);
GO

-- Creating foreign key on [UserID] in table 'RFQs'
ALTER TABLE [dbo].[RFQs]
ADD CONSTRAINT [fk_PFQ_UserID]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_PFQ_UserID'
CREATE INDEX [IX_fk_PFQ_UserID]
ON [dbo].[RFQs]
    ([UserID]);
GO

-- Creating foreign key on [ApprovalBy] in table 'TravelRequests'
ALTER TABLE [dbo].[TravelRequests]
ADD CONSTRAINT [FK__TravelReq__ApprovalBy]
    FOREIGN KEY ([ApprovalBy])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__TravelReq__ApprovalBy'
CREATE INDEX [IX_FK__TravelReq__ApprovalBy]
ON [dbo].[TravelRequests]
    ([ApprovalBy]);
GO

-- Creating foreign key on [UserID] in table 'TravelRequests'
ALTER TABLE [dbo].[TravelRequests]
ADD CONSTRAINT [FK__TravelReq_UserID]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__TravelReq_UserID'
CREATE INDEX [IX_FK__TravelReq_UserID]
ON [dbo].[TravelRequests]
    ([UserID]);
GO

-- Creating foreign key on [Roles_RoleId] in table 'UserRoles'
ALTER TABLE [dbo].[UserRoles]
ADD CONSTRAINT [FK_UserRoles_Roles]
    FOREIGN KEY ([Roles_RoleId])
    REFERENCES [dbo].[Roles]
        ([RoleId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Users_UserId] in table 'UserRoles'
ALTER TABLE [dbo].[UserRoles]
ADD CONSTRAINT [FK_UserRoles_Users]
    FOREIGN KEY ([Users_UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserRoles_Users'
CREATE INDEX [IX_FK_UserRoles_Users]
ON [dbo].[UserRoles]
    ([Users_UserId]);
GO

-- Creating foreign key on [CreatedByUser_UserId] in table 'TravelRequests'
ALTER TABLE [dbo].[TravelRequests]
ADD CONSTRAINT [FK__TravelReq_CreatedBy]
    FOREIGN KEY ([CreatedByUser_UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__TravelReq_CreatedBy'
CREATE INDEX [IX_FK__TravelReq_CreatedBy]
ON [dbo].[TravelRequests]
    ([CreatedByUser_UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------