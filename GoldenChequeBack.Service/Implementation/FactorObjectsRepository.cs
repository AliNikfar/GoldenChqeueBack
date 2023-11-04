using System;
using System.Collections.Generic;
using GoldenChequeBack.Service.Contract;
using System.Linq;
using System.Threading.Tasks;
using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Persistence;

namespace GoldenChequeBack.Service.Implementation
{
    public class FactorObjectsRepository : IFactorObjectsRepository
    {
        private readonly ApplicationDbContext _ctx;
        public FactorObjectsRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public bool delete(int FactorObjectId)
        {
            try
            {
                FactorObjects bsi = _ctx.FactorObjects.Where(p => p.Id == FactorObjectId).FirstOrDefault();
                _ctx.FactorObjects.Remove(bsi);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<FactorObjects> GetAll()
        {
            return _ctx.FactorObjects.ToList();
        }

        public List<FactorObjects> GetByFactorId(int Factorid)
        {
            return _ctx.FactorObjects.Where(p => p.Factor.Id == Factorid).ToList();
        }

        public FactorObjects GetById(int id)
        {
            return _ctx.FactorObjects.Where(p => p.Id == id).FirstOrDefault();
        }

        public bool Insert(FactorObjects factorObjects)
        {
            try
            {
                _ctx.FactorObjects.Add(factorObjects);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool InsertList(List<FactorObjects> factorObjects)
        {
            try
            {
                _ctx.FactorObjects.AddRange(factorObjects);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool update(FactorObjects factorObjects)
        {
            try
            {
                _ctx.FactorObjects.Attach(factorObjects);
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
