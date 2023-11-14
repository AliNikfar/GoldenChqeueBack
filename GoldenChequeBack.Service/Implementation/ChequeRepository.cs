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
    public class ChequeRepository : IChequeRepository
    {
        private readonly ApplicationDbContext _ctx;
        public ChequeRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Cheque> DeleteAsync(Guid Id)
        {
            var existingCheuqe = await _ctx.Cheques.FirstOrDefaultAsync(p => p.Id == Id);
            if (existingCheuqe == null)
            {
                return null;
            }
            _ctx.Cheques.Remove(existingCheuqe);
            await _ctx.SaveChangesAsync();
            return existingCheuqe;
        }

        public async Task<IEnumerable<Cheque>> GetAllAsync()
        {
            return await _ctx.Cheques.ToListAsync(); ;
        }

        public async Task<Cheque> GetById(Guid id)
        {
            return await _ctx.Cheques.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Cheque> InsertAsync(Cheque chq)
        {
            await _ctx.Cheques.AddAsync(chq);
            await _ctx.SaveChangesAsync();
            return chq;
        }


        public async Task<Cheque> UpdateAsync(Cheque chq)
        {
            var existingBank = await _ctx.Cheques.FirstOrDefaultAsync(p => p.Id == chq.Id);
            if (existingBank != null)
            {

                _ctx.Entry(existingBank).CurrentValues.SetValues(chq);
                _ctx.SaveChangesAsync();
                return chq;
            }
            return null;


        }
    }
}
