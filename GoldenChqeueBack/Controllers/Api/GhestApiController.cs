using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Service.Contract;
using GoldenChequeBack.Service.Contract.DTO;
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
        public List<Ghest> GetByFactorId(Guid factorId) => _ghest.GetByFactorId(factorId);

        // GET api/<GhestApiController>/5
        [HttpGet("{id}")]
        public Ghest Get(Guid id) => _ghest.GetById(id);

        // POST api/<GhestApiController>
        [HttpPost]
        public async Task<IActionResult> Post(GhestDTO ghest)
        {
            //Map DTO
            var gst = new Ghest
            {
                Price = ghest.Price,
                Status = ghest.Status,
                Date = ghest.Date,
                PassDate = ghest.PassDate,
                //Factor = ghest.Factor

            };
            await _ghest.InsertAsync(gst);
            var response = new GhestDTO
            {
                Price = gst.Price,
                Status = gst.Status,
                Date = gst.Date,
                PassDate = gst.PassDate,
               // Factor = gst.Factor
            };
            return Ok(response);
        }
        // PUT api/<GhestApiController>/5
        [HttpPut("{id}")]
        public bool Put([FromBody] Ghest ghest) => _ghest.update(ghest);

        // DELETE api/<GhestApiController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id) => _ghest.delete(id);
    }
}

