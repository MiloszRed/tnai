using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNAI.Model.Configurations;
using TNAI.Model.Entities;
using TNAI.Model.Identity;

namespace TNAI.Model
{
    /// <summary>
    /// Komunikacja z bazą danych.
    /// </summary>
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        // Nazwy tych własności będą nazwami tabeli w bazie danych
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public AppDbContext() : base("AppConnection", throwIfV1Schema: false) // AppConnection - pole name z connectionStringa z Web.Config w API.
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PostConfiguration());
            modelBuilder.Configurations.Add(new CommentConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
    }
}
