using CalculatorAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        // GET: api/Customer/id
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            Customer findCostumer = _customers.Find(e => e.Id == id);
            return (findCostumer == null) ? NotFound() : Ok(findCostumer);

            //return _customers.Find(e => e.Id == id) == null ? NotFound() : Ok(_customers.Find(e => e.Id == id));
        }

        // POST api/Customer
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            try
            {
                Customer customer = JsonConvert.DeserializeObject<Customer>(value);

                // Tested with
                // "{\"Id\":4,\"Name\":\"Ana Rodrigues\",\"DOB\":\"02/10/1990 00:00:00\"}"
                _customers.Add(customer);
                return Ok(customer);
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        // PUT api/Customer/id
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            Customer findCostumer = _customers.Find(e => e.Id == id);
            if (findCostumer == null)
                return NotFound();

            try
            {
                Customer updateCustomer = JsonConvert.DeserializeObject<Customer>(value);

                // Tested with
                // "{\"Id\":1,\"Name\":\"Jose Manuel\",\"DOB\":\"02/10/1990 00:00:00\"}"
                _customers[_customers.FindIndex(e => e.Id == id)] = updateCustomer;
                return Ok(updateCustomer);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        // DELETE api/Customer/id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Customer findCostumer = _customers.Find(e => e.Id == id);
            if (findCostumer == null)
                return NotFound();

            _customers.Remove(findCostumer);
            return Ok();
        }
    }
}
