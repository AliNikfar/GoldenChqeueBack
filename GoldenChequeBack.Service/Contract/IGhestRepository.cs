
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

        bool update(Ghest ghest);

        bool delete(Guid ghestId);

        List<Ghest> GetAll();

        Ghest GetById(Guid id);

        List<Ghest> GetByFactorId(Guid Factorid);
    }
}
