namespace WIKI.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Download_Log_Daily",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AttachmentId = c.Long(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.View_Log_Daily",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ContentId = c.Long(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.View_Log_History",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ContentId = c.Long(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.View_Log_Statistic",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        ViewCount = c.Long(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedTime = c.DateTime(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.View_Log_Statistic");
            DropTable("dbo.View_Log_History");
            DropTable("dbo.View_Log_Daily");
            DropTable("dbo.Download_Log_Daily");
        }
    }
}
