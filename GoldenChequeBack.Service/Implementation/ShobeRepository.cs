using System;
using System.Collections.Generic;
using GoldenChequeBack.Service.Contract;
using System.Linq;
using System.Threading.Tasks;
using GoldenChequeBack.Domain.Entities;
 

namespace GoldenChequeBack.Service.Implementation
{
    public class ShobeRepository : IShobeRepository
    {
        private readonly ApplicationDbContext _ctx;
        public ShobeRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public bool delete(int shobeId)
        {
            try
            {
                Shobe bsi = _ctx.Shobes.Where(p => p.Id == shobeId).FirstOrDefault();
                _ctx.Shobes.Remove(bsi);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<Shobe> GetAll()
        {
            return _ctx.Shobes.ToList();
        }

        public List<Shobe> GetByBankId(int bankId)
        {
            return _ctx.Shobes.ToList();
        }

        public Shobe GetById(int id)
        {
            return _ctx.Shobes.Where(p => p.Id == id).FirstOrDefault();
        }

        public bool Insert(Shobe shobe)
        {
            try
            {
                _ctx.Shobes.Add(shobe);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool update(Shobe shobe)
        {
            try
            {
                _ctx.Shobes.Attach(shobe);
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
