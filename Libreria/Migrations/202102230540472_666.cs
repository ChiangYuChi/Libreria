namespace Libreria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _666 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Favorite", "Count", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Favorite", "Count");
        }
    }
}
