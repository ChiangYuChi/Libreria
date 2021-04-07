namespace Libreria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class birthdayNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.member", "birthday", c => c.DateTime(storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.member", "birthday", c => c.DateTime(nullable: false, storeType: "date"));
        }
    }
}
