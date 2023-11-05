using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Persistence;
using GoldenChequeBack.Service.Contract;
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
        public bool delete(int customerId)
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

        public List<CustomerRate> GetAll()
        {
            return _ctx.CustomerRates.ToList();
        }

        public CustomerRate GetById(int id)
        {
            return _ctx.CustomerRates.Where(p => p.Id == id).FirstOrDefault();
        }

        public bool Insert(CustomerRate cus)
        {
            try
            {
                _ctx.CustomerRates.Add(cus);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
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