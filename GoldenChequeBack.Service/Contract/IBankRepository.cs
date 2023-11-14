
using GoldenChequeBack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract
{
    public interface IBankRepository
    {
        Task<Bank> InsertAsync (Bank bank);

        Task<Bank?> UpdateAsync(Bank bank);

        Task<Bank?> DeleteAsync (Guid Id);

        Task<IEnumerable<Bank>> GetAllAsync();

        Task<Bank?> GetById(Guid id);
    }
}
