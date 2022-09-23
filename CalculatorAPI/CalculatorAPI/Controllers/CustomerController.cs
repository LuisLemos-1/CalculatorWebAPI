using CalculatorAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CalculatorController> _logger;
        private readonly List<Customer> _customers;
 
        public CustomerController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
            _customers = new List<Customer>
            {
                new Customer
                {
                    Id = 1,
                    Name = "Jose Manuel",
                    DOB = new DateTime(1980, 7, 3)
                },
                new Customer
                {
                    Id = 2,
                    Name = "Jorge Castro",
                    DOB = new DateTime(1981, 9, 3)
                },
                new Customer
                {
                    Id = 3,
                    Name = "Catarina Costa",
                },
            };
        }

        // GET: api/customer
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_customers);
        }
    }
}
