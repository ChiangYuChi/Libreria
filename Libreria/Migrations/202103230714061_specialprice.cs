namespace Libreria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class specialprice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "SpecialPrice", c => c.Decimal(nullable: false, storeType: "money"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "SpecialPrice");
        }
    }
}
