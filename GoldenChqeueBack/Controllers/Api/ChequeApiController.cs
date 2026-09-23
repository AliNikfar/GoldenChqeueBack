using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Service.Contract;
using GoldenChequeBack.Service.Contract.DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GoldenChqeueBack.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChequeApiController : ControllerBase
    {
        private readonly IChequeRepository _cheque;
        private readonly ICustomerRepository _customerRepository;
        private readonly IShobeRepository _shobeRepository;
        private readonly IFactorRepository _factorRepository;

        public ChequeApiController(IChequeRepository cheque, ICustomerRepository customerRepository, IShobeRepository shobeRepository, IFactorRepository factorRepository)
        {
            _cheque = cheque;
            _customerRepository = customerRepository;
            _shobeRepository = shobeRepository;
            _factorRepository = factorRepository;
        }
        // GET: api/<ChequeApiController>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var cheque = await _cheque.GetAllAsync();


            // Map to DTO
            var response = new List<ChequeDTO>();
            foreach (var crnt in cheque)
            {
                response.Add(new ChequeDTO
                {
                    Id = crnt.Id,   
                    ChequeDate = crnt.ChequeDate,
                    ChequePrice = crnt.ChequePrice,
                    ChequeStatus =  crnt.ChequeStatus,
                    Detail = crnt.Detail,
                    FactorID = crnt.FactorId ?? Guid.Empty,
                    Kind = crnt.Kind,   
                    PassDate = crnt.PassDate,
                    SahabCheque = crnt.SahabChequeId,
                    Shobe = crnt.ShobeId,
                    ShomareChek = crnt.ShomareChek,
                    ShomareHesab = crnt.ShomareHesab
                });
            }
            return Ok(response);
        }

        // GET api/<ChequeApiController>/5
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var existingCheque = await _cheque.GetById(id);
            if (existingCheque is null)
            {
                return NotFound();
            }
            var response = new ChequeDTO
            {
                Id = existingCheque.Id,
                ChequeDate = existingCheque.ChequeDate,
                ChequePrice = existingCheque.ChequePrice,
                ChequeStatus = existingCheque.ChequeStatus,
                Detail = existingCheque.Detail,
                FactorID = existingCheque.FactorId ?? Guid.Empty,
                Kind = existingCheque.Kind,
                PassDate = existingCheque.PassDate,
                SahabCheque = existingCheque.SahabChequeId,
                Shobe = existingCheque.ShobeId,
                ShomareChek = existingCheque.ShomareChek,
                ShomareHesab = existingCheque.ShomareHesab
            };
            return Ok(response);
        }

        // POST api/<ChequeApiController>
        [HttpPost]
        public async Task<IActionResult> Post(ChequeDTO cheque)
        {
            //Map DTO - resolve relationships from the database instead of creating empty rows
            var chq = new Cheque
            {
                Kind = cheque.Kind,
                ShomareHesab = cheque.ShomareHesab,
                ShomareChek = cheque.ShomareChek,
                ChequeDate = cheque.ChequeDate,
                ChequeStatus = cheque.ChequeStatus,
                PassDate = cheque.PassDate,
                Detail = cheque.Detail,
                Visable = cheque.Visable,
                ChequePrice = cheque.ChequePrice
            };

            var existingCustomer = await _customerRepository.GetById(cheque.SahabCheque);
            if (existingCustomer is null)
            {
                return NotFound("مشتری صادرکننده چک یافت نشد");
            }
            chq.SahabCheque = existingCustomer;

            var existingShobe = await _shobeRepository.GetById(cheque.Shobe);
            if (existingShobe is null)
            {
                return NotFound("شعبه یافت نشد");
            }
            chq.Shobe = existingShobe;

            if (cheque.FactorID != Guid.Empty)
            {
                var existingFactor = await _factorRepository.GetById(cheque.FactorID);
                if (existingFactor is null)
                {
                    return NotFound("فاکتور یافت نشد");
                }
                chq.Factor = existingFactor;
            }

            await _cheque.InsertAsync(chq);
            var response = new ChequeDTO
            {
                Id = chq.Id,
                Kind = chq.Kind,
                ShomareHesab = chq.ShomareHesab,
                ShomareChek = chq.ShomareChek,
                SahabCheque = chq.SahabCheque.Id,
                Shobe = chq.Shobe.Id,
                ChequeDate = chq.ChequeDate,
                ChequeStatus = chq.ChequeStatus,
                PassDate = chq.PassDate,
                Detail = chq.Detail,
                FactorID = chq.FactorId ?? Guid.Empty,
                Visable = chq.Visable,
                ChequePrice = chq.ChequePrice
            };
            return Ok(response);
        }

        // PUT api/<ChequeApiController>/5
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, UpdateChequeRequestDTO request)
        {
            //convert DTO to Domain Model
            var chq = new Cheque
            {
                Id = id,
                Kind = request.Kind,
                ShomareHesab = request.ShomareHesab,
                ShomareChek = request.ShomareChek,
                ChequeDate = request.ChequeDate,
                ChequeStatus = request.ChequeStatus,
                PassDate = request.PassDate,
                Detail = request.Detail,
                Visable = request.Visable,
                ChequePrice = request.ChequePrice
            };
            chq = await _cheque.UpdateAsync(chq);
            if (chq == null)
            {
                return NotFound();
            }

            var response = new ChequeDTO
            {
                Id = id,
                Kind = chq.Kind,
                ShomareHesab = chq.ShomareHesab,
                ShomareChek = chq.ShomareChek,
                SahabCheque = chq.SahabChequeId,
                Shobe = chq.ShobeId,
                ChequeDate = chq.ChequeDate,
                ChequeStatus = chq.ChequeStatus,
                PassDate = chq.PassDate,
                Detail = chq.Detail,
                FactorID = chq.FactorId ?? Guid.Empty,
                Visable = chq.Visable,
                ChequePrice = chq.ChequePrice
            };

            return Ok(response);
        }

        // DELETE api/<ChequeApiController>/5
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var chq = await _cheque.DeleteAsync(id);
            if (chq == null)
            {
                return NotFound();
            }
            var response = new ChequeDTO
            {
                Id = id,
                Kind = chq.Kind,
                ShomareHesab = chq.ShomareHesab,
                ShomareChek = chq.ShomareChek,
                SahabCheque = chq.SahabChequeId,
                Shobe = chq.ShobeId,
                ChequeDate = chq.ChequeDate,
                ChequeStatus = chq.ChequeStatus,
                PassDate = chq.PassDate,
                Detail = chq.Detail,
                FactorID = chq.FactorId ?? Guid.Empty,
                Visable = chq.Visable,
                ChequePrice = chq.ChequePrice
            };
            return Ok(response);
        }
    }
}
