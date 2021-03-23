namespace Libreria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isCanceledInExhi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExhibitionOrder", "isCanceled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExhibitionOrder", "isCanceled");
        }
    }
}
