
using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Persistence;
using GoldenChequeBack.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Implementation
{
    public class UserRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _ctx;
        public UserRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public bool delete(Guid userId)
        {
            try
            {
                User bnk = _ctx.Users.Where(p => p.Id == userId).FirstOrDefault();
                _ctx.Users.Remove(bnk);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<User> GetAll()
        {
            return _ctx.Users.ToList();
        }

        public User GetById(Guid id)
        {
            return _ctx.Users.Where(p => p.Id == id).FirstOrDefault();
        }

        public async Task<User> InsertAsync(User user)
        {
            await _ctx.Users.AddAsync(user);
            await _ctx.SaveChangesAsync();
            return user;
        }


        public bool update(User user)
        {
            try
            {
                _ctx.Users.Attach(user);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



    }
}
