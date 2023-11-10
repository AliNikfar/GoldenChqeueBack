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
        [Route("{factorId:Guid}")]
        public async Task<IActionResult> GetByFactorId(Guid factorId)
        {
            var ghest = await _ghest.GetByFactorId(factorId);

            // Map to DTO
            var response = new List<GhestDTO>();
            foreach (var crnt in ghest)
            {
                response.Add(new GhestDTO
                {
                    Price = crnt.Price,
                    Status = crnt.Status,
                    Date = crnt.Date,
                    PassDate = crnt.PassDate,
                     Factor = crnt.Factor.Id
                });
            }
            return Ok(response);
        }

        // GET api/<GhestApiController>/5
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var existingGhest = await _ghest.GetById(id);
            if (existingGhest is null)
            {
                return NotFound();
            }
            var response = new GhestDTO
            {
                Price = existingGhest.Price,
                Status = existingGhest.Status,
                Date = existingGhest.Date,
                PassDate = existingGhest.PassDate,
                Factor = existingGhest.Factor.Id
            };
            return Ok(response);
        }

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

