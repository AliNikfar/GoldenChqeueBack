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
        private readonly IBankRepository _bank;

        public ShobeApiController(IShobeRepository shobe,IBankRepository bank)
        {
            _shobe = shobe;
            _bank = bank;

        }
        // GET: api/<ShobeApiController>
            [HttpGet]
            [Route("{BankId:Guid}")]
            public async Task<IActionResult> GetByBankId([FromRoute] Guid BankId)
            {
                var shobe = await _shobe.GetByBankId(BankId);

                // Map to DTO
                var response = new List<ShobeDTO>();
                foreach (var crnt in shobe)
                {
                    response.Add(new ShobeDTO
                    {
                        Id = crnt.Id,
                        Name = crnt.Name,
                        Code = crnt.Code,
                        Address = crnt.Address,
                        Phone = crnt.Phone,
                        Details = crnt.Details
                    });
                }
                return Ok(response);
            }

        // GET api/<ShobeApiController>/5
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id,int lid)
        {
            var a = lid;
            var existingShobe = await _shobe.GetById(id);
            if (existingShobe is null)
            {
                return NotFound();
            }
            var response = new ShobeDTO
            {
                Name = existingShobe.Name,
                Code = existingShobe.Code,
                Address = existingShobe.Address,
                Phone = existingShobe.Phone,
                Details = existingShobe.Details
            };
            return Ok(response);
        }

        // POST api/<ShobeApiController>
        [HttpPost]
        public async Task<IActionResult> Post(CreateShobeRequestDTO shobe)
        {
            //Map DTO
            var shbe = new Shobe
            {
                Name = shobe.Name,
                Code = shobe.Code,
                Address = shobe.Address,
                Phone = shobe.Phone,
                Details = shobe.Details,
                LastChangeDate = DateTime.Now,
                Visable = true,
                RegisterDate = DateTime.Now,
                RegisterUser = 1 ,
                LastChangeUser = 1,
                Bank = new Bank(),
                Author = true,
            };
            var ExistingBank = _bank.GetById(shobe.BankId);
            if (ExistingBank is not null)
            {
                shbe.Bank = ExistingBank.Result;
            }
            else
            {
                return NotFound("اطلاعات بانک یافت نشد");
            }


            //foreach(var item in cheque.Shobe) 
            //{
            //var existing = await _ShobeRepository.GetById(cheque.Shobe);
            //}
            await _shobe.InsertAsync(shbe);
            var response = new ShobeDTO
            {
                Name = shbe.Name,
                Code = shbe.Code,
                Address = shbe.Address,
                Phone = shbe.Phone,
                Details = shbe.Details
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
                Code = request.Code,
                Address = request.Address,
                Phone = request.Phone,
                Details = request.Details
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
                Code = request.Code,
                Address = request.Address,
                Phone = request.Phone,
                Details = request.Details
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
                Code = shobe.Code
            };
            return Ok(response);
        }
    }
}
