namespace Libreria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class exAddfixIsDeletedMemberChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.member", "Change", c => c.Boolean());
            AddColumn("dbo.Exhibition", "isDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exhibition", "isDeleted");
            DropColumn("dbo.member", "Change");
        }
    }
}
