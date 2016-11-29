

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


CREATE TABLE [dbo].[AllTokens_ReadModell_Commits](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[commitId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_AllTokens_ReadModell_Commits] PRIMARY KEY CLUSTERED 
(
	[id] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


CREATE PROCEDURE CleanAllTokens_ReadModell_Commits AS 
BEGIN
IF (SELECT COUNT(*) FROM AllTokens_ReadModell_Commits) > 1000
	BEGIN
		DECLARE @keepCommit int
		select top 1 @keepCommit = id from AllTokens_ReadModell_Commits ORDER by id desc 
		DELETE FROM AllTokens_ReadModell_Commits where id != @keepCommit
	END
END


GO 
CREATE PROCEDURE AllTokens_ReadModell_ByToken @FireBaseToken nvarchar(400) AS 
BEGIN
SELECT TOP (1000) [RowID]
      ,[PersonalNumber]
      ,[FireBaskeToken]
      ,[NotificationTypeId]
  FROM [DbUpTesting].[dbo].[AllTokens_ReadModell]
  WHERE FireBaskeToken = @FireBaseToken
END


GO
CREATE PROCEDURE AllTokens_ReadModell_Add
	 @PersonalNumber  varchar(12), 
     @FireBaskeToken nvarchar(400),
     @NotificationTypeId nvarchar(50), 
	 @CommitId UNIQUEIDENTIFIER 
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
	INSERT INTO AllTokens_ReadModell_Commits (commitId) VALUES (@CommitId)
COMMIT TRAN
EXEC CleanAllTokens_ReadModell_Commits
END

GO

CREATE PROCEDURE AllTokens_ReadModell_Remove 
	 @PersonalNumber  varchar(12), 
     @FireBaskeToken nvarchar(400),
     @NotificationTypeId nvarchar(50), 
	 @CommitId UNIQUEIDENTIFIER 
	 AS 
BEGIN
BEGIN TRAN 
	DELETE FROM [dbo].[AllTokens_ReadModell]
			   WHERE [PersonalNumber] = @PersonalNumber
					AND [FireBaskeToken] = @FireBaskeToken
					AND [NotificationTypeId] = @NotificationTypeId

	INSERT INTO AllTokens_ReadModell_Commits (commitId) VALUES (@CommitId)
COMMIT TRAN
EXEC CleanAllTokens_ReadModell_Commits
END

