using System;
using System.Collections.Generic;
using GoldenChequeBack.Service.Contract;
using System.Linq;
using System.Threading.Tasks;
using GoldenChequeBack.Domain.Entities;
 
using Object = GoldenChequeBack.Domain.Entities.Object;
using GoldenChequeBack.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GoldenChequeBack.Service.Implementation
{
    public class ObjectRepository : IObjectRepository
    {
        private readonly ApplicationDbContext _ctx;
        public ObjectRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public bool delete(Guid ObjectId)
        {
            try
            {
                Object bsi = _ctx.Objects.Where(p => p.Id == ObjectId).FirstOrDefault();
                _ctx.Objects.Remove(bsi);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<IEnumerable<Object>> GetAllAsync()
        {
            return await _ctx.Objects.ToListAsync(); ;
        }

        public async Task<Object> GetById(Guid id)
        {
            return await _ctx.Objects.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Object> InsertAsync(Object objectt)
        {
            await _ctx.Objects.AddAsync(objectt);
            await _ctx.SaveChangesAsync();
            return objectt;
        }

        public bool update(Object objectt)
        {
            try
            {
                _ctx.Objects.Attach(objectt);
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
