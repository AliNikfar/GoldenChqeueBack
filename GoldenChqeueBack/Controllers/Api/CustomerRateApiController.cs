using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Service.Contract;
using GoldenChequeBack.Service.Contract.DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GoldenChqeueBack.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerRateApiController : ControllerBase
    {
        private readonly ICustomerRateRepository _customerRate;

        public CustomerRateApiController(ICustomerRateRepository customerRate)
        {
            _customerRate = customerRate;
        }
        // GET: api/<CustomerRateApiController>
        [HttpGet]
        public List<CustomerRate> Getall() => _customerRate.GetAll();

        // GET api/<CustomerRateApiController>/5
        [HttpGet("{id}")]
        public CustomerRate Get(int id) => _customerRate.GetById(id);

        // POST api/<CustomerRateApiController>
        [HttpPost]
        public async Task<IActionResult> Post(CustomerRateDTO customRate)
        {
            //Map DTO
            var cr = new CustomerRate
            {
                Title = customRate.Title

            };
            await _customerRate.InsertAsync(cr);
            var response = new CustomerRateDTO
            {
                Title = cr.Title
                //Ostan = ct.Ostan
            };
            return Ok(response);
        }

        // PUT api/<CustomerRateApiController>/5
        [HttpPut("{id}")]
        public bool Put([FromBody] CustomerRate cust) => _customerRate.update(cust);

        // DELETE api/<CustomerRateApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) => _customerRate.delete(id);
    }
}

