using CalculatorAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalculatorAPI.Data
{
    public class DataContext : DbContext, IDataContext
    {
        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<ProductModel> Products { get; set; }

        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public int saveDB()
        {
            return base.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerModel>()
                .HasKey(c => new { c.Id });
            modelBuilder.Entity<OrderModel>()
                .HasKey(c => new { c.IdOrder });
            modelBuilder.Entity<ProductModel>()
                .HasKey(c => new { c.Id });

        }
    }
}