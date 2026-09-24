using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Service.Contract;
using GoldenChequeBack.Service.Contract.DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GoldenChqeueBack.Controllers.Api
{
    [Route("api/[controller]")]
    [Route("api/Ghest")]
    [ApiController]
    public class GhestApiController : ControllerBase
    {
        private readonly IGhestRepository _ghest;
        private readonly IFactorRepository _factor;

        public GhestApiController(IGhestRepository ghest, IFactorRepository factor)
        {
            _ghest = ghest;
            _factor = factor;
        }
        // GET: api/<GhestApiController>
        [HttpGet]
        [Route("factor/{factorId:Guid}")]
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
            };

            var existingFactor = await _factor.GetById(ghest.Factor);
            if (existingFactor is null)
            {
                return NotFound("فاکتور یافت نشد");
            }
            gst.Factor = existingFactor;

            await _ghest.InsertAsync(gst);
            var response = new GhestDTO
            {
                Id = gst.Id,
                Price = gst.Price,
                Status = gst.Status,
                Date = gst.Date,
                PassDate = gst.PassDate,
                Factor = gst.FactorId
            };
            return Ok(response);
        }
        // PUT api/<GhestApiController>/5
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, UpdateGhestRequestDTO request)
        {
            //convert DTO to Domain Model
            var ghest = new Ghest
            {
                Id = id,
                Price = request.Price,
                Status = request.Status,
                Date = request.Date,
                PassDate = request.PassDate,
                // Factor = gst.Factor
            };
            ghest = await _ghest.UpdateAsync(ghest);
            if (ghest == null)
            {
                return NotFound();
            }

            var response = new GhestDTO
            {
                Id = ghest.Id,
                Price = ghest.Price,
                Status = ghest.Status,
                Date = ghest.Date,
                PassDate = ghest.PassDate,
            };

            return Ok(response);
        }

        // DELETE api/<GhestApiController>/5
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var ghest = await _ghest.DeleteAsync(id);
            if (ghest == null)
            {
                return NotFound();
            }
            var response = new GhestDTO
            {
                Id = ghest.Id,
                Price = ghest.Price,
                Status = ghest.Status,
                Date = ghest.Date,
                PassDate = ghest.PassDate,
                // Factor = gst.Factor
            };
            return Ok(response);
        }
    }
}

