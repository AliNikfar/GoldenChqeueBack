
using GoldenChequeBack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract
{
    public interface IBaseInfoRepository
    {

        Task<BaseInfo> InsertAsync(BaseInfo baseInfo);

        Task<BaseInfo?> UpdateAsync(BaseInfo baseInfo);

        bool delete(Guid baseInfoId);

        Task<IEnumerable<BaseInfo>> GetAllAsync();

        Task<BaseInfo?> GetById(Guid id);
    }
}
