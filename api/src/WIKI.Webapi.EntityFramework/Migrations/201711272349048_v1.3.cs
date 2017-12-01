namespace WIKI.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DocumentAttachments", "CreatedTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.DocumentAttachments", "CreatedBy", c => c.String());
            AddColumn("dbo.DocumentAttachments", "UpdatedTime", c => c.DateTime());
            AddColumn("dbo.DocumentAttachments", "UpdatedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DocumentAttachments", "UpdatedBy");
            DropColumn("dbo.DocumentAttachments", "UpdatedTime");
            DropColumn("dbo.DocumentAttachments", "CreatedBy");
            DropColumn("dbo.DocumentAttachments", "CreatedTime");
        }
    }
}
