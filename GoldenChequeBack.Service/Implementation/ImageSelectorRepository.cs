using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Persistence;
using GoldenChequeBack.Service.Contract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Implementation
{
    public class ImageSelectorRepository : IImageSelectorRepository
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IWebHostEnvironment _webHostEnvirenment;
        private readonly ApplicationDbContext _ctx;

        public ImageSelectorRepository(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor , ApplicationDbContext ctx)
        {
            _webHostEnvirenment = webHostEnvironment;
            _contextAccessor = httpContextAccessor;
            _ctx = ctx;
        }

        public async Task<ImageSelector> Upload(IFormFile file, ImageSelector image)
        {
            //Upload Image to API
            var localPath = Path.Combine(_webHostEnvirenment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtention}");
            using var stream = new FileStream(localPath, FileMode.Create);
            await file.CopyToAsync(stream);

            //upload the database
            var httpRequest = _contextAccessor.HttpContext.Request;
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{image.FileName}{image.FileExtention}";
            image.Url = urlPath;
            _ctx.Images.AddAsync(image);
            _ctx.SaveChangesAsync();
            return image;
        }
    }
}
