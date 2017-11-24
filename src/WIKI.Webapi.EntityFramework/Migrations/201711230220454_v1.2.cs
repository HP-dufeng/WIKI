namespace WIKI.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v12 : DbMigration
    {
        public override void Up()
        {
            //AlterColumn("dbo.view_Question", "Answer_Id", c => c.Long());
        }
        
        public override void Down()
        {
            //AlterColumn("dbo.view_Question", "Answer_Id", c => c.Long(nullable: false));
        }
    }
}
