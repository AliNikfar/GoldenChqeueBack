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

        bool update(User user);

        bool delete(int usersId);

        List<User> GetAll();

        User GetById(int id);
    }
}
