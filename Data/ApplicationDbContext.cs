using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TarifPaylasim.Models;

namespace TarifPaylasim.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Recipes> Recipe { get; set; }
        public DbSet<Comment> Comment { get; set; }

        public DbSet<Portfolio> Portfolio { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Portfolio>(c=> c.HasKey(a => new { a.AppUserId, a.RecipeId }));

            builder.Entity<Portfolio>()
                .HasOne(a => a.AppUser)
                .WithMany(a => a.Portfolios)
                .HasForeignKey(a => a.AppUserId);

            builder.Entity<Portfolio>()
                .HasOne(a => a.Recipe)
                .WithMany(a => a.Portfolios)
                .HasForeignKey(a => a.RecipeId);
            

            List<IdentityRole> roles = new List<IdentityRole>
            {
               new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },

                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                }

            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}