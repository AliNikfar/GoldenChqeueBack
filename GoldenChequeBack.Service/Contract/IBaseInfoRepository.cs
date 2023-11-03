
using GoldenChequeBack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract
{
    public interface IBaseInfoRepository
    {
        bool Insert(BaseInfo baseInfo);

        bool update(BaseInfo baseInfo);

        bool delete(int baseInfoId);

        List<BaseInfo> GetAll();

        BaseInfo GetById(int id);
    }
}
