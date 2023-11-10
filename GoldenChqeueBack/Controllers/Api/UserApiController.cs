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
        public async Task<IActionResult> GetAllAsync()
        {
            var user = await _user.GetAllAsync();


            // Map to DTO
            var response = new List<UserDTO>();
            foreach (var crnt in user)
            {
                response.Add(new UserDTO
                {
                    UserId = crnt.UserId,
                    UserName = crnt.UserId,
                });
            }
            return Ok(response);
        }

        // GET api/<UserApiController>/5
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var existingUser = await _user.GetById(id);
            if (existingUser is null)
            {
                return NotFound();
            }
            var response = new UserDTO
            {
                UserId = existingUser.UserId,
                UserName = existingUser.UserName
            };
            return Ok(response);
        }

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
