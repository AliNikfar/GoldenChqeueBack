using System;
using System.Collections.Generic;
using GoldenChequeBack.Service.Contract;
using System.Linq;
using System.Threading.Tasks;
using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Persistence;

namespace GoldenChequeBack.Service.Implementation
{
    public class BaseInfoRepository : IBaseInfoRepository
    {
        private readonly ApplicationDbContext _ctx;
        public BaseInfoRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public bool delete(int BaseInfoId)
        {
            try
            {
                BaseInfo bsi = _ctx.BaseInfoes.Where(p => p.Id == BaseInfoId).FirstOrDefault();
                _ctx.BaseInfoes.Remove(bsi);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<BaseInfo> GetAll()
        {
            return _ctx.BaseInfoes.ToList();
        }

        public BaseInfo GetById(int id)
        {
            return _ctx.BaseInfoes.Where(p => p.Id == id).FirstOrDefault();
        }

        public bool Insert(BaseInfo baseInfo)
        {
            try
            {
                _ctx.BaseInfoes.Add(baseInfo);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool update(BaseInfo baseInfo)
        {
            try
            {
                _ctx.BaseInfoes.Attach(baseInfo);
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
