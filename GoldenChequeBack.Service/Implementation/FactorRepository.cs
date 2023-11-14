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
    public class FactorRepository : IFactorRepository
    {
        private readonly ApplicationDbContext _ctx;
        public FactorRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Factor> DeleteAsync(Guid Id)
        {
            var existingFactor = await _ctx.Factors.FirstOrDefaultAsync(p => p.Id == Id);
            if (existingFactor == null)
            {
                return null;
            }
            _ctx.Factors.Remove(existingFactor);
            await _ctx.SaveChangesAsync();
            return existingFactor;
        }

        public async Task<IEnumerable<Factor>> GetAllAsync()
        {
            return await _ctx.Factors.ToListAsync(); ;
        }

        public async Task<Factor> GetById(Guid id)
        {
            return await _ctx.Factors.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Factor> InsertAsync(Factor factor)
        {
            await _ctx.Factors.AddAsync(factor);
            await _ctx.SaveChangesAsync();
            return factor;
        }

        public async Task<Factor> UpdateAsync(Factor fctr)
        {
            var existingFactor = await _ctx.Factors.FirstOrDefaultAsync(p => p.Id == fctr.Id);
            if (existingFactor != null)
            {

                _ctx.Entry(existingFactor).CurrentValues.SetValues(fctr);
                _ctx.SaveChangesAsync();
                return fctr;
            }
            return null;


        }
    }
}
