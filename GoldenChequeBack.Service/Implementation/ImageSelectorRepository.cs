using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Persistence;
using GoldenChequeBack.Service.Contract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

        public async Task<ImageSelector> GetByImageNameGuid(Guid imageName)
        {
            return await _ctx.Images.Where(p => p.FileName == imageName.ToString()).FirstOrDefaultAsync();
        }

        public async Task<ImageSelector> GetById(Guid? Id)
        {
            return await _ctx.Images.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<ImageSelector> Upload(IFormFile file, ImageSelector image)
        {
            try
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
            catch(Exception ex)
            {
                var localPath = Path.Combine(_webHostEnvirenment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtention}");
                File.Delete(localPath);
                //Re-throw exception 
                throw;
            }
        }
        public async Task<bool> Delete(ImageSelector image)
        {

            try
            {
                var localPath = Path.Combine(_webHostEnvirenment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtention}");
                File.Delete(localPath);
                var isImageExsists = _ctx.Images.Where(p => p.FileName == image.FileName).FirstOrDefault();
                if (isImageExsists is not null)
                {
                    _ctx.Images.Remove(image);
                    _ctx.SaveChangesAsync();
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }



    }
}
