using CalculatorAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CalculatorAPI.Data
{
    public class DataContext : DbContext, IDataContext
    {
        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<ProductModel> Products { get; set; }

        protected readonly IConfiguration Configuration;

        //public DataContext(DbContextOptions<DataContext> options) : base(options) 
        //{  

        //}
        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
        }
    }
}