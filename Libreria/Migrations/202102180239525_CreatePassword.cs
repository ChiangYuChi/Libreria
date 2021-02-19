namespace Libreria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class CreatePassword : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.member", "password", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.member", "password");
        }
    }
}
