 using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Service.Contract;
using GoldenChequeBack.Service.Contract.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GoldenChqeueBack.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BankApiController : ControllerBase
    {
        private readonly IBankRepository _bank;
        public BankApiController(IBankRepository bank)
        {
            _bank = bank;
        }


        // GET: api/<BankApiController>
        //[Authorize(Roles = "Reader")]
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
        //[Authorize(Roles = "Reader")]
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
                Title = existingBank.Title,
                ShobeList = existingBank.ShobeList.Select(s => new ShobeDTO
                {
                    Address = s.Address,
                    Code = s.Code,
                    Details = s.Details,
                    Id = s.Id,
                    Name = s.Name,
                    Phone = s.Phone

                }).ToList()
            };
            return Ok(response);
        }

        // POST api/<BankApiController>
        [HttpPost]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> Post(CreateBankRequestDTO bank)
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
            var response = new CreateBankRequestDTO
            {
                Title = bnk.Title
            };
            return Ok(response);
        }

        // PUT api/<BankApiController>/5

        [HttpPut]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id,UpdateBankRequestDTO request)
        {
            //convert DTO to Domain Model
            var bank = new Bank
            {
                Id = id,
                Title = request.Title
            };
            bank = await _bank.UpdateAsync(bank);
            if (bank == null)
            {
                return NotFound();
            }

            var response = new BankDTO {
                Id = bank.Id,
                Title= bank.Title
            };

            return Ok(response);
        }

        // DELETE api/<BankApiController>/5
        [HttpDelete]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var bank = await _bank.DeleteAsync(id);
            if (bank == null)
            {
                return NotFound();
            }
            var response = new BankDTO
            {
                Id = bank.Id,
                Title = bank.Title
            };
            return Ok(response);
        }

        }
    }

