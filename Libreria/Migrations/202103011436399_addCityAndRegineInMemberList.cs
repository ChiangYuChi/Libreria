namespace Libreria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCityAndRegineInMemberList : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.member", "City", c => c.String());
            AddColumn("dbo.member", "Region", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.member", "Region");
            DropColumn("dbo.member", "City");
        }
    }
}
