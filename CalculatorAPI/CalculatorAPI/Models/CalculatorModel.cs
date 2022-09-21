using System.ComponentModel.DataAnnotations;

namespace CalculatorAPI.Models
{
    public class CalculatorModel
    {
        [Required]
        public int firstValue { get; set; }

        [Required]
        public int secondValue { get; set; }

    }
}
