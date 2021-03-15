namespace Libreria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Manageradd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Manager",
                c => new
                    {
                        ManagerId = c.String(nullable: false, maxLength: 128),
                        ManagerUsername = c.String(maxLength: 50),
                        ManagerPassword = c.String(maxLength: 50),
                        ManagerPhoto = c.String(),
                        ManagerName = c.String(maxLength: 50),
                        JobTitle = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ManagerId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Manager");
        }
    }
}
