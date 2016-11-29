

/****** Object:  Table [dbo].[AllTokens_ReadModell]    Script Date: 2016-11-29 10:21:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AllTokens_ReadModell](
	[RowID] [int] IDENTITY(1,1) NOT NULL,
	[PersonalNumber] [varchar](12) NULL,
	[FireBaskeToken] [nvarchar](400) NULL,
	[NotificationTypeId] [nvarchar](50) NULL,
 CONSTRAINT [PK_AllTokens_ReadModell] PRIMARY KEY CLUSTERED 
(
	[RowID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING ON

GO

/****** Object:  Index [IX_AllTokens_ReadModell__FirebaseToken]    Script Date: 2016-11-29 10:21:59 ******/
CREATE NONCLUSTERED INDEX [IX_AllTokens_ReadModell__FirebaseToken] ON [dbo].[AllTokens_ReadModell]
(
	[FireBaskeToken] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

SET ANSI_PADDING ON

GO

/****** Object:  Index [IX_AllTokens_ReadModell_PersonalNumber]    Script Date: 2016-11-29 10:21:59 ******/
CREATE NONCLUSTERED INDEX [IX_AllTokens_ReadModell_PersonalNumber] ON [dbo].[AllTokens_ReadModell]
(
	[PersonalNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

