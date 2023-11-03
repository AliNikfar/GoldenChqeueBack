
using GoldenChequeBack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract
{
    public interface IGhestRepository
    {
        bool Insert(Ghest ghest);

        bool update(Ghest ghest);

        bool delete(int ghestId);

        List<Ghest> GetAll();

        Ghest GetById(int id);

        List<Ghest> GetByFactorId(int Factorid);
    }
}
