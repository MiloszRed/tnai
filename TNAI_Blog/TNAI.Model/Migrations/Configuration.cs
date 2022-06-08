namespace TNAI.Model.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TNAI.Model.Entities;
    using TNAI.Model.Identity;

    internal sealed class Configuration : DbMigrationsConfiguration<TNAI.Model.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TNAI.Model.AppDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.


            // Role.
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var role = new IdentityRole();
            role.Name = "User";
            roleManager.Create(role);
            role = new IdentityRole();
            role.Name = "Admin";
            roleManager.Create(role);


        }
    }
}
