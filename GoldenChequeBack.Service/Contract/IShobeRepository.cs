
using GoldenChequeBack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract
{
    public interface IShobeRepository
    {
        Task<Shobe> InsertAsync(Shobe shobe);

        bool update(Shobe shobe);

        bool delete(Guid shobeId);

        List<Shobe> GetAll();

        Shobe GetById(Guid id);

        List<Shobe> GetByBankId(Guid bankId);
    }
}
