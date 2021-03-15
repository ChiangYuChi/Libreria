namespace Libreria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LineLogin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.member", "LineUserID", c => c.String());
            AddColumn("dbo.member", "Change", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.member", "Change");
            DropColumn("dbo.member", "LineUserID");
        }
    }
}
