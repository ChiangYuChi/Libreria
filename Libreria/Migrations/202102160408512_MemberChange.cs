namespace Libreria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MemberChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.member", "MobileNumber", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.member", "HomeNumber", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.member", "HomeNumber", c => c.Int());
            AlterColumn("dbo.member", "MobileNumber", c => c.Int(nullable: false));
        }
    }
}
