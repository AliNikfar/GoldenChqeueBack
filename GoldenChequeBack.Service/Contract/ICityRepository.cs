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
        City Insert(City city);

        City update(City city);

        bool delete(int cityId);

        List<City> GetAll();

        City GetById(int id);
    }
}
