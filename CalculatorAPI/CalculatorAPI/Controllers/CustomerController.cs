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
        [HttpGet("{id}", Name = "GetCustomerById")]
        public IActionResult GetCustomerById([FromRoute] int id)
        {
            Customer findCostumer = _customers.Find(e => e.Id == id);
            return (findCostumer == null) ? NotFound() : Ok(findCostumer);
        }

        // POST api/Customer
        [HttpPost]
        public IActionResult Post([FromBody] Customer newCustomer)
        {
            try
            {
                newCustomer.Id = _customers.Max(e => e.Id) + 1;
                _customers.Add(newCustomer);
                return CreatedAtAction(nameof(GetCustomerById), new { id = newCustomer.Id }, newCustomer);
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // PUT api/Customer/id
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Customer editedCustomer)
        {
            Customer findCostumer = _customers.Find(e => e.Id == id);
            if (findCostumer == null)
                return NotFound();

            try
            {
                editedCustomer.Id = id;
                _customers[_customers.FindIndex(e => e.Id == id)] = editedCustomer;
                //return CreatedAtAction(nameof(GetCustomerById), new { id = findCostumer.Id}, editedCustomer);
                return NoContent();
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
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
