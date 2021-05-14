/****** Object:  Table [dbo].[Tracks]    Script Date: 2/8/2021 20:37:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

USE [CinderellaCore]
GO

CREATE TABLE [dbo].[Tracks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AlbumId] [int] NOT NULL,
	[duration] [nvarchar](max) NULL,
	[position] [nvarchar](max) NULL,
	[title] [nvarchar](max) NULL,
 CONSTRAINT [PK_Tracks] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Tracks]  WITH CHECK ADD  CONSTRAINT [FK_Tracks_Albums_AlbumId] FOREIGN KEY([AlbumId])
REFERENCES [dbo].[Albums] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Tracks] CHECK CONSTRAINT [FK_Tracks_Albums_AlbumId]
GO