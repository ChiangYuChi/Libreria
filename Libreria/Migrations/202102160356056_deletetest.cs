namespace Libreria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletetest : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Product", "test");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "test", c => c.Int(nullable: false));
        }
    }
}
