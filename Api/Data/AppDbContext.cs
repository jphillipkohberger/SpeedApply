using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SpeedApply.Api.Models;

namespace SpeedApply.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Replace with your actual PostgreSQL connection string
            optionsBuilder.UseNpgsql("Host=speed_apply;Port=5432;Database=speed_apply;Username=postgres;Password=speed_apply");
        }

        // Use OnModelCreating for Fluent API configuration if needed
         protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
             modelBuilder.Entity<Users>().ToTable("Users");
         }
    }
}

