using GoldenChequeBack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract
{
    public interface ICustomerRepository
    {
        Customer Insert(Customer customer);

        Customer update(Customer customer);

        bool delete(int customerId);

        List<Customer> GetAll();

        Customer GetById(int id);
    }
}
