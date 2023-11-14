
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

        Task<Factor> UpdateAsync(Factor factor);

        Task<Factor?> DeleteAsync(Guid Id);

        Task<IEnumerable<Factor>> GetAllAsync();

        Task<Factor?> GetById(Guid id);
    }
}
