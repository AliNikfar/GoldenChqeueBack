using System;
using System.Collections.Generic;
using GoldenChequeBack.Service.Contract;
using System.Linq;
using System.Threading.Tasks;
using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GoldenChequeBack.Service.Implementation
{
    public class BaseInfoRepository : IBaseInfoRepository
    {
        private readonly ApplicationDbContext _ctx;
        public BaseInfoRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public bool delete(Guid BaseInfoId)
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
        public async Task<IEnumerable<BaseInfo>> GetAllAsync()
        {
            return await _ctx.BaseInfoes.ToListAsync(); ;
        }

        public async Task<BaseInfo> GetById(Guid id)
        {
            return _ctx.BaseInfoes.Where(p => p.Id == id).FirstOrDefault();
        }

        public async Task<BaseInfo> InsertAsync(BaseInfo baseInfo)
        {
            //try
            //{
                _ctx.BaseInfoes.AddAsync(baseInfo);
                _ctx.SaveChangesAsync();
                return baseInfo;
            //}
            //catch (Exception ex)
            //{
            //    return false;
            //}
        }

        public async Task<BaseInfo> UpdateAsync(BaseInfo basinfo)
        {
            var existingbasinfo = await _ctx.BaseInfoes.FirstOrDefaultAsync(p => p.Id == basinfo.Id);
            if (existingbasinfo != null)
            {

                _ctx.Entry(existingbasinfo).CurrentValues.SetValues(basinfo);
                _ctx.SaveChangesAsync();
                return basinfo;
            }
            return null;
        }
    }
}
