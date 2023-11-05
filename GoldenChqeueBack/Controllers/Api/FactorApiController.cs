using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Service.Contract;
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
        public bool Post([FromBody] Factor factor) => _factor.Insert(factor);

        // PUT api/<FactorApiController>/5
        [HttpPut("{id}")]
        public bool Put([FromBody] Factor factor) => _factor.update(factor);

        // DELETE api/<FactorApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) => _factor.delete(id);
    }
}
