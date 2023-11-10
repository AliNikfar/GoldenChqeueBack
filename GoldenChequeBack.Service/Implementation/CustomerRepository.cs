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
    public bool delete(Guid customerId)
    {
        try
        {
            Customer cus = _ctx.Customers.Where(p => p.Id == customerId).FirstOrDefault();
            _ctx.Customers.Remove(cus);
            _ctx.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
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


        public bool update(Customer cus)
    {
        try
        {
            _ctx.Customers.Attach(cus);
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