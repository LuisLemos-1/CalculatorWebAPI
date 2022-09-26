using CalculatorAPI.Contracts;
using CalculatorAPI.Domain;
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
        private readonly IServiceDomain domain;
 
        public CustomerController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
            domain = new ServiceDomain();
            
        }

        // GET: api/customer
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(domain.GetAll());
        }

        // GET: api/Customer/id
        [HttpGet("{id}", Name = "GetCustomerById")]
        public IActionResult GetCustomerById([FromRoute] int id)
        {
            Customer findCostumer = domain.GetCustomerById(id);
            return (findCostumer == null) ? NotFound() : Ok(findCostumer);
        }

        // POST api/Customer
        [HttpPost]
        public IActionResult Post([FromBody] Customer newCustomer)
        {
            var addedCustomer = domain.AddCustomer(newCustomer);

            if(addedCustomer != null) return CreatedAtAction(nameof(GetCustomerById), new { id = addedCustomer.Id }, addedCustomer);
            
            return Problem("Problem occurred while adding Customer");
        }

        // PUT api/Customer/id
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Customer editedCustomer)
        {
            if(domain.UpdateCustomer(id, editedCustomer)) return NoContent();
            
            return Problem("Problem occurred while updating Customer");
        }

        // DELETE api/Customer/id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (domain.DeleteCustomerById(id)) return Ok();

            return NotFound();
        }
    }
}
