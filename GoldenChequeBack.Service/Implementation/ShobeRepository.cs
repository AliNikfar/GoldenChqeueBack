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

        public bool delete(Guid shobeId)
        {
            try
            {
                Shobe bsi = _ctx.Shobes.Where(p => p.Id == shobeId).FirstOrDefault();
                _ctx.Shobes.Remove(bsi);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
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

        public bool update(Shobe shobe)
        {
            try
            {
                _ctx.Shobes.Attach(shobe);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
