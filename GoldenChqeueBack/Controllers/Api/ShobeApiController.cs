using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Service.Contract;
using GoldenChequeBack.Service.Contract.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GoldenChqeueBack.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShobeApiController : ControllerBase
    {
        private readonly IShobeRepository _shobe;

        public ShobeApiController(IShobeRepository shobe)
        {
            _shobe = shobe;
        }
        // GET: api/<ShobeApiController>
        [HttpGet]
        public List<Shobe> GetByBankId(int BankId) => _shobe.GetByBankId(BankId);

        // GET api/<ShobeApiController>/5
        [HttpGet("{id}")]
        public Shobe Get(int id) => _shobe.GetById(id);

        // POST api/<ShobeApiController>
        [HttpPost]
        public async Task<IActionResult> Post(ShobeDTO shobe)
        {
            //Map DTO
            var sh = new Shobe
            {
                Name = shobe.Name,
                code = shobe.code
    };
            await _shobe.InsertAsync(sh);
            var response = new ShobeDTO
            {
                Name = sh.Name,
                code = sh.code
            };
            return Ok(response);
        }

        // PUT api/<ShobeApiController>/5
        [HttpPut("{id}")]
        public bool Put([FromBody] Shobe shobe) => _shobe.update(shobe);

        // DELETE api/<ShobeApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) => _shobe.delete(id);
    }
}
