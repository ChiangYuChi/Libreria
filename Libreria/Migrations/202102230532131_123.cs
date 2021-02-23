namespace Libreria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _123 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Favorite", "RecordId", c => c.Int(nullable: false));
            AddColumn("dbo.Favorite", "ProductName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Favorite", "ProductName");
            DropColumn("dbo.Favorite", "RecordId");
        }
    }
}
