using GoldenChequeBack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract
{
    public interface IUsersRepository
    {

        Task<User> InsertAsync(User user);

        Task<User?> UpdateAsync(User bank);

        Task<User?> DeleteAsync(Guid Id);

        Task<IEnumerable<User>> GetAllAsync();

        Task<User?> GetById(Guid id);
    }
}
