using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Service.Contract;
using Microsoft.AspNetCore.Mvc;

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
        public GoldenChequeBack.Domain.Entities.Object Get(int id) => _obj.GetById(id);

        // POST api/<ObjectApiController>
        [HttpPost]
        public bool Post([FromBody] GoldenChequeBack.Domain.Entities.Object obj) => _obj.Insert(obj);

        // PUT api/<ObjectApiController>/5
        [HttpPut("{id}")]
        public bool Put([FromBody] GoldenChequeBack.Domain.Entities.Object obj) => _obj.update(obj);

        // DELETE api/<ObjectApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) => _obj.delete(id);
    }
}