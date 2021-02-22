namespace Libreria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_membertable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.member", "memberPassword", c => c.String(nullable: false, maxLength: 1024));
            DropColumn("dbo.member", "password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.member", "password", c => c.String());
            AlterColumn("dbo.member", "memberPassword", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
