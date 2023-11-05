using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Service.Contract;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GoldenChqeueBack.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private readonly IUsersRepository _user;
        public UserApiController(IUsersRepository user)
        {
            _user = user;
        }
        // GET: api/<UserApiController>
        [HttpGet]
        public List<User> GetAll() => _user.GetAll();

        // GET api/<UserApiController>/5
        [HttpGet("{id}")]
        public User Get(int id) => _user.GetById(id);

        // POST api/<UserApiController>
        [HttpPost]
        public bool Post([FromBody] User user) => _user.Insert(user);

        // PUT api/<UserApiController>/5
        [HttpPut("{id}")]
        public bool Put([FromBody] User user) => _user.update(user);

        // DELETE api/<UserApiController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id) => _user.delete(id);
    }
}
