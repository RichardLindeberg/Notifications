
/****** Object:  Table [dbo].[Commits]    Script Date: 2016-11-29 10:21:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Commits](
	[BucketId] [varchar](40) NOT NULL,
	[StreamId] [char](40) NOT NULL,
	[StreamIdOriginal] [nvarchar](1000) NOT NULL,
	[StreamRevision] [int] NOT NULL,
	[Items] [tinyint] NOT NULL,
	[CommitId] [uniqueidentifier] NOT NULL,
	[CommitSequence] [int] NOT NULL,
	[CheckpointNumber] [bigint] IDENTITY(1,1) NOT NULL,
	[Dispatched] [bit] NOT NULL,
	[Headers] [varbinary](max) NULL,
	[Payload] [varbinary](max) NOT NULL,
	[CommitStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Commits] PRIMARY KEY CLUSTERED 
(
	[CheckpointNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Snapshots]    Script Date: 2016-11-29 10:21:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Snapshots](
	[BucketId] [varchar](40) NOT NULL,
	[StreamId] [char](40) NOT NULL,
	[StreamRevision] [int] NOT NULL,
	[Payload] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_Snapshots] PRIMARY KEY CLUSTERED 
(
	[BucketId] ASC,
	[StreamId] ASC,
	[StreamRevision] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Commits_CommitId]    Script Date: 2016-11-29 10:21:27 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Commits_CommitId] ON [dbo].[Commits]
(
	[BucketId] ASC,
	[StreamId] ASC,
	[CommitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Commits_CommitSequence]    Script Date: 2016-11-29 10:21:27 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Commits_CommitSequence] ON [dbo].[Commits]
(
	[BucketId] ASC,
	[StreamId] ASC,
	[CommitSequence] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Commits_Dispatched]    Script Date: 2016-11-29 10:21:27 ******/
CREATE NONCLUSTERED INDEX [IX_Commits_Dispatched] ON [dbo].[Commits]
(
	[Dispatched] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Commits_Revisions]    Script Date: 2016-11-29 10:21:27 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Commits_Revisions] ON [dbo].[Commits]
(
	[BucketId] ASC,
	[StreamId] ASC,
	[StreamRevision] ASC,
	[Items] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Commits_Stamp]    Script Date: 2016-11-29 10:21:27 ******/
CREATE NONCLUSTERED INDEX [IX_Commits_Stamp] ON [dbo].[Commits]
(
	[CommitStamp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Commits] ADD  DEFAULT ((0)) FOR [Dispatched]
GO
ALTER TABLE [dbo].[Commits]  WITH CHECK ADD CHECK  (([CommitId]<>0x00))
GO
ALTER TABLE [dbo].[Commits]  WITH CHECK ADD CHECK  (([CommitSequence]>(0)))
GO
ALTER TABLE [dbo].[Commits]  WITH CHECK ADD CHECK  (([Headers] IS NULL OR datalength([Headers])>(0)))
GO
ALTER TABLE [dbo].[Commits]  WITH CHECK ADD CHECK  (([Items]>(0)))
GO
ALTER TABLE [dbo].[Commits]  WITH CHECK ADD CHECK  ((datalength([Payload])>(0)))
GO
ALTER TABLE [dbo].[Commits]  WITH CHECK ADD CHECK  (([StreamRevision]>(0)))
GO
ALTER TABLE [dbo].[Snapshots]  WITH CHECK ADD CHECK  ((datalength([Payload])>(0)))
GO
ALTER TABLE [dbo].[Snapshots]  WITH CHECK ADD CHECK  (([StreamRevision]>(0)))
GO
