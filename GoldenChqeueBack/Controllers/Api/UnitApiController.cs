using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Service.Contract;
using GoldenChequeBack.Service.Contract.DTO;
using GoldenChequeBack.Service.Implementation;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GoldenChqeueBack.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitApiController : ControllerBase
    {
        private readonly IUnitsRepository _unit;
        public UnitApiController(IUnitsRepository unit)
        {
            _unit = unit;
        }
        // GET: api/<UnitApiController>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var unit = await _unit.GetAllAsync();


            // Map to DTO
            var response = new List<UnitDTO>();
            foreach (var crnt in unit)
            {
                response.Add(new UnitDTO
                {
                    Id = crnt.Id,
                    Name = crnt.Name,
                    QuantityPerUnit = crnt.QuantityPerUnit
                });
            }
            return Ok(response);
        }

        // GET api/<UnitApiController>/5
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var existingstate = await _unit.GetById(id);
            if (existingstate is null)
            {
                return NotFound();
            }
            var response = new UnitDTO
            {
                Id = existingstate.Id,
                Name = existingstate.Name,
                QuantityPerUnit = existingstate.QuantityPerUnit
            };
            return Ok(response);
        }

        // POST api/<UnitApiController>
        [HttpPost]
        public async Task<IActionResult> Post(UnitDTO state)
        {
            //Map DTO
            var st = new Unit
            {
                Name = state.Name,
                QuantityPerUnit = state.QuantityPerUnit

            };
            await _unit.InsertAsync(st);
            var response = new UnitDTO
            {
                Name = st.Name,
                QuantityPerUnit = st.QuantityPerUnit
            };
            return Ok(response);
        }
        // PUT api/<UnitApiController>/5
        [HttpPut("{id}")]
        public bool Put([FromBody] Unit unit) => _unit.update(unit);

        // DELETE api/<UnitApiController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id) => _unit.delete(id);
    }
}
