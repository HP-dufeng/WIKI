namespace WIKI.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WIKI.EntityFramework.WIKIDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(WIKI.EntityFramework.WIKIDbContext context)
        {

            CreateLogTable(context);

            CreateQuestionView(context);

            CreateDocumentView(context);

        }

        private void CreateLogTable(WIKIDbContext context)
        {
            // 不存在日志表则增加
            string createLogSql = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Logs]') AND type IN (N'U')) " +
                                "BEGIN " +
                                "  CREATE TABLE [dbo].[Logs]( " +
                                "    [Id] [INT] IDENTITY(1,1) NOT NULL, " +
                                "    [Level] [NVARCHAR](128) NULL, " +
                                "    [TimeStamp] [DATETIMEOFFSET](7) NOT NULL, " +
                                "    [Exception] [NVARCHAR](MAX) NULL, " +
                                "    [LogEvent] [NVARCHAR](200) NULL, " +
                                "    [LogType] [INT] NULL, " +
                                "    [UserId] [NVARCHAR](100) NULL, " +
                                "    [UserName] [NVARCHAR](200) NULL, " +
                                "    [FullName] [NVARCHAR](200) NULL, " +
                                "    [IP] [NVARCHAR](100) NULL, " +
                                "    [RefUrl] [NVARCHAR](4000) NULL, " +
                                "    [Url] [NVARCHAR](4000) NULL, " +
                                "    [Module] [NVARCHAR](200) NULL, " +
                                "    [Action] [NVARCHAR](200) NULL, " +
                                "    [HttpMethod] [NVARCHAR](50) NULL, " +
                                "    [Content] [NVARCHAR](MAX) NULL, " +
                                "   CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED  " +
                                "  ( " +
                                "    [Id] ASC " +
                                "  )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] " +
                                "  ) ON [PRIMARY] " +
                                "END ";

            context.Database.ExecuteSqlCommand(createLogSql);
        }

        private void CreateQuestionView(WIKIDbContext context)
        {
            string delete_question_view = @"
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[view_Question]') AND type in (N'V'))
DROP VIEW [dbo].[view_Question]
";
            string create_question_view = @"
CREATE VIEW [dbo].[view_Question]
AS
	SELECT q.[Id]
          ,q.[ContentNo]
		  ,q.[Title]
		  ,q.[Text]
		  ,[Sticky]
		  ,[Important]
		  ,q.[CreatedTime]
		  ,q.[UpdatedTime]
		  ,q.UpdatedBy
		  ,q.CreatedBy
		  ,cb.FullName as CreatedBy_FullName
		  ,cb.Department as CreatedBy_Department
		  ,a.Id as Answer_Id
		  ,a.Text as Answer_Text
		  ,a.CreatedTime as Answer_CreatedTime
		  ,a.UpdatedTime as Answer_UpdatedTime
		  ,a.UpdatedBy as Answer_UpdatedBy
		  ,a.CreatedBy as Answer_CreatedBy
		  ,rb.FullName as Answer_CreatedBy_FullName
		  ,rb.Department as Answer_CreatedBy_Department
	FROM [WIKI].[dbo].[Questions] as q
	left join Answers as a on a.QuestionId = q.Id
	left join Accounts as cb on cb.UserName = q.CreatedBy
	left join Accounts as rb on rb.UserName = a.CreatedBy
";
            context.Database.ExecuteSqlCommand(delete_question_view);
            context.Database.ExecuteSqlCommand(create_question_view);
        }

        private void CreateDocumentView(WIKIDbContext context)
        {
            string delete_view = @"
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[view_Document]') AND type in (N'V'))
DROP VIEW [dbo].[view_Document]
";
            string create_view = @"
CREATE VIEW [dbo].[view_Document]
AS
	SELECT d.[Id]
      ,[ContentNo]
      ,[Title]
      ,[Sticky]
      ,[Important]
      ,[Description]
      ,d.[CreatedTime]
	  ,d.CreatedBy
      ,a.FullName as CreatedBy_FullName
	  ,a.Department as CreatedBy_Department
      ,d.[UpdatedTime]
      ,d.[UpdatedBy]
  FROM [WIKI].[dbo].[Documents] as d
  join Accounts as a on a.UserName = d.CreatedBy 
";
            context.Database.ExecuteSqlCommand(delete_view);
            context.Database.ExecuteSqlCommand(create_view);
        }

    }
}
