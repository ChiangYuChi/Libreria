namespace Libreria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LineLogin1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.member", "IDnumber", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.member", "IDnumber", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
