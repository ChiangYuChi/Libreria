namespace Libreria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeIsFavFromProductAddColumnAtExhiAndMember : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExhibitionOrder", "customerVerify", c => c.Boolean(nullable: false));
            AlterColumn("dbo.member", "City", c => c.String(maxLength: 10));
            AlterColumn("dbo.member", "Region", c => c.String(maxLength: 10));
            AlterColumn("dbo.member", "Address", c => c.String(maxLength: 100));
            AlterColumn("dbo.member", "LineUserID", c => c.String(maxLength: 512));
            DropColumn("dbo.Product", "isFav");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "isFav", c => c.Boolean(nullable: false));
            AlterColumn("dbo.member", "LineUserID", c => c.String());
            AlterColumn("dbo.member", "Address", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.member", "Region", c => c.String());
            AlterColumn("dbo.member", "City", c => c.String());
            DropColumn("dbo.ExhibitionOrder", "customerVerify");
        }
    }
}
