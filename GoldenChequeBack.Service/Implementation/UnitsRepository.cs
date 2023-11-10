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
    public class UnitsRepository : IUnitsRepository
    {
        private readonly ApplicationDbContext _ctx;
        public UnitsRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public bool delete(Guid UnitId)
        {
            try
            {
                Unit bsi = _ctx.Units.Where(p => p.Id == UnitId).FirstOrDefault();
                _ctx.Units.Remove(bsi);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<IEnumerable<Unit>> GetAllAsync()
        {
            return await _ctx.Units.ToListAsync();
        }

        public async Task<Unit> GetById(Guid id)
        {
            return await _ctx.Units.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Unit> InsertAsync(Unit unit)
        {
            await _ctx.Units.AddAsync(unit);
            await _ctx.SaveChangesAsync();
            return unit;
        }

        public bool update(Unit unit)
        {
            try
            {
                _ctx.Units.Attach(unit);
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
