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

        bool update(City city);

        bool delete(Guid cityId);

        Task<IEnumerable<City>> GetAllAsync();

        Task<City> GetById(Guid id);

        Task<IEnumerable<City>> GetByStateIdAsync(Guid stateId);
    }
}
