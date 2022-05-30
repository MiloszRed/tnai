using System.Linq;

using TNAI.Model.Entities;

namespace TNAI.Model.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<TNAI.Model.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TNAI.Model.AppDbContext context)
        {

        }
    }
}
