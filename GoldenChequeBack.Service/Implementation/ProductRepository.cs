using System;
using System.Collections.Generic;
using GoldenChequeBack.Service.Contract;
using System.Linq;
using System.Threading.Tasks;
using GoldenChequeBack.Domain.Entities;
 
using Product = GoldenChequeBack.Domain.Entities.Product;
using GoldenChequeBack.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace GoldenChequeBack.Service.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _ctx;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAcc;
        public ProductRepository(ApplicationDbContext ctx,IWebHostEnvironment webHostEnvironment , IHttpContextAccessor httpContextAcc)
        {
            _ctx = ctx;
             _webHostEnvironment = webHostEnvironment;
            _httpContextAcc = httpContextAcc;
    } 

        public async Task<Product> DeleteAsync(Guid Id)
        {
            var existingObject = await _ctx.Products.FirstOrDefaultAsync(p => p.Id == Id);
            if (existingObject == null)
            {
                return null;
            }
            _ctx.Products.Remove(existingObject);
            await _ctx.SaveChangesAsync();
            return existingObject;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _ctx.Products.Include(p=>p.Unit).ToListAsync(); ;
        }

        public async Task<Product> GetById(Guid id)
        {
            return await _ctx.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Product> InsertAsync(Product objectt,IFormFile file , string fileName)
        {
            var localPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", $"{file.FileName}{objectt.ImageExtention}");
            using var stream = new FileStream(localPath, FileMode.Create);
            await file.CopyToAsync(stream);

            var httpRequest = _httpContextAcc.HttpContext.Request;
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{file.FileName}{objectt.ImageExtention}";
            objectt.ImageExtention = urlPath;
            await _ctx.Products.AddAsync(objectt);
            await _ctx.SaveChangesAsync();
            return objectt;
        }

        public async Task<Product> UpdateAsync(Product obj)
        {
            var existingObject = await _ctx.Products.FirstOrDefaultAsync(p => p.Id == obj.Id);
            if (existingObject != null)
            {

                _ctx.Entry(existingObject).CurrentValues.SetValues(obj);
                _ctx.SaveChangesAsync();
                return obj;
            }
            return null;

        }
        }
    }
