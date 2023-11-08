
using GoldenChequeBack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract
{
    public interface IChequeRepository
    {

        Task<Cheque> InsertAsync(Cheque cheque);

        bool update(Cheque cheque);

        bool delete(Guid chequeId);

        List<Cheque> GetAll();

        Cheque GetById(Guid id);
    }
}
