using CalculatorAPI.Contracts;
using CalculatorAPI.Domain;
using CalculatorAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Dynamic;

namespace CalculatorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CalculatorController> _logger;
        private readonly IServiceDomain _domain;
 
        public CustomerController(ILogger<CalculatorController> logger, IServiceDomain domain)
        {
            _logger = logger;
            _domain = domain;
            
        }

        // GET: api/customer
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_domain.GetAll());
        }

        // GET: api/Customer/id
        [HttpGet("{id}", Name = "GetCustomerById")]
        public IActionResult GetCustomerById([FromRoute] int id)
        {
            Customer findCostumer = _domain.GetCustomerById(id);
            return (findCostumer == null) ? NotFound() : Ok(findCostumer);
        }

        // POST api/Customer
        [HttpPost]
        public IActionResult Post([FromBody] Customer newCustomer)
        {
            var addedCustomer = _domain.AddCustomer(newCustomer);

            if(addedCustomer != null) return CreatedAtAction(nameof(GetCustomerById), new { id = addedCustomer.Id }, addedCustomer);
            
            return Problem("Problem occurred while adding Customer");
        }

        // PUT api/Customer/id
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Customer editedCustomer)
        {
            if(_domain.UpdateCustomer(id, editedCustomer)) return NoContent();
            
            return Problem("Problem occurred while updating Customer");
        }

        // DELETE api/Customer/id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_domain.DeleteCustomerById(id)) return Ok();

            return NotFound();
        }

        // GET api/Customer/Order/Product/id
        [HttpGet("Order/Product/{id}")]
        public IActionResult GetCustomersByIdProduct(int id)
        {
            var result = _domain.GetCustomersByIdProduct(id);

            return (result.ToList().Count == 0) ? NotFound() : Ok(result.ToList());
        }
    }
}
