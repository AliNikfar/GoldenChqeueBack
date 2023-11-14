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
        public async Task<IActionResult> GetAllAsync()
        {
            var cheque = await _customerRate.GetAllAsync();


            // Map to DTO
            var response = new List<CustomerRateDTO>();
            foreach (var crnt in cheque)
            {
                response.Add(new CustomerRateDTO
                {
                    Id = crnt.Id,
                    Title = crnt.Title
                }); 
            }
            return Ok(response);
        }

        // GET api/<CustomerRateApiController>/5
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var existingcustomerRate = await _customerRate.GetById(id);
            if (existingcustomerRate is null)
            {
                return NotFound();
            }
            var response = new CustomerRateDTO
            {
                Id = existingcustomerRate.Id,
                Title = existingcustomerRate.Title
            };
            return Ok(response);
        }

    }
}

