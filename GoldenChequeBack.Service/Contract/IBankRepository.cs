
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

        bool delete(Guid bankId);

        Task<IEnumerable<Bank>> GetAllAsync();

        Task<Bank?> GetById(Guid id);
    }
}
