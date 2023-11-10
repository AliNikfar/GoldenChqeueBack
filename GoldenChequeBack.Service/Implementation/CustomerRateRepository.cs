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
        public bool delete(Guid customerId)
        {
            try
            {
                CustomerRate cus = _ctx.CustomerRates.Where(p => p.Id == customerId).FirstOrDefault();
                _ctx.CustomerRates.Remove(cus);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<CustomerRate>> GetAllAsync()
        {
            return await _ctx.CustomerRates.ToListAsync(); ;
        }

        public async Task<CustomerRate> GetById(Guid id)
        {
            return await _ctx.CustomerRates.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<CustomerRate> InsertAsync(CustomerRate cus)
        {
            await _ctx.CustomerRates.AddAsync(cus);
            await _ctx.SaveChangesAsync();
            return cus;
        }


        public bool update(CustomerRate cus)
        {
            try
            {
                _ctx.CustomerRates.Attach(cus);
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