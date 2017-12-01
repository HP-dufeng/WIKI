use WIKI
GO


IF EXISTS(SELECT 1 FROM sys.tables WHERE object_id = OBJECT_ID('dbo.Questions_ChangeLog'))
	DROP table dbo.Questions_ChangeLog

CREATE TABLE [dbo].[Questions_ChangeLog](
	[Id] [uniqueidentifier] NOT NULL,
	[QuestionId] [bigint] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[IsIndex] [bit] NOT NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL
CONSTRAINT [PK_dbo.Questions_ChangeLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Questions_ChangeLog] ADD  CONSTRAINT [DF_Questions_ChangeLog_CreatedTime]  DEFAULT (getdate()) FOR [CreatedTime]
GO



IF EXISTS(SELECT 1 FROM sys.tables WHERE object_id = OBJECT_ID('dbo.Answers_ChangeLog'))
	DROP table dbo.Answers_ChangeLog

CREATE TABLE [dbo].[Answers_ChangeLog](
	[Id] [uniqueidentifier] NOT NULL,
	[AnswerId] [bigint] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[IsIndex] [bit] NOT NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL
CONSTRAINT [PK_dbo.Answers_ChangeLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Answers_ChangeLog] ADD  CONSTRAINT [DF_Answers_ChangeLog_CreatedTime]  DEFAULT (getdate()) FOR [CreatedTime]
GO




IF EXISTS(SELECT 1 FROM sys.tables WHERE object_id = OBJECT_ID('dbo.Documents_ChangeLog'))
	DROP table dbo.Documents_ChangeLog

CREATE TABLE [dbo].[Documents_ChangeLog](
	[Id] [uniqueidentifier] NOT NULL,
	[DocumentId] [bigint] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[IsIndex] [bit] NOT NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL
CONSTRAINT [PK_dbo.Documents_ChangeLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Documents_ChangeLog] ADD  CONSTRAINT [DF_Documents_ChangeLog_CreatedTime]  DEFAULT (getdate()) FOR [CreatedTime]
GO

