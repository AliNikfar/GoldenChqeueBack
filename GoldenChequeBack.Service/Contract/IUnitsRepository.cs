
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

        bool update(Unit units);

        bool delete(Guid unitsId);

        List<Unit> GetAll();

        Unit GetById(Guid id);

    }
}
