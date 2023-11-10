using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Service.Contract;
using GoldenChequeBack.Service.Contract.DTO;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GoldenChqeueBack.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateApiController : ControllerBase
    {
        private readonly IStateRepository _state;

        public StateApiController(IStateRepository state)
        {
            _state = state;
        }
        // GET: api/<StateApiController>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var state = await _state.GetAllAsync();


            // Map to DTO
            var response = new List<StateDTO>();
            foreach (var crnt in state)
            {
                response.Add(new StateDTO
                {
                    Id = crnt.Id,
                    Name = crnt.Name,
                });
            }
            return Ok(response);
        }


        // GET api/<StateApiController>/5
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var existingstate = await _state.GetById(id);
            if (existingstate is null)
            {
                return NotFound();
            }
            var response = new StateDTO
            {
                Id = existingstate.Id,
                Name = existingstate.Name
            };
            return Ok(response);
        }

        // POST api/<StateApiController>
        [HttpPost]
        public async Task<IActionResult> Post(StateDTO state)
        {
            //Map DTO
            var st = new State
            {
                Name = state.Name

            };
            await _state.InsertAsync(st);
            var response = new StateDTO
            {
                Name = st.Name
            };
            return Ok(response);
        }

        // PUT api/<StateApiController>/5
        [HttpPut("{id}")]
        public bool Put([FromBody] State state) => _state.update(state);

        // DELETE api/<StateApiController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id) => _state.delete(id);
    }
}
