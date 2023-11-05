using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Service.Contract;
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
        public List<Unit> GetAll() => _unit.GetAll();

        // GET api/<UnitApiController>/5
        [HttpGet("{id}")]
        public Unit Get(int id) => _unit.GetById(id);

        // POST api/<UnitApiController>
        [HttpPost]
        public bool Post([FromBody] Unit unit) => _unit.Insert(unit);

        // PUT api/<UnitApiController>/5
        [HttpPut("{id}")]
        public bool Put([FromBody] Unit unit) => _unit.update(unit);

        // DELETE api/<UnitApiController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id) => _unit.delete(id);
    }
}
