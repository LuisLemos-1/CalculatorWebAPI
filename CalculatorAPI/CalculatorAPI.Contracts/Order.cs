namespace CalculatorAPI.Contracts
{
    public class Order
    {
        public int IdOrder { get; set; }

        public int IdProduct { get; set; }

        public int IdCustomer { get; set; }

        public DateTime? Date { get; set; }
        public int Quantity { get; set; }
    }
}
