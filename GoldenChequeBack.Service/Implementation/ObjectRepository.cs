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

        public async Task<Object> DeleteAsync(Guid Id)
        {
            var existingObject = await _ctx.Objects.FirstOrDefaultAsync(p => p.Id == Id);
            if (existingObject == null)
            {
                return null;
            }
            _ctx.Objects.Remove(existingObject);
            await _ctx.SaveChangesAsync();
            return existingObject;
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

        public async Task<Object> UpdateAsync(Object obj)
        {
            var existingObject = await _ctx.Objects.FirstOrDefaultAsync(p => p.Id == obj.Id);
            if (existingObject != null)
            {

                _ctx.Entry(existingObject).CurrentValues.SetValues(obj);
                _ctx.SaveChangesAsync();
                return obj;
            }
            return null;

        }
        }
    }
