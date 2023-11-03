using System;
using System.Collections.Generic;
using GoldenChequeBack.Service.Contract;
using System.Linq;
using System.Threading.Tasks;
using GoldenChequeBack.Domain.Entities;
 

namespace GoldenChequeBack.Service.Implementation
{
    public class FactorRepository : IFactorRepository
    {
        private readonly ApplicationDbContext _ctx;
        public FactorRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public bool delete(int FactorId)
        {
            try
            {
                Factor bsi = _ctx.Factors.Where(p => p.Id == FactorId).FirstOrDefault();
                bsi.Visable = false;
                _ctx.Factors.Attach(bsi);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Factor> GetAll()
        {
            return _ctx.Factors.ToList();
        }

        public Factor GetById(int id)
        {
            return _ctx.Factors.Where(p => p.Id == id).FirstOrDefault();
        }

        public bool Insert(Factor factor)
        {
            try
            {
                _ctx.Factors.Add(factor);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool update(Factor factor)
        {
            try
            {
                _ctx.Factors.Attach(factor);
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
