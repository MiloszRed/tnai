using System.Linq;

using TNAI.Model.Entities;

namespace TNAI.Model.Migrations
{
    using System.Data.Entity.Migrations;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using TNAI.Model.Identity;

    internal sealed class Configuration : DbMigrationsConfiguration<TNAI.Model.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TNAI.Model.AppDbContext context)
        {
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
