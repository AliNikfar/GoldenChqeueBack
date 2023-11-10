using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Service.Contract;
using GoldenChequeBack.Service.Contract.DTO;
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
        public async Task<IActionResult> GetAllAsync()
        {
            var baseInfo = await _baseInfo.GetAllAsync();


            // Map to DTO
            var response = new List<BaseInfoDTO>();
            foreach (var crnt in baseInfo)
            {
                response.Add(new BaseInfoDTO {
                    Id = crnt.Id,
                    Title = crnt.Title ,
                    BoolValue =crnt.BoolValue,
                    Detail = crnt.Detail,
                    IntValue = crnt.IntValue,
                    LongValue = crnt.LongValue,
                    StringValue =   crnt.StringValue
                });
            }
            return Ok(response);
        }

        // GET api/<BaseInfoApiController>/5
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var existingBaseInfo = await _baseInfo.GetById(id);
            if (existingBaseInfo is null)
            {
                return NotFound();
            }
            var response = new BaseInfoDTO
            {
                Id = existingBaseInfo.Id,
                Title = existingBaseInfo.Title,
                BoolValue = existingBaseInfo.BoolValue,
                Detail = existingBaseInfo.Detail,
                IntValue = existingBaseInfo.IntValue,
                LongValue = existingBaseInfo.LongValue,
                StringValue = existingBaseInfo.StringValue
            };
            return Ok(response);
        }

        // POST api/<BaseInfoApiController>
        [HttpPost]
        public async Task<IActionResult> Post(BaseInfoDTO baseinfo)
        {
            //Map DTO
            var bse = new BaseInfo
            {
                 Title= baseinfo.Title,
                 StringValue = baseinfo.StringValue,
                 IntValue = baseinfo.IntValue,
                 LongValue = baseinfo.LongValue,
                 Detail = baseinfo.Detail,
                 BoolValue = baseinfo.BoolValue

            };
            await _baseInfo.InsertAsync(bse);
            var response = new BaseInfoDTO
            {
                Title = bse.Title,
                StringValue = bse.StringValue,
                IntValue = bse.IntValue,
                LongValue = bse.LongValue,
                Detail = bse.Detail,
                BoolValue = bse.BoolValue
            };
            return Ok(response);
        }

        // PUT api/<BaseInfoApiController>/5
        [HttpPut("{id}")]
        public bool Put([FromBody] BaseInfo baseinfo) => _baseInfo.update(baseinfo);

        // DELETE api/<BaseInfoApiController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id) => _baseInfo.delete(id);
    }
}
