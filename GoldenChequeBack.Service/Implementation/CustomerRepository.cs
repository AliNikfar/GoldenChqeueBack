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
    public class CustomerRepository : ICustomerRepository
    { 
        private readonly ApplicationDbContext _ctx;
    public CustomerRepository(ApplicationDbContext ctx)
    {
        _ctx = ctx;
    }
        public async Task<Customer> DeleteAsync(Guid Id)
        {
            var existingCustomer = await _ctx.Customers.FirstOrDefaultAsync(p => p.Id == Id);
            if (existingCustomer == null)
            {
                return null;
            }
            _ctx.Customers.Remove(existingCustomer);
            await _ctx.SaveChangesAsync();
            return existingCustomer;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _ctx.Customers.ToListAsync(); ;
        }

        public async Task<Customer> GetById(Guid id)
        {
            return await _ctx.Customers.Where(p => p.Id == id).FirstOrDefaultAsync();
        }


        public async Task<Customer> InsertAsync(Customer cus)
        {
            await _ctx.Customers.AddAsync(cus);
            await _ctx.SaveChangesAsync();
            return cus;
        }

        public async Task<Customer> UpdateAsync(Customer bank)
        {
            var existingCustomer = await _ctx.Customers.FirstOrDefaultAsync(p => p.Id == bank.Id);
            if (existingCustomer != null)
            {

                _ctx.Entry(existingCustomer).CurrentValues.SetValues(bank);
                _ctx.SaveChangesAsync();
                return bank;
            }
            return null;


        }



    }
}