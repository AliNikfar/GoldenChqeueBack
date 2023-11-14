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
        Task<Customer> InsertAsync(Customer customer);

        Task<Customer?> UpdateAsync(Customer customer);

        Task<Customer?> DeleteAsync(Guid Id);

        Task<IEnumerable<Customer>> GetAllAsync();

        Task<Customer?> GetById(Guid id);
    }
}
