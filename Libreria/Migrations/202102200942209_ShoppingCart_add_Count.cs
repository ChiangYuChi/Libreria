namespace Libreria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShoppingCart_add_Count : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShoppingCart", "Count", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShoppingCart", "Count");
        }
    }
}
