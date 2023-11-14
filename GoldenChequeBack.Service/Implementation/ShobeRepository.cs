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
    public class ShobeRepository : IShobeRepository
    {
        private readonly ApplicationDbContext _ctx;
        public ShobeRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Shobe> DeleteAsync(Guid Id)
        {
            var existingShobe = await _ctx.Shobes.FirstOrDefaultAsync(p => p.Id == Id);
            if (existingShobe == null)
            {
                return null;
            }
            _ctx.Shobes.Remove(existingShobe);
            await _ctx.SaveChangesAsync();
            return existingShobe;
        }
        public async Task<IEnumerable<Shobe>> GetAllAsync()
        {
            return await _ctx.Shobes.ToListAsync();
        }

        public async Task<IEnumerable<Shobe>> GetByBankId(Guid bankId)
        {
            return await _ctx.Shobes.Where(p => p.BankId == bankId).ToListAsync();
        }

        public async Task<Shobe> GetById(Guid id)
        {
            return await _ctx.Shobes.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Shobe> InsertAsync(Shobe shobe)
        {
            await _ctx.Shobes.AddAsync(shobe);
            await _ctx.SaveChangesAsync();
            return shobe;
        }

        public async Task<Shobe> UpdateAsync(Shobe shobe)
        {
            var existingShobe = await _ctx.Shobes.FirstOrDefaultAsync(p => p.Id == shobe.Id);
            if (existingShobe != null)
            {

                _ctx.Entry(existingShobe).CurrentValues.SetValues(shobe);
                _ctx.SaveChangesAsync();
                return shobe;
            }
            return null;
        }
    }
}
