
using GoldenChequeBack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract
{
    public interface IUnitsRepository
    {
        bool Insert(Unit units);

        bool update(Unit units);

        bool delete(int unitsId);

        List<Unit> GetAll();

        Unit GetById(int id);

    }
}
