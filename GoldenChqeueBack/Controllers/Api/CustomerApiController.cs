using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Service.Contract;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GoldenChqeueBack.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private readonly ICustomerRepository _customer;

        public CustomerApiController(ICustomerRepository customer)
        {
            _customer = customer;
        }
        // GET: api/<CustomerApiController>
        [HttpGet]
        public   List<Customer> Getall() => _customer.GetAll(); 

        // GET api/<CustomerApiController>/5
        [HttpGet("{id}")]
        public Customer Get(int id) => _customer.GetById(id);   

        // POST api/<CustomerApiController>
        [HttpPost]
        public bool Post([FromBody] Customer cust) => _customer.Insert(cust); 

        // PUT api/<CustomerApiController>/5
        [HttpPut("{id}")]
        public bool Put( [FromBody] Customer cust) => _customer.update(cust);

        // DELETE api/<CustomerApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) => _customer.delete(id);
    }
}
