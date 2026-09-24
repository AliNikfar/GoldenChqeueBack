using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Service.Contract;
using GoldenChequeBack.Service.Contract.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GoldenChequeBack.Controllers.Api
{
    [Route("api/[controller]")]
    [Route("api/Shobe")]
    [ApiController]
    public class ShobeApiController : ControllerBase
    {
        private readonly IShobeRepository _shobe;
        private readonly IBankRepository _bank;

        public ShobeApiController(IShobeRepository shobe, IBankRepository bank)
        {
            _shobe = shobe;
            _bank = bank;
        }

        // GET: api/Shobe  (all branches)
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var shobe = await _shobe.GetAllAsync();

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
                    Details = crnt.Details,
                    BankId = crnt.Bank?.Id
                });
            }
            return Ok(response);
        }

        // GET: api/Shobe/bank/{bankId}  (branches of one bank)
        [HttpGet("bank/{bankId:Guid}")]
        public async Task<IActionResult> GetByBankId([FromRoute] Guid bankId)
        {
            var shobe = await _shobe.GetByBankId(bankId);

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
                    Details = crnt.Details,
                    BankId = crnt.Bank?.Id
                });
            }
            return Ok(response);
        }

        // GET api/Shobe/{id}
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var existingShobe = await _shobe.GetById(id);
            if (existingShobe is null)
            {
                return NotFound();
            }
            var response = new ShobeDTO
            {
                Id = existingShobe.Id,
                Name = existingShobe.Name,
                Code = existingShobe.Code,
                Address = existingShobe.Address,
                Phone = existingShobe.Phone,
                Details = existingShobe.Details,
                BankId = existingShobe.Bank?.Id
            };
            return Ok(response);
        }

        // POST api/Shobe
        [HttpPost]
        public async Task<IActionResult> Post(ShobeDTO shobe)
        {
            // resolve the parent bank from the database (never create a fake Bank row)
            var bank = await _bank.GetById(shobe.BankId ?? Guid.Empty);
            if (bank is null)
            {
                return NotFound("بانک یافت نشد");
            }

            var st = new Shobe
            {
                Name = shobe.Name,
                Code = shobe.Code,
                Address = shobe.Address,
                Phone = shobe.Phone,
                Details = shobe.Details,
                Bank = bank
            };
            await _shobe.InsertAsync(st);

            var response = new ShobeDTO
            {
                Id = st.Id,
                Name = st.Name,
                Code = st.Code,
                Address = st.Address,
                Phone = st.Phone,
                Details = st.Details,
                BankId = st.Bank.Id
            };
            return Ok(response);
        }

        // PUT api/Shobe/{id}
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, ShobeDTO request)
        {
            var existingShobe = await _shobe.GetById(id);
            if (existingShobe is null)
            {
                return NotFound();
            }

            existingShobe.Name = request.Name;
            existingShobe.Code = request.Code;
            existingShobe.Address = request.Address;
            existingShobe.Phone = request.Phone;
            existingShobe.Details = request.Details;
            if (request.BankId.HasValue && request.BankId.Value != existingShobe.Bank?.Id)
            {
                var bank = await _bank.GetById(request.BankId.Value);
                if (bank is null)
                {
                    return NotFound("بانک یافت نشد");
                }
                existingShobe.Bank = bank;
            }

            var updated = await _shobe.UpdateAsync(existingShobe);
            if (updated is null)
            {
                return NotFound();
            }

            var response = new ShobeDTO
            {
                Id = updated.Id,
                Name = updated.Name,
                Code = updated.Code,
                Address = updated.Address,
                Phone = updated.Phone,
                Details = updated.Details,
                BankId = updated.Bank?.Id
            };
            return Ok(response);
        }

        // DELETE api/Shobe/{id}
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
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
                Code = shobe.Code,
                Address = shobe.Address,
                Phone = shobe.Phone,
                Details = shobe.Details,
                BankId = shobe.Bank?.Id
            };
            return Ok(response);
        }
    }
}
