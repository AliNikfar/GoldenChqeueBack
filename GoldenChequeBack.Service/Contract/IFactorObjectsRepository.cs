using GoldenChequeBack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract
{
    public interface IFactorObjectsRepository
    {
        Task<FactorObjects> InsertAsync(FactorObjects factorObjects);
        bool update(FactorObjects factorObjects);

        bool InsertList(List<FactorObjects> factorObjects);

        bool delete(int factorObjectsId);

        List<FactorObjects> GetAll();

        FactorObjects GetById(int id);

        List<FactorObjects> GetByFactorId(int Factorid);
    }
}
