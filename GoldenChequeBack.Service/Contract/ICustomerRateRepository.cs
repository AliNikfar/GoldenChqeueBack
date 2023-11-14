using GoldenChequeBack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract
{
    public interface ICustomerRateRepository
    {


        Task<IEnumerable<CustomerRate>> GetAllAsync();

        Task<CustomerRate?> GetById(Guid id);
    }
}
