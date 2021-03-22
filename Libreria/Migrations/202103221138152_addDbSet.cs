namespace Libreria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDbSet : DbMigration
    {
        public override void Up()
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
        
        public override void Down()
        {
            DropTable("dbo.Manager");
        }
    }
}
