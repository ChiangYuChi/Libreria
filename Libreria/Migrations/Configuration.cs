namespace Libreria.Migrations
{
    using Libreria.Models.EntityModel;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Libreria.Models.EntityModel.LibreriaDataModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Libreria.Models.EntityModel.LibreriaDataModel context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.members.AddOrUpdate(x => x.memberId,
                new member() { memberId = 1, memberName = "Libreria01", MobileNumber = "0900000001", HomeNumber = "(00)00000000", Address = "1", Email = "Libreria01@gmail.com", memberUserName = "Libreria01", memberPassword = "@Libreria01",IDnumber="A123456789" , RoleId = 1 });
            context.Roles.AddOrUpdate(x => x.RoleID,
                new Role() { RoleID = 1, RoleName = "Libreria01"});
        }
    }
}
