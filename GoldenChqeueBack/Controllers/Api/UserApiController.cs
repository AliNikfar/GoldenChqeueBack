using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Service.Contract;
using GoldenChequeBack.Service.Contract.DTO;
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
        public User Get(Guid id) => _user.GetById(id);

        // POST api/<UserApiController>
        [HttpPost]
        public async Task<IActionResult> Post(UserDTO user)
        {
            //Map DTO
            var us = new User
            {
                UserId = user.UserId,
                UserName = user.UserName

            };
            await _user.InsertAsync(us);
            var response = new UserDTO
            {
                UserId = us.UserId,
                UserName = us.UserName
            };
            return Ok(response);
        }

        // PUT api/<UserApiController>/5
        [HttpPut("{id}")]
        public bool Put([FromBody] User user) => _user.update(user);

        // DELETE api/<UserApiController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id) => _user.delete(id);
    }
}
