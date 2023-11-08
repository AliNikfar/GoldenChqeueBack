using System;
using System.Collections.Generic;
using GoldenChequeBack.Service.Contract;
using System.Linq;
using System.Threading.Tasks;
using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Persistence;

namespace GoldenChequeBack.Service.Implementation
{
    public class FactorRepository : IFactorRepository
    {
        private readonly ApplicationDbContext _ctx;
        public FactorRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public bool delete(Guid FactorId)
        {
            try
            {
                Factor bsi = _ctx.Factors.Where(p => p.Id == FactorId).FirstOrDefault();
                _ctx.Factors.Attach(bsi);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Factor> GetAll()
        {
            return _ctx.Factors.ToList();
        }

        public Factor GetById(Guid id)
        {
            return _ctx.Factors.Where(p => p.Id == id).FirstOrDefault();
        }

        public async Task<Factor> InsertAsync(Factor factor)
        {
            await _ctx.Factors.AddAsync(factor);
            await _ctx.SaveChangesAsync();
            return factor;
        }

        public bool update(Factor factor)
        {
            try
            {
                _ctx.Factors.Attach(factor);
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
