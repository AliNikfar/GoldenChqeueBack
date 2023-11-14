
using GoldenChequeBack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract
{
    public interface IGhestRepository
    {
        Task<Ghest> InsertAsync(Ghest ghest);

        Task<Ghest?> UpdateAsync(Ghest bank);

        Task<Ghest?> DeleteAsync(Guid Id);

        Task<IEnumerable<Ghest>> GetAllAsync();

        Task<Ghest?> GetById(Guid id);

        Task<IEnumerable<Ghest>> GetByFactorId(Guid Factorid);
    }
}
