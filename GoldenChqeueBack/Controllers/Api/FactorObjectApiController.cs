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
        //public async Task<IActionResult> GetAllAsync()                                               
        //{
        //    var factorObjects = await _factorObject.GetAllAsync();


        //    // Map to DTO
        //    var response = new List<FactorObjectsDTO>();
        //    foreach (var crnt in factorObjects)
        //    {
        //        response.Add(new FactorObjectsDTO
        //        {
        //            Id = crnt.Id,
        //            Price = crnt.Price,
        //            Name = crnt.Name,
        //            Count = crnt.Count,
        //            Sum = crnt.Sum,
        //        });
        //    }
        //    return Ok(response);
        //}

        //// GET: api/<FactorObjectApiController>
        //[HttpGet]
        //[Route("{factorId:Guid}")]
        //public async Task<IActionResult> GetByFactorId(Guid factorId) 
        //{
        //    var factorObjects = await _factorObject.GetByFactorId(factorId);

        //    // Map to DTO
        //    var response = new List<FactorObjectsDTO>();
        //    foreach (var crnt in factorObjects)
        //    {
        //        response.Add(new FactorObjectsDTO
        //        {
        //            Id = crnt.Id,
        //            Price = crnt.Price,
        //            Name = crnt.Name,
        //            Count = crnt.Count,
        //            Sum = crnt.Sum,
        //        });
        //    }
        //    return Ok(response);
        //}

        //// GET api/<FactorObjectApiController>/5
        //[HttpGet]
        //[Route("{id:Guid}")]
        //public async Task<IActionResult> GetById([FromRoute] Guid id)
        //{
        //    var existingfactorObject = await _factorObject.GetById(id);
        //    if (existingfactorObject is null)
        //    {
        //        return NotFound();
        //    }
        //    var response = new FactorObjectsDTO
        //    {
        //        Id = existingfactorObject.Id,
        //        Price = existingfactorObject.Price,
        //        Name = existingfactorObject.Name,
        //        Count = existingfactorObject.Count,
        //        Sum = existingfactorObject.Sum,
        //    };
        //    return Ok(response);
        //}

        //// POST api/<FactorObjectApiController>
        //[HttpPost]
        //public async Task<IActionResult> Post(FactorObjectsDTO factorobject)
        //{
        //    //Map DTO
        //    var fctobj = new FactorObjects
        //    {
        //        Price = factorobject.Price,
        //        Name = factorobject.Name,
        //        Count = factorobject.Count,
        //        Sum = factorobject.Sum,
        //        //ObjectsList = new List<GoldenChequeBack.Domain.Entities.Object>(),
        //        //Factor = new Factor()

        //    };
        //    //foreach (var factorObjectGuid in factorobject.ObjectsList)
        //    //{
        //    //    var existing = await _objectRepository.GetById(factorObjectGuid);
        //    //}

        //    await _factorObject.InsertAsync(fctobj);
        //    var response = new FactorObjectsDTO
        //    {
        //        Price = factorobject.Price,
        //        Name = factorobject.Name,
        //        Count = factorobject.Count,
        //        Sum = factorobject.Sum,
        //        //ObjectsList = factorobject.ObjectsList,
        //        //Factor = factorobject.Factor
        //    };
        //    return Ok(response);
        //}

        //// PUT api/<FactorObjectApiController>/5
        //[HttpPut("{id}")]
        //public bool Put([FromBody] FactorObjects factorObject) => _factorObject.update(factorObject);

        //// DELETE api/<FactorObjectApiController>/5
        //[HttpDelete("{id}")]
        //public void Delete(Guid id) => _factorObject.delete(id);
    }
}
