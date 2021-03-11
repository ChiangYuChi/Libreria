namespace Libreria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Manager : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "isSpecial", c => c.Boolean(nullable: false));
            AddColumn("dbo.Exhibition", "ReviewState", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exhibition", "ReviewState");
            DropColumn("dbo.Product", "isSpecial");
        }
    }
}
