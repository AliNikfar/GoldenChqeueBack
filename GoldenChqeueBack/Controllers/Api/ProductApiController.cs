using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Service.Contract;
using GoldenChequeBack.Service.Contract.DTO;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GoldenChqeueBack.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductRepository _obj;

        public ProductApiController(IProductRepository obj)
        {
            _obj = obj;
        }
        // GET: api/<ObjectApiController>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var product = await _obj.GetAllAsync();


            // Map to DTO
            var response = new List<ProductDTO>();
            foreach (var crnt in product)
            {
                response.Add(new ProductDTO
                {
                    Title = crnt.Title,
                    Price = crnt.Price,
                    BuyPrice = crnt.BuyPrice,
                    WareHouseStock = crnt.WareHouseStock,
                    ImageExtention = crnt.ImageExtention
                });
            }
            return Ok(response);
        }

        // GET api/<ObjectApiController>/5
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var existingObject = await _obj.GetById(id);
            if (existingObject is null)
            {
                return NotFound();
            }
            var response = new ProductDTO
            {
                Title = existingObject.Title,
                Price = existingObject.Price,
                BuyPrice = existingObject.BuyPrice,
                WareHouseStock = existingObject.WareHouseStock,
                ImageExtention = existingObject.ImageExtention
            };
            return Ok(response);
        }

        // POST api/<ObjectApiController>
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductRequestDTO product,[FromForm] IFormFile file
            ,[FromForm] string fileName)
        {
            ValidateUploadedFile(file);
            if (ModelState.IsValid)
            {
                //Upload the file

                //Map DTO
                var obj = new Product
                {
                    Title = product.Title,
                    Price = product.Price,
                    BuyPrice = product.BuyPrice,
                    WareHouseStock = product.WareHouseStock,
                    ImageExtention = Path.GetExtension(file.FileName).ToLower(),
                    Author = true,
                    LastChangeDate = DateTime.Now,
                    Visable = true,
                    LastChangeUser = 1 ,
                    RegisterDate = DateTime.Now,
                    RegisterUser = 1


                };

                await _obj.InsertAsync(obj,file, fileName);
                var response = new ProductDTO
                {
                    Title = obj.Title,
                    Price = obj.Price,
                    BuyPrice = obj.BuyPrice,
                    WareHouseStock = obj.WareHouseStock,
                    ImageExtention = obj.ImageExtention
                };
                return Ok(response);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        private void ValidateUploadedFile(IFormFile file)
        {
            var allowedExtention = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtention.Contains(Path.GetExtension(file.FileName).ToLower())) ;
            {
                ModelState.AddModelError("file", "فایل ارسالی از نوع درست نیست");

            }
            if (file.Length>= 104876)
            {
                ModelState.AddModelError("file", "حجم فایل ارسالی نباید بیشتر از 1 مگابایت باشد");
            }

        }
        // PUT api/<ObjectApiController>/5
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, UpdateProductRequestDTO request)
        {
            //convert DTO to Domain Model
            var obj = new Product
            {
                Id = id,
                Title = request.Title,
                Price = request.Price,
                BuyPrice = request.BuyPrice,
                //Unit = request.Unit.Id,
                WareHouseStock = request.WareHouseStock,
                ImageExtention = request.ImageURL
            };
            obj = await _obj.UpdateAsync(obj);
            if (obj == null)
            {
                return NotFound();
            }

            var response = new ProductDTO
            {
                Id = obj.Id,
                Title = obj.Title,
                Price = obj.Price,
                BuyPrice = obj.BuyPrice,
                WareHouseStock = obj.WareHouseStock,
                ImageExtention = request.ImageURL
            };

            return Ok(response);
        }

        // DELETE api/<ObjectApiController>/5
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var obj = await _obj.DeleteAsync(id);
            if (obj == null)
            {
                return NotFound();
            }
            var response = new ProductDTO
            {
                Title = obj.Title,
                Price = obj.Price,
                BuyPrice = obj.BuyPrice,
                WareHouseStock = obj.WareHouseStock,
                ImageExtention = obj.ImageExtention
            };
            return Ok(response);
        }
    }
}