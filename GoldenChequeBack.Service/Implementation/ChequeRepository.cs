using System;
using System.Collections.Generic;
using GoldenChequeBack.Service.Contract;
using System.Linq;
using System.Threading.Tasks;
using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Persistence;

namespace GoldenChequeBack.Service.Implementation
{
    public class ChequeRepository : IChequeRepository
    {
        private readonly ApplicationDbContext _ctx;
        public ChequeRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public bool delete(int chequeId)
        {
            try
            {
                Cheque bsi = _ctx.Cheques.Where(p => p.Id == chequeId).FirstOrDefault();
                bsi.Visable = false;
                _ctx.Cheques.Attach(bsi);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<Cheque> GetAll()
        {
            return _ctx.Cheques.ToList();
        }

        public Cheque GetById(int id)
        {
            return _ctx.Cheques.Where(p => p.Id == id).FirstOrDefault();
        }

        public bool Insert(Cheque cheque)
        {
            try
            {
                _ctx.Cheques.Add(cheque);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool update(Cheque cheque)
        {
            try
            {
                _ctx.Cheques.Attach(cheque);
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
