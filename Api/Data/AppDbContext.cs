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
        public DbSet<RootUrls> RootUrls { get; set; }
        public DbSet<Queries> Queries { get; set; }
        public DbSet<Files> Files { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Replace with your actual PostgreSQL connection string
            optionsBuilder.UseNpgsql("Host=speed_apply;Port=5432;Database=speed_apply;Username=postgres;Password=speed_apply");
        }

        // Use OnModelCreating for Fluent API configuration if needed
         protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<RootUrls>().ToTable("RootUrls");
            modelBuilder.Entity<Queries>().ToTable("Queries");

            modelBuilder.Entity<Queries>()
                .HasOne(q => q.User)            // Each Query has one User
                .WithMany(u => u.Queries)       // A User can have many Queries
                .HasForeignKey(q => q.UserId);  // Tied together by the UserId property

            modelBuilder.Entity<Files>().ToTable("Files");

            modelBuilder.Entity<Files>()
                .HasOne(q => q.User)            // Each File has one User
                .WithMany(u => u.Files)       // A User can have many Files
                .HasForeignKey(q => q.UserId);  // Tied together by the UserId property
        }
    }
}

