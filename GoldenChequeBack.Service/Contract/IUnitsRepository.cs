
using GoldenChequeBack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract
{
    public interface IUnitsRepository
    {
        Task<Unit> InsertAsync(Unit units);

        Task<bool> IsUnitExsist(Unit unit);

        Task<Unit?> UpdateAsync(Unit bank);

        Task<Unit?> DeleteAsync(Guid Id);

        Task<IEnumerable<Unit>> GetAllAsync();

        Task<Unit?> GetById(Guid id);

    }
}
