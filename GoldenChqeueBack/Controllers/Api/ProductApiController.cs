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
        private readonly IUnitsRepository _unit;
        private readonly ICategoryRepository _category;
        private readonly IImageSelectorRepository _image;

        public ProductApiController(IProductRepository obj,IImageSelectorRepository image,IUnitsRepository unit , ICategoryRepository category )
        {
            _obj = obj;
            _unit = unit;
            _category = category;
            _image = image;
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
                    category =  new CategoryDTO {Id = crnt.Category.Id , Title = crnt.Category.Title },
                    Unit = new UnitDTO { Id = crnt.Unit.Id, Name = crnt.Unit.Name,QuantityPerUnit = crnt.Unit.QuantityPerUnit },
                    Image = new ImageSelectorDTO {Id = crnt.Image.Id , FileExtention = crnt.Image.FileExtention 
                                                  , FileName = crnt.Image.FileName , Title = crnt.Image.Title ,
                                                    Url = crnt.Image.Url}


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
                WareHouseStock = existingObject.WareHouseStock
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
                Image= new ImageSelector(),
                Category = new Category(),
                RegisterDate = DateTime.Now,
                LastChangeDate = DateTime.Now,
                Visable = true,
                LastChangeUser = 1 ,
                Author = true,
                RegisterUser = 1,
                Unit = new Unit(),
                
            };
            var exsistProduct = _obj.IsProductExsist(obj);
            if (exsistProduct.Result)
            {
                return NotFound("کالا با این نام قبلا ثبت شده است");
               
            }
            else
            {
                var exsistingUnit = _unit.GetById(product.UnitId);

                if (exsistingUnit is not null)
                {
                    obj.Unit = exsistingUnit.Result;
                }
                else
                {
                    return NotFound("اطلاعات واحد کالا یافت نشد");
                }
                var exsistingcategory = _category.GetById(product.CategoryId);

                if (exsistingcategory is not null)
                {
                    obj.Category = exsistingcategory.Result;
                }
                else
                {
                    return NotFound("اطلاعات واحد کالا یافت نشد");
                }
                if (product.ImageId is not null)
                {
                    var exsistingImage = _image.GetById(product.ImageId);

                    if (exsistingImage is not null)
                    {
                        obj.Image = exsistingImage.Result;
                    }
                    else
                    {
                        return NotFound("تصویر یافت نشد");
                    }
                }
                else
                {
                    obj.Image = null;
                }


                await _obj.InsertAsync(obj);
                var response = new ProductDTO
                {
                    Title = obj.Title,
                    Price = obj.Price,
                    BuyPrice = obj.BuyPrice,
                    WareHouseStock = obj.WareHouseStock
                };
                return Ok(response);

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
                WareHouseStock = request.WareHouseStock
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
                WareHouseStock = obj.WareHouseStock
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
                WareHouseStock = obj.WareHouseStock
            };
            return Ok(response);
        }
    }
}