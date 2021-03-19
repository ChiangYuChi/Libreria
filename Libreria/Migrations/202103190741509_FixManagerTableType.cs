namespace Libreria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixManagerTableType : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Manager");
            AddColumn("dbo.Manager", "ManagerRoleId", c => c.Int(nullable: false));
            AlterColumn("dbo.Manager", "ManagerId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Manager", "ManagerPassword", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Manager", "ManagerName", c => c.String(nullable: false, maxLength: 50));
            AddPrimaryKey("dbo.Manager", "ManagerId");
            DropColumn("dbo.Manager", "JobTitle");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Manager", "JobTitle", c => c.String(maxLength: 50));
            DropPrimaryKey("dbo.Manager");
            AlterColumn("dbo.Manager", "ManagerName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Manager", "ManagerPassword", c => c.String(maxLength: 50));
            AlterColumn("dbo.Manager", "ManagerId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Manager", "ManagerRoleId");
            AddPrimaryKey("dbo.Manager", "ManagerId");
        }
    }
}
