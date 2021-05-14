/****** Object:  Table [dbo].[Wishes]    Script Date: 2/8/2021 20:37:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IdENTIFIER ON
GO

USE [CinderellaCore]
GO

CREATE TABLE [dbo].[Wishes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[UserId] [nvarchar](max) NOT NULL,
	[ApiId] [nvarchar](max) NULL,
	[DateAdded] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
	[ItemType] [int] NOT NULL,
	[Notes] [nvarchar](max) NULL,
	[Category] [nvarchar](max) NULL,
	[Owned] [bit] NOT NULL,
	[ImageUrl] [nvarchar](max) NULL,
 CONSTRAINT [PK_Wishes] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO