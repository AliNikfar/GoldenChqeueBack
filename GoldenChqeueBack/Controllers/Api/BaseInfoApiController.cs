using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Service.Contract;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GoldenChqeueBack.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseInfoApiController : ControllerBase
    {
        private readonly IBaseInfoRepository _baseInfo;
        
        public BaseInfoApiController(IBaseInfoRepository baseInfo)
        {
            _baseInfo = baseInfo;
        }

        // GET: api/<BaseInfoApiController>
        [HttpGet]
        public List<BaseInfo> Getall() => _baseInfo.GetAll();

        // GET api/<BaseInfoApiController>/5
        [HttpGet("{id}")]
        public BaseInfo Get(int id) => _baseInfo.GetById(id);

        // POST api/<BaseInfoApiController>
        [HttpPost]
        public bool Post([FromBody] BaseInfo baseinfo) => _baseInfo.Insert(baseinfo);

        // PUT api/<BaseInfoApiController>/5
        [HttpPut("{id}")]
        public bool Put([FromBody] BaseInfo baseinfo) => _baseInfo.update(baseinfo);

        // DELETE api/<BaseInfoApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) => _baseInfo.delete(id);
    }
}
