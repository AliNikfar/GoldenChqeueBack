using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Service.Contract;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GoldenChqeueBack.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactorObjectApiController : ControllerBase
    {
        private readonly IFactorObjectsRepository _factorObject;

        public FactorObjectApiController(IFactorObjectsRepository factorObject)
        {
            _factorObject = factorObject;
        }
        // GET: api/<FactorObjectApiController>
        [HttpGet]
        public List<FactorObjects> GetByFactorId(int factorId) => _factorObject.GetByFactorId(factorId);

        // GET api/<FactorObjectApiController>/5
        [HttpGet("{id}")]
        public FactorObjects Get(int id) => _factorObject.GetById(id);

        // POST api/<FactorObjectApiController>
        [HttpPost]
        public bool Post([FromBody] FactorObjects factorObject) => _factorObject.Insert(factorObject);

        // PUT api/<FactorObjectApiController>/5
        [HttpPut("{id}")]
        public bool Put([FromBody] FactorObjects factorObject) => _factorObject.update(factorObject);

        // DELETE api/<FactorObjectApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) => _factorObject.delete(id);
    }
}
