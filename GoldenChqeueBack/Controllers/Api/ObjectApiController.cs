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
        public List<GoldenChequeBack.Domain.Entities.Object> Getall() => _obj.GetAll();

        // GET api/<ObjectApiController>/5
        [HttpGet("{id}")]
        public GoldenChequeBack.Domain.Entities.Object Get(Guid id) => _obj.GetById(id);

        // POST api/<ObjectApiController>
        [HttpPost]
        public async Task<IActionResult> Post(ObjectDTO objct)
        {
            //Map DTO
            var obj = new Object
            {
                Title = objct.Title,
                Price = objct.Price,
                BuyPrice = objct.BuyPrice,
                //Unit = objct.Unit,
                WareHouseStock = objct.WareHouseStock

            };
            await _obj.InsertAsync(obj);
            var response = new ObjectDTO
            {
                Title = obj.Title,
                Price = obj.Price,
                BuyPrice = obj.BuyPrice,
                //Unit = obj.Unit,
                WareHouseStock = obj.WareHouseStock
            };
            return Ok(response);
        }
        // PUT api/<ObjectApiController>/5
        [HttpPut("{id}")]
        public bool Put([FromBody] GoldenChequeBack.Domain.Entities.Object obj) => _obj.update(obj);

        // DELETE api/<ObjectApiController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id) => _obj.delete(id);
    }
}