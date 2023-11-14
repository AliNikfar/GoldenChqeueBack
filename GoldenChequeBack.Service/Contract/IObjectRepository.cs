
using GoldenChequeBack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Object = GoldenChequeBack.Domain.Entities.Object;

namespace GoldenChequeBack.Service.Contract
{
    public interface IObjectRepository
    {
        Task<Object> InsertAsync(Object objectt);

        Task<Object?> UpdateAsync(Object bank);

        Task<Object?> DeleteAsync(Guid Id);

        Task<IEnumerable<Object>> GetAllAsync();

        Task<Object?> GetById(Guid id);
    }
}
