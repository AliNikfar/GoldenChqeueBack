
using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Persistence;
using GoldenChequeBack.Service.Contract;
using Microsoft.EntityFrameworkCore;
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
        public async Task<User> DeleteAsync(Guid Id)
        {
            var existingUser = await _ctx.Users.FirstOrDefaultAsync(p => p.Id == Id);
            if (existingUser == null)
            {
                return null;
            }
            _ctx.Users.Remove(existingUser);
            await _ctx.SaveChangesAsync();
            return existingUser;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _ctx.Users.ToListAsync();
        }

        public async Task<User> GetById(Guid id)
        {
            return await _ctx.Users.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> InsertAsync(User user)
        {
            await _ctx.Users.AddAsync(user);
            await _ctx.SaveChangesAsync();
            return user;
        }


        public async Task<User> UpdateAsync(User user)
        {
            var existingBank = await _ctx.Users.FirstOrDefaultAsync(p => p.Id == user.Id);
            if (existingBank != null)
            {

                _ctx.Entry(existingBank).CurrentValues.SetValues(user);
                _ctx.SaveChangesAsync();
                return user;
            }
            return null;
        }



    }
}
