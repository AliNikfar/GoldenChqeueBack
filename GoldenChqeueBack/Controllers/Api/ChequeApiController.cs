using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Service.Contract;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GoldenChqeueBack.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChequeApiController : ControllerBase
    {
        private readonly IChequeRepository _cheque;
        public ChequeApiController( IChequeRepository cheque)
        {
            _cheque = cheque;
        }
        // GET: api/<ChequeApiController>
        [HttpGet]
        public List<Cheque> Get() => _cheque.GetAll();

        // GET api/<ChequeApiController>/5
        [HttpGet("{id}")]
        public Cheque Get(int id) => _cheque.GetById(id);

        // POST api/<ChequeApiController>
        [HttpPost]
        public bool Insert([FromBody] Cheque cheque) => _cheque.Insert(cheque);

        // PUT api/<ChequeApiController>/5
        [HttpPut("{id}")]
        public bool Update([FromBody] Cheque cheque)=> _cheque.update(cheque);

        // DELETE api/<ChequeApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) => _cheque.delete(id);
    }
}
