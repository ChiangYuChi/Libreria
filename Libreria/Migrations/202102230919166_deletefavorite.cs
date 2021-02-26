namespace Libreria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletefavorite : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Favorite", "RecordId");
            DropColumn("dbo.Favorite", "ProductName");
            DropColumn("dbo.Favorite", "Count");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Favorite", "Count", c => c.Int(nullable: false));
            AddColumn("dbo.Favorite", "ProductName", c => c.String());
            AddColumn("dbo.Favorite", "RecordId", c => c.Int(nullable: false));
        }
    }
}
