namespace Libreria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class specialprice1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetail", "SpecialPrice", c => c.Decimal(nullable: false, storeType: "money"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetail", "SpecialPrice");
        }
    }
}
