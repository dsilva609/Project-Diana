SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefreshTokens](
	[Id] [uniqueidentifier] NOT NULL,
	[CreatedOn] [datetimeoffset](7) NOT NULL,
	[ExpiresOn] [datetimeoffset](7) NOT NULL,
	[Token] [nvarchar](256) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[RefreshTokens] ADD  CONSTRAINT [PK_RefreshToken] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO