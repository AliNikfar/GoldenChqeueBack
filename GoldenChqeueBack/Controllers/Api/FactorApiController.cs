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
        public async Task<IActionResult> GetAllAsync()
        {
            var factor = await _factor.GetAllAsync();


            // Map to DTO
            var response = new List<FactorDTO>();
            foreach (var crnt in factor)
            {
                response.Add(new FactorDTO
                {
                    Id = crnt.Id,
                    PersonCode = crnt.Customer.Id,
                    FactorSumPrice = crnt.FactorSumPrice,
                    FactorSodDarsad = crnt.FactorSodDarsad,
                    FactorKharidDate = crnt.FactorKharidDate,
                    FactorSumObjectsPrice = crnt.FactorSumObjectsPrice,
                    Kind = crnt.Kind,
                    FactorBeforePrice = crnt.FactorBeforePrice
                    //FactorObjectList = crnt.FactorObjectList.ToList(),
                    //GhestList = crnt.GhestList
                });
            }
            return Ok(response);
        }

        // GET api/<FactorApiController>/5
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var existingfactor = await _factor.GetById(id);
            if (existingfactor is null)
            {
                return NotFound();
            }
            var response = new FactorDTO
            {
                Id = existingfactor.Id,
                PersonCode = existingfactor.Customer.Id,
                FactorSumPrice = existingfactor.FactorSumPrice,
                FactorSodDarsad = existingfactor.FactorSodDarsad,
                FactorKharidDate = existingfactor.FactorKharidDate,
                FactorSumObjectsPrice = existingfactor.FactorSumObjectsPrice,
                Kind = existingfactor.Kind,
                FactorBeforePrice = existingfactor.FactorBeforePrice
                //FactorObjectList = crnt.FactorObjectList.ToList(),
                //GhestList = crnt.GhestList
            };
            return Ok(response);
        }

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
        public void Delete(Guid id) => _factor.delete(id);
    }
}
