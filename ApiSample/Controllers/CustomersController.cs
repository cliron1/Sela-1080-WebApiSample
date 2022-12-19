using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiSample.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase {
	// GET: api/<CustomersController>
	[HttpGet]
	public IEnumerable<Customer> Get() {
		return new List<Customer>();
	}

	// GET api/<CustomersController>/5
	[HttpGet("{id}")]
	public Customer Get(int id) {
		return new Customer();
	}

	// POST api/<CustomersController>
	[HttpPost]
	public void Post([FromBody] Customer value) {
	}

	// PUT api/<CustomersController>/5
	[HttpPut("{id}")]
	public void Put(int id, [FromBody] Customer value) {
	}

	// DELETE api/<CustomersController>/5
	[HttpDelete("{id}")]
	public void Delete(int id) {
	}
}
