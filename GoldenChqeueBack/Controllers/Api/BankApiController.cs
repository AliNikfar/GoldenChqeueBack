 using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Service.Contract;
using GoldenChequeBack.Service.Contract.DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GoldenChqeueBack.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankApiController : ControllerBase
    {
        private readonly IBankRepository _bank;
        public BankApiController(IBankRepository bank)
        {
            _bank = bank;
        }
        // GET: api/<BankApiController>
        [HttpGet]
        public List<Bank> GetAll() => _bank.GetAll();

        // GET api/<BankApiController>/5
        [HttpGet("{id}")]
        public Bank Get(int id) => _bank.GetById(id);

        // POST api/<BankApiController>
        [HttpPost]
        public async Task<IActionResult> Post(BankDTO bank)
        {
            //Map DTO
            var bnk = new Bank
            {
                Title = bank.Title
            };
            await _bank.InsertAsync(bnk);
            var response = new BankDTO
            {
                Title = bank.Title
            };
            return Ok(response);
        }

        // PUT api/<BankApiController>/5
        [HttpPut("{id}")]
        public bool Put([FromBody] Bank bank) => _bank.update(bank);

        // DELETE api/<BankApiController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id) => _bank.delete(id);
    }
}
