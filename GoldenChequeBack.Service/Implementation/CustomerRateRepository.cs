using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Persistence;
using GoldenChequeBack.Service.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Implementation
{
    public class CustomerRateRepository : ICustomerRateRepository
    {
        private readonly ApplicationDbContext _ctx;
        public CustomerRateRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<CustomerRate> DeleteAsync(Guid Id)
        {
            var existingCustomerRate = await _ctx.CustomerRates.FirstOrDefaultAsync(p => p.Id == Id);
            if (existingCustomerRate == null)
            {
                return null;
            }
            _ctx.CustomerRates.Remove(existingCustomerRate);
            await _ctx.SaveChangesAsync();
            return existingCustomerRate;
        }

        public async Task<IEnumerable<CustomerRate>> GetAllAsync()
        {
            return await _ctx.CustomerRates.ToListAsync(); ;
        }

        public async Task<CustomerRate> GetById(Guid id)
        {
            return await _ctx.CustomerRates.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

    }
}