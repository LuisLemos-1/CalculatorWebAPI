using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorAPI.Models
{
    [Table("Order")]
    public class OrderModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("idOrder", Order = 0)]
        public int IdOrder { get; set; }
        [Column("idProduct")]
        [ForeignKey("FK_Product_Order")]
        public int IdProduct { get; set; }
        [Column("idCustomer")]
        [ForeignKey("FK_Customer_Order")]
        public int IdCustomer { get; set; }
        [Column("date")]

        public DateTime? Date { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }
    }
}
