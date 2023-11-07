using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Service.Contract;
using GoldenChequeBack.Service.Contract.DTO;
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
        public async Task<IActionResult> Post(CustomerDTO customer)
        {
            //Map DTO
            var cus = new Customer
            {
                Code = customer.Code,
                Name = customer.Name,
                LastName = customer.LastName,
                FatherName = customer.FatherName,
                PhoneNum = customer.PhoneNum,
                Mob1 = customer.Mob1,
                Mob2 = customer.Mob2,   
                Mob3 = customer.Mob3,   
                //City = customer.City,
                Address = customer.Address,
                PostalCode = customer.PostalCode,
                Details = customer.Details,
                MaxBuyPrice = customer.MaxBuyPrice,
                BirthDate = customer.BirthDate,
                //CustomerRate = customer.CustomerRate

            };
            await _customer.InsertAsync(cus);
            var response = new CustomerDTO
            {
                Code = cus.Code,
                Name = cus.Name,
                LastName = cus.LastName,
                FatherName = cus.FatherName,
                PhoneNum = cus.PhoneNum,
                Mob1 = cus.Mob1,
                Mob2 = cus.Mob2,
                Mob3 = cus.Mob3,
                //City = cus.City,
                Address = cus.Address,
                PostalCode = cus.PostalCode,
                Details = cus.Details,
                MaxBuyPrice = cus.MaxBuyPrice,
                BirthDate = cus.BirthDate,
                //CustomerRate = cus.CustomerRate
            };
            return Ok(response);
        }

        // PUT api/<CustomerApiController>/5
        [HttpPut("{id}")]
        public bool Put( [FromBody] Customer cust) => _customer.update(cust);

        // DELETE api/<CustomerApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) => _customer.delete(id);
    }
}
