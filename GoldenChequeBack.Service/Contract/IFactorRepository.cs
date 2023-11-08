
using GoldenChequeBack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract
{
    public interface IFactorRepository
    {
        Task<Factor> InsertAsync(Factor factor);

        bool update(Factor factor);

        bool delete(Guid factorId);

        List<Factor> GetAll();

        Factor GetById(Guid id);
    }
}
