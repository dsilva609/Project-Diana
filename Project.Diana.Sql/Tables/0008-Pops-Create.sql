/****** Object:  Table [dbo].[Pops]    Script Date: 2/8/2021 20:36:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

USE [CinderellaCore]
GO

CREATE TABLE [dbo].[Pops](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](max) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[UserNum] [int] NOT NULL,
	[Genre] [nvarchar](max) NULL,
	[Language] [nvarchar](max) NULL,
	[Notes] [nvarchar](max) NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[YearReleased] [int] NOT NULL,
	[IsPhysical] [bit] NOT NULL,
	[IsNew] [bit] NOT NULL,
	[LocationPurchased] [nvarchar](max) NULL,
	[DatePurchased] [datetime2](7) NOT NULL,
	[DateAdded] [datetime2](7) NOT NULL,
	[DateUpdated] [datetime2](7) NOT NULL,
	[LastCompleted] [datetime2](7) NOT NULL,
	[CompletionStatus] [int] NOT NULL,
	[DateStarted] [datetime2](7) NOT NULL,
	[DateCompleted] [datetime2](7) NOT NULL,
	[CheckedOut] [bit] NOT NULL,
	[TimesCompleted] [int] NOT NULL,
	[Category] [nvarchar](max) NULL,
	[CountryOfOrigin] [nvarchar](max) NULL,
	[CountryPurchased] [nvarchar](max) NULL,
	[IsShowcased] [bit] NOT NULL,
	[IsQueued] [bit] NOT NULL,
	[QueueRank] [int] NOT NULL,
	[Series] [nvarchar](max) NOT NULL,
	[PopLine] [nvarchar](max) NOT NULL,
	[Number] [int] NOT NULL,
 CONSTRAINT [PK_Pops] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO