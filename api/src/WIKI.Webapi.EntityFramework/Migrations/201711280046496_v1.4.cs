namespace WIKI.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v14 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tags", "ContentType", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tags", "ContentType");
        }
    }
}
