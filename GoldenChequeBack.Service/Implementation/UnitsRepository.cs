using System;
using System.Collections.Generic;
using GoldenChequeBack.Service.Contract;
using System.Linq;
using System.Threading.Tasks;
using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Persistence;

namespace GoldenChequeBack.Service.Implementation
{
    public class UnitsRepository : IUnitsRepository
    {
        private readonly ApplicationDbContext _ctx;
        public UnitsRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public bool delete(int UnitId)
        {
            try
            {
                Units bsi = _ctx.Units.Where(p => p.Id == UnitId).FirstOrDefault();
                _ctx.Units.Remove(bsi);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<Units> GetAll()
        {
            return _ctx.Units.ToList();
        }


        public Units GetById(int id)
        {
            return _ctx.Units.Where(p => p.Id == id).FirstOrDefault();
        }

        public bool Insert(Units unit)
        {
            try
            {
                _ctx.Units.Add(unit);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool update(Units unit)
        {
            try
            {
                _ctx.Units.Attach(unit);
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
