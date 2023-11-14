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
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, UpdateCityRequestDTO request)
        {
            //convert DTO to Domain Model
            var city = new City
            {
                CityCode = request.CityCode,
                Name = request.Name
            };
            city = await _city.UpdateAsync(city);
            if (city == null)
            {
                return NotFound();
            }

            var response = new CityDTO
            {
                Id = city.Id,
                CityCode = city.CityCode,
                Name = city.Name,
                Ostan = city.Ostan.Id
            };

            return Ok(response);


        }

        // DELETE api/<CityApiController>/5
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var city = await _city.DeleteAsync(id);
            if (city == null)
            {
                return NotFound();
            }
            var response = new CityDTO
            {
                Id = city.Id,
                CityCode = city.CityCode,
                Name = city.Name,
                Ostan = city.Ostan.Id
            };
            return Ok(response);
        }
    }
}
