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

        public async Task<Unit> DeleteAsync(Guid Id)
        {
            var existingUnit = await _ctx.Units.FirstOrDefaultAsync(p => p.Id == Id);
            if (existingUnit == null)
            {
                return null;
            }
            _ctx.Units.Remove(existingUnit);
            await _ctx.SaveChangesAsync();
            return existingUnit;
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

        public async Task<Unit> UpdateAsync(Unit unit)
        {
            var existingUnit = await _ctx.Units.FirstOrDefaultAsync(p => p.Id == unit.Id);
            if (existingUnit != null)
            {

                _ctx.Entry(existingUnit).CurrentValues.SetValues(unit);
                _ctx.SaveChangesAsync();
                return unit;
            }
            return null;
        }
    }
}
