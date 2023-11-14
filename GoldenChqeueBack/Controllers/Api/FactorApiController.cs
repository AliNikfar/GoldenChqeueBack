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
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, UpdateFactorRequestDTO request)
        {
            //convert DTO to Domain Model
            var factor = new Factor
            {
                Id = id,
                FactorSumPrice = request.FactorSumPrice,
                FactorSodDarsad = request.FactorSodDarsad,
                FactorKharidDate = request.FactorKharidDate,
                FactorSumObjectsPrice = request.FactorSumObjectsPrice,
                Kind = request.Kind,
                FactorBeforePrice = request.FactorBeforePrice,
                //FactorObjectList = request.FactorObjectList
                //GhestList = request.GhestList
            };
            factor = await _factor.UpdateAsync(factor);
            if (factor == null)
            {
                return NotFound();
            }

            var response = new FactorDTO
            {
                //CustomerId = fct.CustomerId,
                FactorSumPrice = factor.FactorSumPrice,
                FactorSodDarsad = factor.FactorSodDarsad,
                FactorKharidDate = factor.FactorKharidDate,
                FactorSumObjectsPrice = factor.FactorSumObjectsPrice,
                Kind = factor.Kind,
                FactorBeforePrice = factor.FactorBeforePrice,
                //FactorObjectList = factor.FactorObjectList
                //GhestList = factor.GhestList
            };

            return Ok(response);
        }

        // DELETE api/<FactorApiController>/5
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var factor = await _factor.DeleteAsync(id);
            if (factor == null)
            {
                return NotFound();
            }
            var response = new FactorDTO
            {
                Id = factor.Id,
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
            return Ok(response);
        }
    }
}
