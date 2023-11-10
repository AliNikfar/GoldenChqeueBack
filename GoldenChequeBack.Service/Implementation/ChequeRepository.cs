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

        public bool delete(Guid chequeId)
        {
            try
            {
                Cheque bsi = _ctx.Cheques.Where(p => p.Id == chequeId).FirstOrDefault();
                bsi.Visable = false;
                _ctx.Cheques.Attach(bsi);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<IEnumerable<Cheque>> GetAllAsync()
        {
            return await _ctx.Cheques.ToListAsync(); ;
        }

        public async Task<Cheque> GetById(Guid id)
        {
            return await _ctx.Cheques.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Cheque> InsertAsync(Cheque cheque)
        {
            await _ctx.Cheques.AddAsync(cheque);
            await _ctx.SaveChangesAsync();
            return cheque;
        }

        public bool update(Cheque cheque)
        {
            try
            {
                _ctx.Cheques.Attach(cheque);
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
