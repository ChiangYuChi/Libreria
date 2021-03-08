namespace Libreria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class paymenteshi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExhibitionOrder", "PaymentState", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExhibitionOrder", "PaymentState");
        }
    }
}
