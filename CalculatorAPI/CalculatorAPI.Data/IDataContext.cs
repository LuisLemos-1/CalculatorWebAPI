using CalculatorAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorAPI.Data
{
    public interface IDataContext
    {
        DbSet<CustomerModel> Customers { get; set; }
        DbSet<OrderModel> Orders { get; set; }
        DbSet<ProductModel> Products { get; set; }
        int saveDB();

    }
}
