
using GoldenChequeBack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Object = GoldenChequeBack.Domain.Entities.Object;

namespace GoldenChequeBack.Service.Contract
{
    public interface IObjectRepository
    {
        bool Insert(Object objectt);

        bool update(Object objectt);

        bool delete(int objectId);

        List<Object> GetAll();

        Object GetById(int id);
    }
}
