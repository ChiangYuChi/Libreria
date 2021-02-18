namespace Libreria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.member", "password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.member", "password", c => c.String());
        }
    }
}
