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

        Task<CustomerRate> InsertAsync(CustomerRate customerrate);

        bool update(CustomerRate customerrate);

        bool delete(Guid customerrateId);

        List<CustomerRate> GetAll();

        CustomerRate GetById(Guid id);
    }
}
