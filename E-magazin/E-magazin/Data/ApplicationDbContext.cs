using E_magazin.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_magazin.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CashDesk> CashDesks { get; set; }
        public DbSet<SoldProduct> SoldProducts { get; set; }
        public DbSet<RestockProduct> RestockProducts { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }
    }
}