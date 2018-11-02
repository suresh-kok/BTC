
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/02/2018 12:35:13
-- Generated from EDMX file: C:\Users\skanniyappan\Documents\GitHub\BTC\Travel_Request_System\Travel_Request_System_EF\Models\BTC.edmx
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

IF OBJECT_ID(N'[dbo].[FK__BTCEmploy__BTCEm__690797E6]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BTCEmployeeInfoes] DROP CONSTRAINT [FK__BTCEmploy__BTCEm__690797E6];
GO
IF OBJECT_ID(N'[dbo].[FK__BTCEmploy__HREmp__69FBBC1F]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BTCEmployeeInfoes] DROP CONSTRAINT [FK__BTCEmploy__HREmp__69FBBC1F];
GO
IF OBJECT_ID(N'[dbo].[FK__TravelReq__Appro__6DCC4D03]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TravelRequests] DROP CONSTRAINT [FK__TravelReq__Appro__6DCC4D03];
GO
IF OBJECT_ID(N'[dbo].[FK__TravelReq__BTCEm__6CD828CA]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TravelRequests] DROP CONSTRAINT [FK__TravelReq__BTCEm__6CD828CA];
GO
IF OBJECT_ID(N'[dbo].[FK__TravelReq__Desti__05A3D694]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TravelRequestDetails] DROP CONSTRAINT [FK__TravelReq__Desti__05A3D694];
GO
IF OBJECT_ID(N'[dbo].[FK__TravelReq__Origi__04AFB25B]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TravelRequestDetails] DROP CONSTRAINT [FK__TravelReq__Origi__04AFB25B];
GO
IF OBJECT_ID(N'[dbo].[FK__TravelReq__Curre__0697FACD]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TravelRequestDetails] DROP CONSTRAINT [FK__TravelReq__Curre__0697FACD];
GO
IF OBJECT_ID(N'[dbo].[FK_HRW_Employee_HRW_Company]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HRW_Employee] DROP CONSTRAINT [FK_HRW_Employee_HRW_Company];
GO
IF OBJECT_ID(N'[dbo].[FK_ORG_ChartMaster_HRW_Company]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ORG_ChartMaster] DROP CONSTRAINT [FK_ORG_ChartMaster_HRW_Company];
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
IF OBJECT_ID(N'[dbo].[FK_ORG_EmpEntityLink_HRW_Employee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ORG_EmpEntityLink] DROP CONSTRAINT [FK_ORG_EmpEntityLink_HRW_Employee];
GO
IF OBJECT_ID(N'[dbo].[FK_ORG_EntityType_ORG_ChartMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ORG_EntityType] DROP CONSTRAINT [FK_ORG_EntityType_ORG_ChartMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_ORG_EmpEntityLink_ORG_EntityMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ORG_EmpEntityLink] DROP CONSTRAINT [FK_ORG_EmpEntityLink_ORG_EntityMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_ORG_EntityMaster_ORG_EntityType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ORG_EntityMaster] DROP CONSTRAINT [FK_ORG_EntityMaster_ORG_EntityType];
GO
IF OBJECT_ID(N'[dbo].[FK_ORG_EntityTypeParam_ORG_EntityType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ORG_EntityTypeParam] DROP CONSTRAINT [FK_ORG_EntityTypeParam_ORG_EntityType];
GO
IF OBJECT_ID(N'[dbo].[FK__TravelReq__Trave__03BB8E22]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TravelRequestDetails] DROP CONSTRAINT [FK__TravelReq__Trave__03BB8E22];
GO
IF OBJECT_ID(N'[dbo].[FK_UserRole]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Roles] DROP CONSTRAINT [FK_UserRole];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[BTCEmployeeInfoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BTCEmployeeInfoes];
GO
IF OBJECT_ID(N'[dbo].[BTCLoginInfoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BTCLoginInfoes];
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
IF OBJECT_ID(N'[dbo].[TravelRequests]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TravelRequests];
GO
IF OBJECT_ID(N'[dbo].[TravelRequestDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TravelRequestDetails];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'BTCEmployeeInfoes'
CREATE TABLE [dbo].[BTCEmployeeInfoes] (
    [BTCEmployeeInfoId] int  NOT NULL,
    [BTCEmployeeId] int  NOT NULL,
    [HREmployeeID] bigint  NOT NULL
);
GO

-- Creating table 'BTCLoginInfoes'
CREATE TABLE [dbo].[BTCLoginInfoes] (
    [BTCEmployeeId] int  NOT NULL,
    [HREmployeeID] bigint  NOT NULL,
    [UserName] nvarchar(50)  NULL,
    [Password] nvarchar(20)  NULL,
    [LastLoginDate] datetime  NULL,
    [LoginFailedCount] int  NULL,
    [LoginIPAddress] nvarchar(20)  NULL,
    [CustomerTimeZone] nvarchar(20)  NULL,
    [LastAccessedDate] datetime  NULL,
    [AccountLocked] bit  NULL
);
GO

-- Creating table 'Cities'
CREATE TABLE [dbo].[Cities] (
    [CityID] int  NOT NULL,
    [CityDesc] nvarchar(100)  NOT NULL,
    [IsActive] bit  NULL,
    [GradeLevel] int  NULL
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

-- Creating table 'TravelRequests'
CREATE TABLE [dbo].[TravelRequests] (
    [TravelRequestID] int  NOT NULL,
    [BTCEmployeeId] int  NOT NULL,
    [ApplicationNumber] nvarchar(50)  NOT NULL,
    [DepartureDate] datetime  NULL,
    [ReutrnDate] datetime  NULL,
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
    [AdditionalAllowance] nvarchar(50)  NULL,
    [AirportPickUp] nvarchar(50)  NULL,
    [PickUpBy] nvarchar(50)  NULL,
    [CheckInDate] datetime  NULL,
    [CheckOutDate] datetime  NULL,
    [ApprovalLevel] int  NOT NULL,
    [ApprovalBy] int  NULL,
    [ApprovalRemarks] varchar(max)  NULL
);
GO

-- Creating table 'TravelRequestDetails'
CREATE TABLE [dbo].[TravelRequestDetails] (
    [TravelRequestDetailsID] int  NOT NULL,
    [TravelRequestID] int  NOT NULL,
    [OriginPort] int  NOT NULL,
    [DestinationPort] int  NOT NULL,
    [TicketClass] nvarchar(20)  NOT NULL,
    [DailyAllowance] int  NULL,
    [Currency] int  NULL,
    [TravelDays] int  NULL,
    [Remarks] nvarchar(500)  NULL,
    [PurposeOfVisit] varchar(max)  NULL
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
    [ActivationCode] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [RoleId] int IDENTITY(1,1) NOT NULL,
    [RoleName] nvarchar(max)  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [BTCEmployeeInfoId] in table 'BTCEmployeeInfoes'
ALTER TABLE [dbo].[BTCEmployeeInfoes]
ADD CONSTRAINT [PK_BTCEmployeeInfoes]
    PRIMARY KEY CLUSTERED ([BTCEmployeeInfoId] ASC);
GO

-- Creating primary key on [BTCEmployeeId] in table 'BTCLoginInfoes'
ALTER TABLE [dbo].[BTCLoginInfoes]
ADD CONSTRAINT [PK_BTCLoginInfoes]
    PRIMARY KEY CLUSTERED ([BTCEmployeeId] ASC);
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

-- Creating primary key on [TravelRequestID] in table 'TravelRequests'
ALTER TABLE [dbo].[TravelRequests]
ADD CONSTRAINT [PK_TravelRequests]
    PRIMARY KEY CLUSTERED ([TravelRequestID] ASC);
GO

-- Creating primary key on [TravelRequestDetailsID] in table 'TravelRequestDetails'
ALTER TABLE [dbo].[TravelRequestDetails]
ADD CONSTRAINT [PK_TravelRequestDetails]
    PRIMARY KEY CLUSTERED ([TravelRequestDetailsID] ASC);
GO

-- Creating primary key on [UserId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [RoleId] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([RoleId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [BTCEmployeeId] in table 'BTCEmployeeInfoes'
ALTER TABLE [dbo].[BTCEmployeeInfoes]
ADD CONSTRAINT [FK__BTCEmploy__BTCEm__690797E6]
    FOREIGN KEY ([BTCEmployeeId])
    REFERENCES [dbo].[BTCLoginInfoes]
        ([BTCEmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__BTCEmploy__BTCEm__690797E6'
CREATE INDEX [IX_FK__BTCEmploy__BTCEm__690797E6]
ON [dbo].[BTCEmployeeInfoes]
    ([BTCEmployeeId]);
GO

-- Creating foreign key on [HREmployeeID] in table 'BTCEmployeeInfoes'
ALTER TABLE [dbo].[BTCEmployeeInfoes]
ADD CONSTRAINT [FK__BTCEmploy__HREmp__69FBBC1F]
    FOREIGN KEY ([HREmployeeID])
    REFERENCES [dbo].[HRW_Employee]
        ([EmployeeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__BTCEmploy__HREmp__69FBBC1F'
CREATE INDEX [IX_FK__BTCEmploy__HREmp__69FBBC1F]
ON [dbo].[BTCEmployeeInfoes]
    ([HREmployeeID]);
GO

-- Creating foreign key on [ApprovalBy] in table 'TravelRequests'
ALTER TABLE [dbo].[TravelRequests]
ADD CONSTRAINT [FK__TravelReq__Appro__6DCC4D03]
    FOREIGN KEY ([ApprovalBy])
    REFERENCES [dbo].[BTCLoginInfoes]
        ([BTCEmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__TravelReq__Appro__6DCC4D03'
CREATE INDEX [IX_FK__TravelReq__Appro__6DCC4D03]
ON [dbo].[TravelRequests]
    ([ApprovalBy]);
GO

-- Creating foreign key on [BTCEmployeeId] in table 'TravelRequests'
ALTER TABLE [dbo].[TravelRequests]
ADD CONSTRAINT [FK__TravelReq__BTCEm__6CD828CA]
    FOREIGN KEY ([BTCEmployeeId])
    REFERENCES [dbo].[BTCLoginInfoes]
        ([BTCEmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__TravelReq__BTCEm__6CD828CA'
CREATE INDEX [IX_FK__TravelReq__BTCEm__6CD828CA]
ON [dbo].[TravelRequests]
    ([BTCEmployeeId]);
GO

-- Creating foreign key on [DestinationPort] in table 'TravelRequestDetails'
ALTER TABLE [dbo].[TravelRequestDetails]
ADD CONSTRAINT [FK__TravelReq__Desti__05A3D694]
    FOREIGN KEY ([DestinationPort])
    REFERENCES [dbo].[Cities]
        ([CityID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__TravelReq__Desti__05A3D694'
CREATE INDEX [IX_FK__TravelReq__Desti__05A3D694]
ON [dbo].[TravelRequestDetails]
    ([DestinationPort]);
GO

-- Creating foreign key on [OriginPort] in table 'TravelRequestDetails'
ALTER TABLE [dbo].[TravelRequestDetails]
ADD CONSTRAINT [FK__TravelReq__Origi__04AFB25B]
    FOREIGN KEY ([OriginPort])
    REFERENCES [dbo].[Cities]
        ([CityID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__TravelReq__Origi__04AFB25B'
CREATE INDEX [IX_FK__TravelReq__Origi__04AFB25B]
ON [dbo].[TravelRequestDetails]
    ([OriginPort]);
GO

-- Creating foreign key on [Currency] in table 'TravelRequestDetails'
ALTER TABLE [dbo].[TravelRequestDetails]
ADD CONSTRAINT [FK__TravelReq__Curre__0697FACD]
    FOREIGN KEY ([Currency])
    REFERENCES [dbo].[Currencies]
        ([CurrencyID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__TravelReq__Curre__0697FACD'
CREATE INDEX [IX_FK__TravelReq__Curre__0697FACD]
ON [dbo].[TravelRequestDetails]
    ([Currency]);
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

-- Creating foreign key on [TravelRequestID] in table 'TravelRequestDetails'
ALTER TABLE [dbo].[TravelRequestDetails]
ADD CONSTRAINT [FK__TravelReq__Trave__03BB8E22]
    FOREIGN KEY ([TravelRequestID])
    REFERENCES [dbo].[TravelRequests]
        ([TravelRequestID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__TravelReq__Trave__03BB8E22'
CREATE INDEX [IX_FK__TravelReq__Trave__03BB8E22]
ON [dbo].[TravelRequestDetails]
    ([TravelRequestID]);
GO

-- Creating foreign key on [UserId] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [FK_UserRoles]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserRoles'
CREATE INDEX [IX_FK_UserRoles]
ON [dbo].[Roles]
    ([UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------