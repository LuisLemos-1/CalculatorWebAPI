using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorAPI.Models
{
    public class OrderModel
    {
        public int IdOrder { get; set; }

        public int IdProduct { get; set; }

        public int IdCustomer { get; set; }

        public DateTime? Date { get; set; }
        public int Quantity { get; set; }
    }
}
