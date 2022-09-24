using System.ComponentModel.DataAnnotations;

namespace CalculatorAPI.Contracts
{
    public class CalculatorModel
    {
        [Required]
        public int firstValue { get; set; }

        [Required]
        public int secondValue { get; set; }

    }
}
