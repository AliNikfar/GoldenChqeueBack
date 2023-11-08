using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Service.Contract;
using GoldenChequeBack.Service.Contract.DTO;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GoldenChqeueBack.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactorObjectApiController : ControllerBase
    {
        private readonly IFactorObjectsRepository _factorObject;
       // private readonly IObjectRepository _objectRepository;

        public FactorObjectApiController(IFactorObjectsRepository factorObject)
            //,IObjectRepository objectRepository)
        {
            _factorObject = factorObject;
            //_objectRepository = objectRepository;
        }
        // GET: api/<FactorObjectApiController>
        [HttpGet]
        public List<FactorObjects> GetByFactorId(Guid factorId) => _factorObject.GetByFactorId(factorId);

        // GET api/<FactorObjectApiController>/5
        [HttpGet("{id}")]
        public FactorObjects Get(Guid id) => _factorObject.GetById(id);

        // POST api/<FactorObjectApiController>
        [HttpPost]
        public async Task<IActionResult> Post(FactorObjectsDTO factorobject)
        {
            //Map DTO
            var fctobj = new FactorObjects
            {
                Price = factorobject.Price,
                Name = factorobject.Name,
                Count = factorobject.Count,
                Sum = factorobject.Sum,
                //ObjectsList = new List<GoldenChequeBack.Domain.Entities.Object>(),
                //Factor = new Factor()

            };
            //foreach (var factorObjectGuid in factorobject.ObjectsList)
            //{
            //    var existing = await _objectRepository.GetById(factorObjectGuid);
            //}

            await _factorObject.InsertAsync(fctobj);
            var response = new FactorObjectsDTO
            {
                Price = factorobject.Price,
                Name = factorobject.Name,
                Count = factorobject.Count,
                Sum = factorobject.Sum,
                //ObjectsList = factorobject.ObjectsList,
                //Factor = factorobject.Factor
            };
            return Ok(response);
        }

        // PUT api/<FactorObjectApiController>/5
        [HttpPut("{id}")]
        public bool Put([FromBody] FactorObjects factorObject) => _factorObject.update(factorObject);

        // DELETE api/<FactorObjectApiController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id) => _factorObject.delete(id);
    }
}
