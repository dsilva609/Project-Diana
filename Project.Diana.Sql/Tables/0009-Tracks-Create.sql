/****** Object:  Table [dbo].[Tracks]    Script Date: 2/8/2021 20:37:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Tracks](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AlbumID] [int] NOT NULL,
	[duration] [nvarchar](max) NULL,
	[position] [nvarchar](max) NULL,
	[title] [nvarchar](max) NULL,
 CONSTRAINT [PK_Tracks] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Tracks]  WITH CHECK ADD  CONSTRAINT [FK_Tracks_Albums_AlbumID] FOREIGN KEY([AlbumID])
REFERENCES [dbo].[Albums] ([ID])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Tracks] CHECK CONSTRAINT [FK_Tracks_Albums_AlbumID]
GO


