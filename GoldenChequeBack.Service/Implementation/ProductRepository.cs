using System;
using System.Collections.Generic;
using GoldenChequeBack.Service.Contract;
using System.Linq;
using System.Threading.Tasks;
using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GoldenChequeBack.Service.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _ctx;
        public ProductRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Product> DeleteAsync(Guid Id)
        {
            var existingObject = await _ctx.Products.Include(p => p.Image).FirstOrDefaultAsync(p => p.Id == Id);
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
            return await _ctx.Products.Include(p => p.Unit).Include(c=>c.Category).Include(p=>p.Image).ToListAsync(); 
        }
        public async Task<bool> IsProductExsist(Product product)
        {
            var isExist = _ctx.Products.Where(p => p.Title == product.Title).Count();
            return isExist > 0;

        }

        public async Task<Product> GetById(Guid id)
        {
            return await _ctx.Products.Where(p => p.Id == id).Include(p => p.Unit).Include(c => c.Category).Include(p => p.Image).FirstOrDefaultAsync();
        }

        public async Task<Product> InsertAsync(Product objectt)
        {
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
