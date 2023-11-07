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
        public List<City> GetByStateId(int stateId)=> _city.GetByStateId(stateId);

        // GET api/<CityApiController>/5
        [HttpGet("{id}")]
        public City Get(int id) => _city.GetById(id);

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
        public bool Delete(int id)=> _city.delete(id);
    }
}
