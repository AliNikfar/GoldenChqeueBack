
using GoldenChequeBack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract
{
    public interface IBankRepository
    {

        Task<Bank> InsertAsync (Bank bank);

        bool update (Bank bank);

        bool delete(Guid bankId);

        Task<IEnumerable<Bank>> GetAllAsync();

        Bank GetById(Guid id);
    }
}
