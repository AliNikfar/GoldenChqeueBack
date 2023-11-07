using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Service.Contract;
using GoldenChequeBack.Service.Contract.DTO;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GoldenChqeueBack.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactorApiController : ControllerBase
    {
        private readonly IFactorRepository _factor;

        public FactorApiController(IFactorRepository factor)
        {
            _factor = factor;
        }
        // GET: api/<FactorApiController>
        [HttpGet]
        public List<Factor> Getall() => _factor.GetAll();

        // GET api/<FactorApiController>/5
        [HttpGet("{id}")]
        public Factor Get(int id) => _factor.GetById(id);

        // POST api/<FactorApiController>
        [HttpPost]
        public async Task<IActionResult> Post(FactorDTO factor)
        {
            //Map DTO
            var fct = new Factor
            {
                //CustomerId = factor.CustomerId,
                FactorSumPrice = factor.FactorSumPrice,
                FactorSodDarsad = factor.FactorSodDarsad,
                FactorKharidDate = factor.FactorKharidDate,
                FactorSumObjectsPrice = factor.FactorSumObjectsPrice,
                Kind = factor.Kind,
                FactorBeforePrice = factor.FactorBeforePrice,
                //FactorObjectList = factor.FactorObjectList,
                //GhestList = factor.GhestList

            };
            await _factor.InsertAsync(fct);
            var response = new FactorDTO
            {
                //CustomerId = fct.CustomerId,
                FactorSumPrice = fct.FactorSumPrice,
                FactorSodDarsad = fct.FactorSodDarsad,
                FactorKharidDate = fct.FactorKharidDate,
                FactorSumObjectsPrice = fct.FactorSumObjectsPrice,
                Kind = fct.Kind,
                FactorBeforePrice = fct.FactorBeforePrice,
                //FactorObjectList = fct.FactorObjectList
                //GhestList = fct.GhestList
            };
            return Ok(response);
        }

        // PUT api/<FactorApiController>/5
        [HttpPut("{id}")]
        public bool Put([FromBody] Factor factor) => _factor.update(factor);

        // DELETE api/<FactorApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) => _factor.delete(id);
    }
}
