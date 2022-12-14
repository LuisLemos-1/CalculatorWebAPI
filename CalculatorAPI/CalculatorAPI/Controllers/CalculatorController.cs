using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CalculatorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        // GET: api/Calculator/Sum/1/2
        [HttpGet("Sum/{val1}/{val2}")]
        public int Sum([FromRoute] int val1, int val2)
        {
            return val1 + val2;
        }

        // GET: api/Calculator/Substract/1/2
        [HttpGet("Substract/{val1}/{val2}")]
        public int Substract([FromRoute] int val1, int val2)
        {
            return val1 - val2;
        }

        // GET: api/Calculator/Multiply/1/2
        [HttpGet("Multiply/{val1}/{val2}")]
        public int Multiply([FromRoute] int val1, int val2)
        {
            return val1 * val2;
        }

        // GET: api/Calculator/Divide/1/2
        [HttpGet("Divide/{val1}/{val2}")]
        public double Divide([FromRoute] double val1, double val2)
        {
            return (val2 == 0) ? 0 : (val1 / val2);
        }

        // GET: api/Calculator/Power/1/2
        [HttpGet("Power/{val1}/{val2}")]
        public double Power([FromRoute] double val1, double val2)
        {
            return Math.Pow(val1, val2);
        }

        // GET: api/Calculator/SquareRoot/1/2
        [HttpGet("SquareRoot/{val1}")]
        public double SquareRoot([FromRoute] double val1)
        {
            return Math.Sqrt(val1);
        }

        // GET: api/<CalculatorController>
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CalculatorController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CalculatorController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CalculatorController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CalculatorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
