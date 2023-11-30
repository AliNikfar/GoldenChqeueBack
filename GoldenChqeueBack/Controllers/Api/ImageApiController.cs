using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Service.Contract;
using GoldenChequeBack.Service.Contract.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GoldenChqeueBack.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageApiController : ControllerBase
    {
        private readonly IImageSelectorRepository _imageSelectorRepository;
        public ImageApiController(IImageSelectorRepository imageSelectorRepository)
        {
            _imageSelectorRepository = imageSelectorRepository;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid imageName)
        {
            var image = await _imageSelectorRepository.GetByImageNameGuid( imageName);
            if (image == null)
            {
                return NotFound();
            }
            else
            {
             var delresult =   _imageSelectorRepository.Delete(image);
                if (delresult.Result)
                {
                    var response = new ImageSelectorDTO
                    {
                        Id = image.Id,
                        Title = image.Title,
                        FileExtention = image.FileExtention,
                        FileName = image.FileName,
                        Url = image.Url
                    };
                    return Ok(response);
                }
                else
                    return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file ,
            [FromForm] string fileName , [FromForm] string title)
        {

            ValidateUploadedFile(file);
            if (ModelState.IsValid)
            {
                var imageSelector = new ImageSelector
                {
                    FileExtention = Path.GetExtension(file.FileName).ToLower(),
                    FileName = Guid.NewGuid().ToString(),
                    Title = title,
                    Author = true,
                    LastChangeDate = DateTime.Now,
                    LastChangeUser = 1,
                    RegisterDate = DateTime.Now,
                    RegisterUser = 1,
                    Visable = true
                };
                imageSelector = await _imageSelectorRepository.Upload(file, imageSelector);
                // convert domain model to DTO
                var response = new ImageSelectorDTO
                {
                    Id = imageSelector.Id,
                    Title = imageSelector.Title,
                    FileExtention = imageSelector.FileExtention,
                    FileName = imageSelector.FileName,
                    Url = imageSelector.Url
                };

                return Ok(response);
            }
            return BadRequest(ModelState);


        }
        private void ValidateUploadedFile(IFormFile file)
        {
            var allowedExtention = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtention.Contains(Path.GetExtension(file.FileName).ToLower())) 
            {
                ModelState.AddModelError("file", "فایل ارسالی از نوع درست نیست");

            }
            if (file.Length >= 134876)
            {
                ModelState.AddModelError("file", "حجم فایل ارسالی نباید بیشتر از 1 مگابایت باشد");
            }

        }

    }
}
