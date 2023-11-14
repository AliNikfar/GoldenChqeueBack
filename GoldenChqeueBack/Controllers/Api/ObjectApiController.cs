using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Service.Contract;
using GoldenChequeBack.Service.Contract.DTO;
using Microsoft.AspNetCore.Mvc;
using Object = GoldenChequeBack.Domain.Entities.Object;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GoldenChqeueBack.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjectApiController : ControllerBase
    {
        private readonly IObjectRepository _obj;

        public ObjectApiController(IObjectRepository obj)
        {
            _obj = obj;
        }
        // GET: api/<ObjectApiController>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var objectt = await _obj.GetAllAsync();


            // Map to DTO
            var response = new List<ObjectDTO>();
            foreach (var crnt in objectt)
            {
                response.Add(new ObjectDTO
                {
                    Title = crnt.Title,
                    Price = crnt.Price,
                    BuyPrice = crnt.BuyPrice,
                    Unit = crnt.Unit.Id,
                    WareHouseStock = crnt.WareHouseStock
                });
            }
            return Ok(response);
        }

        // GET api/<ObjectApiController>/5
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var existingObject = await _obj.GetById(id);
            if (existingObject is null)
            {
                return NotFound();
            }
            var response = new ObjectDTO
            {
                Title = existingObject.Title,
                Price = existingObject.Price,
                BuyPrice = existingObject.BuyPrice,
                Unit = existingObject.Unit.Id,
                WareHouseStock = existingObject.WareHouseStock
            };
            return Ok(response);
        }

        // POST api/<ObjectApiController>
        [HttpPost]
        public async Task<IActionResult> Post(Object objectt)
        {
            //Map DTO
            var obj = new Object
            {
                Title = objectt.Title,
                Price = objectt.Price,
                BuyPrice = objectt.BuyPrice,
                //Unit = objectt.Unit.Id,
                WareHouseStock = objectt.WareHouseStock

            };
            await _obj.InsertAsync(obj);
            var response = new ObjectDTO
            {
                Title = obj.Title,
                Price = obj.Price,
                BuyPrice = obj.BuyPrice,
                Unit = obj.Unit.Id,
                WareHouseStock = obj.WareHouseStock
            };
            return Ok(response);
        }
        // PUT api/<ObjectApiController>/5
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, UpdateObjectRequestDTO request)
        {
            //convert DTO to Domain Model
            var obj = new Object
            {
                Id = id,
                Title = request.Title,
                Price = request.Price,
                BuyPrice = request.BuyPrice,
                //Unit = request.Unit.Id,
                WareHouseStock = request.WareHouseStock
            };
            obj = await _obj.UpdateAsync(obj);
            if (obj == null)
            {
                return NotFound();
            }

            var response = new ObjectDTO
            {
                Id = obj.Id,
                Title = obj.Title,
                Price = obj.Price,
                BuyPrice = obj.BuyPrice,
                Unit = obj.Unit.Id,
                WareHouseStock = obj.WareHouseStock
            };

            return Ok(response);
        }

        // DELETE api/<ObjectApiController>/5
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var obj = await _obj.DeleteAsync(id);
            if (obj == null)
            {
                return NotFound();
            }
            var response = new ObjectDTO
            {
                Title = obj.Title,
                Price = obj.Price,
                BuyPrice = obj.BuyPrice,
                Unit = obj.Unit.Id,
                WareHouseStock = obj.WareHouseStock
            };
            return Ok(response);
        }
    }
}