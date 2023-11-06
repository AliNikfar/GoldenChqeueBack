
using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Persistence;
using GoldenChequeBack.Service.Contract;
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
        public bool delete(int bankId)
        {
            try
            {
                Bank bnk = _ctx.Banks.Where(p => p.Id == bankId).FirstOrDefault();
                _ctx.Banks.Remove(bnk);
                _ctx.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public List<Bank> GetAll()
        {
            return _ctx.Banks.ToList();
        }

        public Bank GetById(int id)
        {
            return _ctx.Banks.Where(p => p.Id == id).FirstOrDefault();
        }

        public async Task<Bank> InsertAsync(Bank bank)
        {
            //try
            //{

                await _ctx.Banks.AddAsync(bank);
                await _ctx.SaveChangesAsync();
                return bank;
            //}
            //catch (Exception ex)
            //{
            //    return false;
            //}
        }


        public bool update(Bank bank)
        {
            try
            {
                _ctx.Banks.Attach(bank);
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
