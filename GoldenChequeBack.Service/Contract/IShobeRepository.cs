
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

        Task<IEnumerable<Shobe>> GetAllAsync();

        Task<Shobe?> GetById(Guid id);

        Task<IEnumerable<Shobe>> GetByBankId(Guid bankId);
    }
    }
