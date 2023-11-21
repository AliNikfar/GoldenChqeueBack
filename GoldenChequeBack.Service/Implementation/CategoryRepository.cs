using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Persistence;
using GoldenChequeBack.Service.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Implementation
{
    public class CategoryRepository :ICategoryRepository
    {
        private readonly ApplicationDbContext _ctx;
        public CategoryRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<Category> DeleteAsync(Guid Id)
        {
            var existingCat = await _ctx.Categories.FirstOrDefaultAsync(p => p.Id == Id);
            if (existingCat == null)
            {
                return null;
            }
            _ctx.Categories.Remove(existingCat);
            await _ctx.SaveChangesAsync();
            return existingCat;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _ctx.Categories.ToListAsync(); ;
        }

        public async Task<Category> GetById(Guid id)
        {
            return await _ctx.Categories.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Category> InsertAsync(Category categ)
        {
            await _ctx.Categories.AddAsync(categ);
            await _ctx.SaveChangesAsync();
            return categ;
        }


        public async Task<Category> UpdateAsync(Category categ)
        {
            var existingCat = await _ctx.Categories.FirstOrDefaultAsync(p => p.Id == categ.Id);
            if (existingCat != null)
            {

                _ctx.Entry(existingCat).CurrentValues.SetValues(categ);
                _ctx.SaveChangesAsync();
                return categ;
            }
            return null;
        }



    }
}
