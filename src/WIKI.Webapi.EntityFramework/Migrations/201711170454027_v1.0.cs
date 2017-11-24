namespace WIKI.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        WUCC_UserId = c.Guid(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 20),
                        FullName = c.String(),
                        Department = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.WUCC_UserId, unique: true)
                .Index(t => t.UserName, unique: true);
            
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        QuestionId = c.Long(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedTime = c.DateTime(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Text = c.String(maxLength: 200),
                        ContentNo = c.String(maxLength: 20),
                        ContentType = c.String(maxLength: 20),
                        Title = c.String(nullable: false, maxLength: 60),
                        Sticky = c.Boolean(nullable: false),
                        Important = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedTime = c.DateTime(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contents",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ContentNo = c.String(maxLength: 20),
                        ContentType = c.String(maxLength: 20),
                        Title = c.String(nullable: false, maxLength: 60),
                        Sticky = c.Boolean(nullable: false),
                        Important = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedTime = c.DateTime(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Description = c.String(maxLength: 200),
                        ContentNo = c.String(maxLength: 20),
                        ContentType = c.String(maxLength: 20),
                        Title = c.String(nullable: false, maxLength: 60),
                        Sticky = c.Boolean(nullable: false),
                        Important = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedTime = c.DateTime(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DocumentAttachments",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FileName = c.String(maxLength: 100),
                        DisplayName = c.String(maxLength: 50),
                        DocumentId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Documents", t => t.DocumentId, cascadeDelete: true)
                .Index(t => t.DocumentId);
            
            //CreateTable(
            //    "dbo.view_Document",
            //    c => new
            //        {
            //            Id = c.Long(nullable: false, identity: true),
            //            ContentNo = c.String(),
            //            Title = c.String(),
            //            Description = c.String(),
            //            Sticky = c.Boolean(nullable: false),
            //            Important = c.Boolean(nullable: false),
            //            CreatedBy_FullName = c.String(),
            //            CreatedBy_Department = c.String(),
            //            CreatedTime = c.DateTime(nullable: false),
            //            CreatedBy = c.String(),
            //            UpdatedTime = c.DateTime(),
            //            UpdatedBy = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.view_Question",
            //    c => new
            //        {
            //            Id = c.Long(nullable: false, identity: true),
            //            ContentNo = c.String(),
            //            Title = c.String(),
            //            Text = c.String(),
            //            Sticky = c.Boolean(nullable: false),
            //            Important = c.Boolean(nullable: false),
            //            CreatedBy_FullName = c.String(),
            //            CreatedBy_Department = c.String(),
            //            Answer_Id = c.Long(nullable: false),
            //            Answer_Text = c.String(),
            //            Answer_CreatedTime = c.DateTime(),
            //            Answer_UpdatedTime = c.DateTime(),
            //            Answer_UpdatedBy = c.String(),
            //            Answer_CreatedBy = c.String(),
            //            Answer_CreatedBy_FullName = c.String(),
            //            Answer_CreatedBy_Department = c.String(),
            //            CreatedTime = c.DateTime(nullable: false),
            //            CreatedBy = c.String(),
            //            UpdatedTime = c.DateTime(),
            //            UpdatedBy = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        ContentId = c.Long(nullable: false),
                        Value = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => new { t.ContentId, t.Value });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DocumentAttachments", "DocumentId", "dbo.Documents");
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropIndex("dbo.DocumentAttachments", new[] { "DocumentId" });
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            DropIndex("dbo.Accounts", new[] { "UserName" });
            DropIndex("dbo.Accounts", new[] { "WUCC_UserId" });
            DropTable("dbo.Tags");
            //DropTable("dbo.view_Question");
            //DropTable("dbo.view_Document");
            DropTable("dbo.DocumentAttachments");
            DropTable("dbo.Documents");
            DropTable("dbo.Contents");
            DropTable("dbo.Questions");
            DropTable("dbo.Answers");
            DropTable("dbo.Accounts");
        }
    }
}
