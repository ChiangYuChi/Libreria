namespace Libreria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteManager : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Manager");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Manager",
                c => new
                    {
                        ManagerId = c.Int(nullable: false, identity: true),
                        ManagerName = c.String(nullable: false, maxLength: 50),
                        ManagerPassword = c.String(nullable: false, maxLength: 50),
                        ManagerUsername = c.String(maxLength: 50),
                        ManagerPhoto = c.String(),
                        ManagerRoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ManagerId);
            
        }
    }
}
