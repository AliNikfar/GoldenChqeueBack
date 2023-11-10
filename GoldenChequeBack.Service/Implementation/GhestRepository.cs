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
    public class GhestRepository : IGhestRepository
    {
        private readonly ApplicationDbContext _ctx;
        public GhestRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public bool delete(Guid GhestId)
        {
            try
            {
                Ghest bsi = _ctx.Ghests.Where(p => p.Id == GhestId).FirstOrDefault();
                _ctx.Ghests.Remove(bsi);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<IEnumerable<Ghest>> GetAllAsync()
        {
            return await _ctx.Ghests.ToListAsync(); ;
        }
        public async Task<IEnumerable<Ghest>> GetByFactorId(Guid Factorid)
        {
            return await _ctx.Ghests.Where(p => p.Factor.Id == Factorid).ToListAsync(); ;
        }

        public async Task<Ghest> GetById(Guid id)
        {
            return await _ctx.Ghests.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public bool Insert(Ghest ghest)
        {
            try
            {
                _ctx.Ghests.Add(ghest);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }
        public async Task<Ghest> InsertAsync(Ghest ghest)
        {
            await _ctx.Ghests.AddAsync(ghest);
            await _ctx.SaveChangesAsync();
            return ghest;
        }

        public bool update(Ghest ghest)
        {
            try
            {
                _ctx.Ghests.Attach(ghest);
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
