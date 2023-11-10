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
        public async Task<IActionResult> GetAllAsync()
        {
            var banks = await _bank.GetAllAsync();


            // Map to DTO
            var response = new List<BankDTO>();
            foreach(var crnt in banks)
            {
                response.Add(new BankDTO { Id = crnt.Id ,  Title = crnt.Title  });
            }
            return Ok(response);
        }

        // GET api/<BankApiController>/5
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        { 
            var existingBank =  await _bank.GetById(id);
            if(existingBank is null)
            {
                return NotFound();
            }
            var response = new BankDTO
            {
                Id = existingBank.Id,
                Title = existingBank.Title
            };
            return Ok(response);
        }

        // POST api/<BankApiController>
        [HttpPost]
        public async Task<IActionResult> Post(BankDTO bank)
        {
            //Map DTO
            var bnk = new Bank
            {
                Title = bank.Title,
                RegisterDate = DateTime.Now,
                LastChangeDate = DateTime.Now,
                Visable = true


            };
            await _bank.InsertAsync(bnk);
            var response = new BankDTO
            {
                Title = bnk.Title
            };
            return Ok(response);
        }

        // PUT api/<BankApiController>/5
        [HttpPut("{id}")]
        public bool Put([FromBody] Bank bank) => _bank.update(bank);

        // DELETE api/<BankApiController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id) => _bank.delete(id);
    }
}
