using GoldenChequeBack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract
{
    public interface ICityRepository
    {
        Task<City> InsertAsync(City city);

        Task<City?> UpdateAsync(City city);

        Task<City?> DeleteAsync(Guid cityId);

        Task<IEnumerable<City>> GetAllAsync();

        Task<City> GetById(Guid id);

        Task<IEnumerable<City?>> GetByStateIdAsync(Guid stateId);
    }
}
