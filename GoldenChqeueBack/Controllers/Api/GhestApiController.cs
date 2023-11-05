using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Service.Contract;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GoldenChqeueBack.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class GhestApiController : ControllerBase
    {
        private readonly IGhestRepository _ghest;

        public GhestApiController(IGhestRepository ghest)
        {
            _ghest = ghest;
        }
        // GET: api/<GhestApiController>
        [HttpGet]
        public List<Ghest> GetByFactorId(int factorId) => _ghest.GetByFactorId(factorId);

        // GET api/<GhestApiController>/5
        [HttpGet("{id}")]
        public Ghest Get(int id) => _ghest.GetById(id);

        // POST api/<GhestApiController>
        [HttpPost]
        public bool Post([FromBody] Ghest ghest) => _ghest.Insert(ghest);

        // PUT api/<GhestApiController>/5
        [HttpPut("{id}")]
        public bool Put([FromBody] Ghest ghest) => _ghest.update(ghest);

        // DELETE api/<GhestApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) => _ghest.delete(id);
    }
}

