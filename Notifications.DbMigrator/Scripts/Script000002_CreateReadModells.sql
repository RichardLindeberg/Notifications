USE [Notifications]
GO
/****** Object:  Table [dbo].[AllTokens_ReadModell]    Script Date: 2016-11-30 00:03:32 ******/
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
/****** Object:  Table [dbo].[AllTokens_ReadModell_Commits]    Script Date: 2016-11-30 00:03:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AllTokens_ReadModell_Commits](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[CheckPointToken] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_AllTokens_ReadModell_Commits] PRIMARY KEY CLUSTERED 
(
	[id] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MultiActiveTokenProhibiter_Commits]    Script Date: 2016-11-30 00:03:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MultiActiveTokenProhibiter_Commits](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CheckPointToken] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MultiActiveTokenProhibiter_Commits] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_AllTokens_ReadModell__FirebaseToken]    Script Date: 2016-11-30 00:03:32 ******/
CREATE NONCLUSTERED INDEX [IX_AllTokens_ReadModell__FirebaseToken] ON [dbo].[AllTokens_ReadModell]
(
	[FireBaskeToken] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_AllTokens_ReadModell_PersonalNumber]    Script Date: 2016-11-30 00:03:32 ******/
CREATE NONCLUSTERED INDEX [IX_AllTokens_ReadModell_PersonalNumber] ON [dbo].[AllTokens_ReadModell]
(
	[PersonalNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[AllTokens_ReadModell_Add]    Script Date: 2016-11-30 00:03:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AllTokens_ReadModell_Add]
	 @PersonalNumber  varchar(12), 
     @FireBaskeToken nvarchar(400),
     @NotificationTypeId nvarchar(50), 
	 @CheckPointToken nvarchar(50)
	 AS 
BEGIN
BEGIN TRAN 
	INSERT INTO [dbo].[AllTokens_ReadModell]
			   ([PersonalNumber]
			   ,[FireBaskeToken]
			   ,[NotificationTypeId])
		 VALUES (
			   @PersonalNumber, 
			   @FireBaskeToken, 
			   @NotificationTypeId
			   )
	INSERT INTO AllTokens_ReadModell_Commits (CheckPointToken) VALUES (@CheckPointToken)
COMMIT TRAN
EXEC CleanAllTokens_ReadModell_Commits
END
GO
/****** Object:  StoredProcedure [dbo].[AllTokens_ReadModell_ByPersonalNumber]    Script Date: 2016-11-30 00:03:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AllTokens_ReadModell_ByPersonalNumber] @PersonalNumber varchar(12) AS 
BEGIN
SELECT TOP (1000) [RowID]
      ,[PersonalNumber]
      ,[FireBaskeToken]
      ,[NotificationTypeId]
  FROM [dbo].[AllTokens_ReadModell]
  WHERE PersonalNumber = @PersonalNumber
END
GO
/****** Object:  StoredProcedure [dbo].[AllTokens_ReadModell_ByToken]    Script Date: 2016-11-30 00:03:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AllTokens_ReadModell_ByToken] @FireBaseToken nvarchar(400) AS 
BEGIN
SELECT TOP (1000) [RowID]
      ,[PersonalNumber]
      ,[FireBaskeToken]
      ,[NotificationTypeId]
  FROM [dbo].[AllTokens_ReadModell]
  WHERE FireBaskeToken = @FireBaseToken
END
GO
/****** Object:  StoredProcedure [dbo].[AllTokens_ReadModell_GetAll]    Script Date: 2016-11-30 00:03:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AllTokens_ReadModell_GetAll] AS 
BEGIN
SELECT TOP (1000) [RowID]
      ,[PersonalNumber]
      ,[FireBaskeToken]
      ,[NotificationTypeId]
  FROM [dbo].[AllTokens_ReadModell]
END
GO
/****** Object:  StoredProcedure [dbo].[AllTokens_ReadModell_LastCommitId]    Script Date: 2016-11-30 00:03:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AllTokens_ReadModell_LastCommitId] AS 
SELECT TOP 1 CheckPointToken FROM AllTokens_ReadModell_Commits ORDER BY Id DESC
GO
/****** Object:  StoredProcedure [dbo].[AllTokens_ReadModell_Remove]    Script Date: 2016-11-30 00:03:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AllTokens_ReadModell_Remove] 
	 @PersonalNumber  varchar(12), 
     @FireBaskeToken nvarchar(400),
     @NotificationTypeId nvarchar(50), 
	  @CheckPointToken nvarchar(50)
	 AS 
BEGIN
BEGIN TRAN 
	DELETE FROM [dbo].[AllTokens_ReadModell]
			   WHERE [PersonalNumber] = @PersonalNumber
					AND [FireBaskeToken] = @FireBaskeToken
					AND [NotificationTypeId] = @NotificationTypeId

	INSERT INTO AllTokens_ReadModell_Commits (CheckPointToken) VALUES (@CheckPointToken)
COMMIT TRAN
EXEC CleanAllTokens_ReadModell_Commits
END
GO
/****** Object:  StoredProcedure [dbo].[CleanAllTokens_ReadModell_Commits]    Script Date: 2016-11-30 00:03:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CleanAllTokens_ReadModell_Commits] AS 
BEGIN
IF (SELECT COUNT(*) FROM AllTokens_ReadModell_Commits) > 1000
	BEGIN
		DECLARE @keepCommit int
		select top 1 @keepCommit = id from AllTokens_ReadModell_Commits ORDER by id desc 
		DELETE FROM AllTokens_ReadModell_Commits where id != @keepCommit
	END
END
GO
/****** Object:  StoredProcedure [dbo].[MultiActiveTokenProhibiter_Commits_AddCommit]    Script Date: 2016-11-30 00:03:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MultiActiveTokenProhibiter_Commits_AddCommit] 
	  @CheckPointToken nvarchar(50)
	  AS
BEGIN
	INSERT INTO MultiActiveTokenProhibiter_Commits (CheckPointToken) VALUES (@CheckPointToken)
	IF (SELECT COUNT(*) FROM MultiActiveTokenProhibiter_Commits) > 1000
	BEGIN
		DECLARE @keepId int
		SELECT TOP 1 @keepId = Id FROM MultiActiveTokenProhibiter_Commits ORDER BY ID DESC 
		DELETE FROM MultiActiveTokenProhibiter_Commits WHERE Id < @keepId
	END
END
GO
/****** Object:  StoredProcedure [dbo].[MultiActiveTokenProhibiter_Commits_LastCommit]    Script Date: 2016-11-30 00:03:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MultiActiveTokenProhibiter_Commits_LastCommit] AS
SELECT TOP 1 CheckPointToken FROM MultiActiveTokenProhibiter_Commits ORDER BY Id DESC
GO
