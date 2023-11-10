using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Service.Contract;
using GoldenChequeBack.Service.Contract.DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GoldenChqeueBack.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityApiController : ControllerBase
    {
        private readonly ICityRepository _city;
        public CityApiController(ICityRepository city)
        {
            _city = city;
        }
        // GET: api/<CityApiController>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var city = await _city.GetAllAsync();


            // Map to DTO
            var response = new List<CityDTO>();
            foreach (var crnt in city)
            {
                response.Add(new CityDTO
                {
                    CityCode = crnt.CityCode,
                    Name = crnt.Name
                    
                });
            }
            return Ok(response);
        }

        // GET api/<CityApiController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var existingcity = await _city.GetById(id);
            if (existingcity is null)
            {
                return NotFound();
            }
            var response = new CityDTO
            {
                CityCode = existingcity.CityCode,
                Name = existingcity.Name
            };
            return Ok(response);
        }


        // POST api/<CityApiController>
        [HttpPost]
        public async Task<IActionResult> Post(CityDTO city)
        {
            //Map DTO
            var ct = new City
            {
                Name = city.Name,
                CityCode = city.CityCode,
                //Ostan = city.Ostan

            };
            await _city.InsertAsync(ct);
            var response = new CityDTO
            {
                Name = ct.Name,
                CityCode = ct.CityCode,
                //Ostan = ct.Ostan
            };
            return Ok(response);
        }

        // PUT api/<CityApiController>/5
        [HttpPut("{id}")]
        public bool Put([FromBody] City ct) => _city.update(ct);

        // DELETE api/<CityApiController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)=> _city.delete(id);
    }
}
