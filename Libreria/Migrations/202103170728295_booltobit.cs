namespace Libreria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class booltobit : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.member", "Change");
        }
        
        public override void Down()
        {
            AddColumn("dbo.member", "Change", c => c.Boolean());
        }
    }
}
