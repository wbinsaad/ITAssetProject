using ITAssetProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ITAssetProject.Database
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("name=ConnectionStrings:DefaultConnection");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ITAsset>(item =>
            {
                // Is Unique & Index
                item.HasIndex(n => n.SerialNumber).IsUnique(true);


                // Enums
                item.Property(n => n.Status).HasConversion<EnumToStringConverter<StatusEnum>>();


                // Max Length
                item.Property(n => n.SerialNumber).HasMaxLength(128).IsRequired(true);
                item.Property(n => n.Brand).HasMaxLength(128).IsRequired(true);
                item.Property(n => n.Status).HasMaxLength(128).IsRequired(true);
            });
        }

        public DbSet<ITAsset> ITAsset { get; set; }
    }
}
