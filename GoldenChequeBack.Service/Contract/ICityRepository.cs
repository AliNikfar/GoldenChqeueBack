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

        List<City> GetAll();

        City GetById(Guid id);

        List<City> GetByStateId(Guid CityId);
    }
}
