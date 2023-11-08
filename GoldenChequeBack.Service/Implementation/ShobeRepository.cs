using System;
using System.Collections.Generic;
using GoldenChequeBack.Service.Contract;
using System.Linq;
using System.Threading.Tasks;
using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Persistence;

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
        public List<Shobe> GetAll()
        {
            return _ctx.Shobes.ToList();
        }

        public List<Shobe> GetByBankId(Guid bankId)
        {
            return _ctx.Shobes.ToList();
        }

        public Shobe GetById(Guid id)
        {
            return _ctx.Shobes.Where(p => p.Id == id).FirstOrDefault();
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
