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
                    ImageId = crnt.ImageId,
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
                ImageId=existingObject.ImageId,
            };
            return Ok(response);
        }

        // POST api/<ObjectApiController>
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductRequestDTO product)
        {
            //Map DTO
            var obj = new Product
            {
                Title = product.Title,
                Price = product.Price,
                BuyPrice = product.BuyPrice,
                WareHouseStock = product.WareHouseStock,
                ImageId=product.ImageId,



            };
            await _obj.InsertAsync(obj);
            var response = new ProductDTO
            {
                Title = obj.Title,
                Price = obj.Price,
                BuyPrice = obj.BuyPrice,
                WareHouseStock = obj.WareHouseStock,
                ImageId = obj.ImageId,
            };
            return Ok(response);
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
                ImageId = request.ImageId,
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
                ImageId = obj.ImageId,
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
                ImageId=obj.ImageId,
            };
            return Ok(response);
        }
    }
}