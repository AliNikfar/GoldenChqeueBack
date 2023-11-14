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
            [Route("{factorId:Guid}")]
            public async Task<IActionResult> GetByBankId(Guid BankId)
            {
                var shobe = await _shobe.GetByBankId(BankId);

                // Map to DTO
                var response = new List<ShobeDTO>();
                foreach (var crnt in shobe)
                {
                    response.Add(new ShobeDTO
                    {
                        Name = crnt.Name,
                        code = crnt.code
                    });
                }
                return Ok(response);
            }

        // GET api/<ShobeApiController>/5
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var existingShobe = await _shobe.GetById(id);
            if (existingShobe is null)
            {
                return NotFound();
            }
            var response = new ShobeDTO
            {
                Name = existingShobe.Name,
                code = existingShobe.code
            };
            return Ok(response);
        }

        // POST api/<ShobeApiController>
        [HttpPost]
        public async Task<IActionResult> Post(ShobeDTO shobe)
        {
            //Map DTO
            var shbe = new Shobe
            {
                Name = shobe.Name,
                code = shobe.code
            };

            //foreach(var item in cheque.Shobe)
            //{
            //var existing = await _ShobeRepository.GetById(cheque.Shobe);
            //}
            await _shobe.InsertAsync(shbe);
            var response = new ShobeDTO
            {
                Name = shbe.Name,
                code = shbe.code
            };
            return Ok(response);
        }


        // PUT api/<ShobeApiController>/5
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, UpdateShobeRequestDTO request)
        {
            //convert DTO to Domain Model
            var shobe = new Shobe
            {
                Id = id,
                Name = request.Name,
                code = request.code
            };
            shobe = await _shobe.UpdateAsync(shobe);
            if (shobe == null)
            {
                return NotFound();
            }

            var response = new ShobeDTO
            {
                Id = id,
                Name = request.Name,
                code = request.code
            };

            return Ok(response);
        }

        // DELETE api/<ShobeApiController>/5
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var shobe = await _shobe.DeleteAsync(id);
            if (shobe == null)
            {
                return NotFound();
            }
            var response = new ShobeDTO
            {
                Id = shobe.Id,
                Name = shobe.Name,
                code = shobe.code
            };
            return Ok(response);
        }
    }
}
