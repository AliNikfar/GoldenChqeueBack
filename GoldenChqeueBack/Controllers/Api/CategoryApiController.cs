using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Service.Contract;
using GoldenChequeBack.Service.Contract.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GoldenChqeueBack.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryApiController : ControllerBase
    {
        private readonly ICategoryRepository _categ;
        public CategoryApiController(ICategoryRepository category)
        {
            _categ = category;
        }
        // GET: api/<BankApiController>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var categories = await _categ.GetAllAsync();


            // Map to DTO
            var response = new List<CategoryDTO>();
            foreach (var crnt in categories)
            {
                response.Add(new CategoryDTO { Id = crnt.Id, Title = crnt.Title });
            }
            return Ok(response);
        }

        // GET api/<BankApiController>/5
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var existingcategory = await _categ.GetById(id);
            if (existingcategory is null)
            {
                return NotFound();
            }
            var response = new CategoryDTO
            {
                Id = existingcategory.Id,
                Title = existingcategory.Title
            };
            return Ok(response);
        }

        // POST api/<BankApiController>
        [HttpPost]
        public async Task<IActionResult> Post(CreateBankRequestDTO category)
        {
            //Map DTO
            var cat = new Category
            {
                Title = category.Title,
                RegisterDate = DateTime.Now,
                LastChangeDate = DateTime.Now,
                Visable = true


            };
            await _categ.InsertAsync(cat);
            var response = new CreateBankRequestDTO
            {
                Title = cat.Title
            };
            return Ok(response);
        }

        // PUT api/<BankApiController>/5

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, UpdateBankRequestDTO request)
        {
            //convert DTO to Domain Model
            var category = new Category
            {
                Id = id,
                Title = request.Title
            };
            category = await _categ.UpdateAsync(category);
            if (category == null)
            {
                return NotFound();
            }

            var response = new CategoryDTO
            {
                Id = category.Id,
                Title = category.Title
            };

            return Ok(response);
        }

        // DELETE api/<BankApiController>/5
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var category = await _categ.DeleteAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            var response = new CategoryDTO
            {
                Id = category.Id,
                Title = category.Title
            };
            return Ok(response);
        }

    }
}

