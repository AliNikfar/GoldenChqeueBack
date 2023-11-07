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
        public ChequeApiController( IChequeRepository cheque)
        {
            _cheque = cheque;
        }
        // GET: api/<ChequeApiController>
        [HttpGet]
        public List<Cheque> Get() => _cheque.GetAll();

        // GET api/<ChequeApiController>/5
        [HttpGet("{id}")]
        public Cheque Get(int id) => _cheque.GetById(id);

        // POST api/<ChequeApiController>
        [HttpPost]
        public async Task<IActionResult> Post(ChequeDTO cheque)
        {
            //Map DTO
            var chq = new Cheque
            {
                Kind = cheque.Kind,
                ShomareHesab = cheque.ShomareHesab,
                ShomareChek = cheque.ShomareChek,
                //SahabCheque = cheque.SahabCheque,
                //Shobe = cheque.Shobe,
                ChequeDate = cheque.ChequeDate,
                ChequeStatus = cheque.ChequeStatus,
                PassDate = cheque.PassDate,
                Detail = cheque.Detail,
                //FactorID = cheque.FactorID,
                Visable = cheque.Visable,
                ChequePrice = cheque.ChequePrice


            };
            await _cheque.InsertAsync(chq);
            var response = new ChequeDTO
            {
                Kind = chq.Kind,
                ShomareHesab = chq.ShomareHesab,
                ShomareChek = chq.ShomareChek,
                //SahabCheque = chq.SahabCheque,
                //Shobe = chq.Shobe,
                ChequeDate = chq.ChequeDate,
                ChequeStatus = chq.ChequeStatus,
                PassDate = chq.PassDate,
                Detail = chq.Detail,
                //FactorID = chq.FactorID,
                Visable = chq.Visable,
                ChequePrice = chq.ChequePrice
            };
            return Ok(response);
        }

        // PUT api/<ChequeApiController>/5
        [HttpPut("{id}")]
        public bool Update([FromBody] Cheque cheque)=> _cheque.update(cheque);

        // DELETE api/<ChequeApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) => _cheque.delete(id);
    }
}
