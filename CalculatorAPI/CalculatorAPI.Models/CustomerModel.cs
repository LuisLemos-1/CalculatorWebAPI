using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalculatorAPI.Models
{
    [Table("Customer")]
    public class CustomerModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("idCustomer", Order=0)]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("dob")]
        public DateTime? DOB { get; set; }

    }
}
