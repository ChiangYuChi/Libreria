namespace Libreria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsFav : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "isFav", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "isFav");
        }
    }
}
