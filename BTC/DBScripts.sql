/****** Object:  Table [dbo].[BTCLoginInfo] ******/  
SET ANSI_NULLS ON  
GO  
  
SET QUOTED_IDENTIFIER ON  
GO  
  
CREATE TABLE [dbo].[BTCLoginInfo](  
    [BTCEmployeeId] [int] NOT NULL, 
    [BTCEmployeeInfoId] [int] NOT NULL,  
    [UserName] [nvarchar](50) NULL,  
    [Password] [nvarchar](20) NULL,  
    [LastLoginDate] [datetime] NULL,  
    [LoginFailedCount] [int] NULL,  
    [LoginIPAddress] [nvarchar](20) NULL,  
    [CustomerTimeZone] [nvarchar](20) NULL,  
    [LastAccessedDate] [datetime] NULL,  
    [AccountLocked] [bit] NULL,  
 CONSTRAINT [PK_CBLogin1] PRIMARY KEY CLUSTERED   
(  
    [BTCEmployeeId] ASC  
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]  
) ON [PRIMARY]  
  
GO 


CREATE TABLE [dbo].[BTCEmployeeInfo]( 
	[BTCEmployeeInfoId] [int] NOT NULL PRIMARY KEY,  
    [BTCEmployeeId] [int] NOT NULL FOREIGN KEY REFERENCES [BTCLoginInfo]([BTCEmployeeId]),   
	[HREmployeeID] [bigint] NOT NULL FOREIGN KEY REFERENCES [HRW_Employee]([EmployeeID])
)
GO

CREATE TABLE [dbo].[TravelRequest]( 
    [TravelRequestID] [int] NOT NULL PRIMARY KEY,  
    [BTCEmployeeId] [int] NOT NULL FOREIGN KEY REFERENCES [BTCLoginInfo]([BTCEmployeeId]),  
	[ApplicationNumber] [nvarchar](50) NOT NULL,
	[DepartureDate] [datetime] NULL,
	[ReutrnDate] [datetime] NULL,
	[FirstBusinessDay] [datetime] NULL,
	[LastBusinessDay] [datetime] NULL,
	[Remarks] [nvarchar](500) NULL,
	[AirTicketManagement] [nvarchar](50) NULL,
	[HotelName] [nvarchar](50) NULL,
	[TravelAllowance] [nvarchar](50) NULL,
	[HotelStay] [nvarchar](50) NULL,
	[HotelCategory] [nvarchar](50) NULL,
	[RoomCategory] [nvarchar](50) NULL,
	[RoomType] [nvarchar](50) NULL,
	[AdditionalAllowance] [nvarchar](50) NULL,
	[AirportPickUp] [nvarchar](50) NULL,
	[PickUpBy] [nvarchar](50) NULL,
	[CheckInDate] [datetime] NULL,
	[CheckOutDate] [datetime] NULL,
	[ApprovalLevel] [int] NOT NULL,
	[ApprovalBy] [int] FOREIGN KEY REFERENCES [BTCLoginInfo]([BTCEmployeeId]),  
	[ApprovalRemarks] [text],
)
GO

CREATE TABLE [dbo].[TravelRequestDetails](
    [TravelRequestDetailsID] [int] NOT NULL PRIMARY KEY,  
    [TravelRequestID] [int] NOT NULL FOREIGN KEY REFERENCES [TravelRequest]([TravelRequestID]),  
	[OriginPort] [int] NOT NULL FOREIGN KEY REFERENCES [City]([CityID]),  
	[DestinationPort] [int] NOT NULL FOREIGN KEY REFERENCES [City]([CityID]),  
	[TicketClass] [nvarchar](20) NOT NULL,
	[DailyAllowance] [int] NULL,
	[Currency] [int] NULL FOREIGN KEY REFERENCES [Currency]([CurrencyID]),  
	[TravelDays] [int] NULL,
	[Remarks] [nvarchar](500) NULL,
	[PurposeOfVisit] [text] NULL
)
GO 

CREATE TABLE [dbo].[City](
	[CityID] [int] NOT NULL PRIMARY KEY,
	[CityDesc] [nvarchar](100) NOT NULL,
	[IsActive] [bit],
	[GradeLevel] [int]
)
GO

CREATE TABLE [dbo].[Currency](
	[CurrencyID] [int] NOT NULL PRIMARY KEY,
	[CurrencyDesc] [nvarchar](100) NOT NULL,
	[IsActive] [bit]
)
GO