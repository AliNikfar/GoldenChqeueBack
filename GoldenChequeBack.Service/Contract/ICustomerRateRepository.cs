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

        CustomerRate Insert(CustomerRate customerrate);

        CustomerRate update(CustomerRate customerrate);

        bool delete(int customerrateId);

        List<CustomerRate> GetAll();

        CustomerRate GetById(int id);
    }
}
