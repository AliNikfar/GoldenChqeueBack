using GoldenChequeBack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract
{
    public interface IStateRepository
    {
        Task<State> InsertAsync(State state);

        Task<State?> UpdateAsync(State bank);

        Task<State?> DeleteAsync(Guid Id);

        Task<IEnumerable<State>> GetAllAsync();

        Task<State?> GetById(Guid id);
    }
}
