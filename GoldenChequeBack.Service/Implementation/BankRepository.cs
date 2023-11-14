
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

    public class BankRepository : IBankRepository
    {
        private readonly ApplicationDbContext _ctx;
        public BankRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<Bank> DeleteAsync(Guid Id)
        {
            var existingBank = await _ctx.Banks.FirstOrDefaultAsync(p => p.Id == Id);
            if (existingBank == null)
            {
                return null;
            }
            _ctx.Banks.Remove(existingBank);
            await _ctx.SaveChangesAsync();
            return existingBank;
        }

        public async Task<IEnumerable<Bank>> GetAllAsync()
        {
            return await _ctx.Banks.ToListAsync(); ;
        }

        public async Task<Bank> GetById(Guid id)
        {
            return await _ctx.Banks.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Bank> InsertAsync(Bank bank)
        {
                await _ctx.Banks.AddAsync(bank);
                await _ctx.SaveChangesAsync();
                return bank;
        }


        public async Task<Bank> UpdateAsync(Bank bank)
        {
            var existingBank = await _ctx.Banks.FirstOrDefaultAsync(p => p.Id == bank.Id);
            if(existingBank != null)
            {
            
                _ctx.Entry(existingBank).CurrentValues.SetValues(bank);
                _ctx.SaveChangesAsync();
                return bank;
            }
            return null;
        }



    }
}
