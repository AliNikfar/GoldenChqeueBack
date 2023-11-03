
using GoldenChequeBack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract
{
    public interface IChequeRepository
    {

        bool Insert(Cheque cheque);

        bool update(Cheque cheque);

        bool delete(int chequeId);

        List<Cheque> GetAll();

        Cheque GetById(int id);
    }
}
