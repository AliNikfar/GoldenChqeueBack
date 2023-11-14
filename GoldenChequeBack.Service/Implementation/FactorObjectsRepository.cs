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
    public class FactorObjectsRepository : IFactorObjectsRepository
    {
        private readonly ApplicationDbContext _ctx;
        public FactorObjectsRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        //public async Task<FactorObjects> DeleteAsync(Guid Id)
        //{
        //    var existingFactorObjects = await _ctx.FactorObjects.FirstOrDefaultAsync(p => p.Id == Id);
        //    if (existingFactorObjects == null)
        //    {
        //        return null;
        //    }
        //    _ctx.FactorObjects.Remove(existingFactorObjects);
        //    await _ctx.SaveChangesAsync();
        //    return existingFactorObjects;
        //}
        //public async Task<IEnumerable<FactorObjects>> GetAllAsync()
        //{
        //    return await _ctx.FactorObjects.ToListAsync(); ;
        //}

        //public async Task<IEnumerable<FactorObjects>> GetByFactorId(Guid Factorid)
        //{
        //    return await _ctx.FactorObjects.Where(p => p.Factor.Id == Factorid).ToListAsync(); ;
        //}

        //public async Task<FactorObjects> GetById(Guid id)
        //{
        //    return await _ctx.FactorObjects.Where(p => p.Id == id).FirstOrDefaultAsync();
        //}

        //public async Task<FactorObjects> InsertAsync(FactorObjects factorObjects)
        //{
        //    await _ctx.FactorObjects.AddAsync(factorObjects);
        //    await _ctx.SaveChangesAsync();
        //    return factorObjects;
        //}
        //public bool InsertList(List<FactorObjects> factorObjects)
        //{
        //    try
        //    {
        //        _ctx.FactorObjects.AddRange(factorObjects);
        //        _ctx.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        //public async Task<FactorObjects> UpdateAsync(FactorObjects fctObj)
        //{
        //    var existingfctObj = await _ctx.FactorObjects.FirstOrDefaultAsync(p => p.Id == fctObj.Id);
        //    if(existingfctObj != null)
        //    {

        //        _ctx.Entry(existingfctObj).CurrentValues.SetValues(fctObj);
        //        _ctx.SaveChangesAsync();
        //        return fctObj;
        //    }
        //    return null;


        //}
    }
}
