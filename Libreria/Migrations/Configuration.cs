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
            context.members.AddOrUpdate(x => x.memberId,
               new member() { memberId = 2, memberName = "Libreria02", MobileNumber = "0900000002", HomeNumber = "(00)00000002", Address = "2", Email = "Libreria02@gmail.com", memberUserName = "Libreria02", memberPassword = "@Libreria02", IDnumber = "B123456789", RoleId = 2 });
            context.Roles.AddOrUpdate(x => x.RoleID,
                new Role() { RoleID = 2, RoleName = "Libreria02" });
            context.members.AddOrUpdate(x => x.memberId,
               new member() { memberId = 3, memberName = "Libreria03", MobileNumber = "0900000003", HomeNumber = "(00)00000003", Address = "3", Email = "Libreria03@gmail.com", memberUserName = "Libreria03", memberPassword = "@Libreria03", IDnumber = "C123456789", RoleId = 3 });
            context.Roles.AddOrUpdate(x => x.RoleID,
                new Role() { RoleID = 3, RoleName = "Libreria03" });
            context.members.AddOrUpdate(x => x.memberId,
               new member() { memberId = 4, memberName = "Libreria04", MobileNumber = "0900000004", HomeNumber = "(00)00000000", Address = "4", Email = "Libreria04@gmail.com", memberUserName = "Libreria04", memberPassword = "@Libreria04", IDnumber = "D123456789", RoleId = 4 });
            context.Roles.AddOrUpdate(x => x.RoleID,
                new Role() { RoleID = 4, RoleName = "Libreria04" });
            context.members.AddOrUpdate(x => x.memberId,
               new member() { memberId = 5, memberName = "Libreria05", MobileNumber = "0900000005", HomeNumber = "(00)00000000", Address = "5", Email = "Libreria05@gmail.com", memberUserName = "Libreria05", memberPassword = "@Libreria05", IDnumber = "E123456789", RoleId = 5 });
            context.Roles.AddOrUpdate(x => x.RoleID,
                new Role() { RoleID = 5, RoleName = "Libreria05" });
        }
    }
}
