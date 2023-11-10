using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Persistence;
using GoldenChequeBack.Service.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Implementation
{
    public class StateRepository : IStateRepository
    {
        private readonly ApplicationDbContext _ctx;
        public StateRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public bool delete(Guid StateId)
        {
            try
            {
                State bnk = _ctx.States.Where(p => p.Id == StateId).FirstOrDefault();
                _ctx.States.Remove(bnk);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<State>> GetAllAsync()
        {
            return await _ctx.States.ToListAsync();
        }

        public async Task<State> GetById(Guid id)
        {
            return await _ctx.States.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<State> InsertAsync(State State)
        {
            await _ctx.States.AddAsync(State);
            await _ctx.SaveChangesAsync();
            return State;
        }


        public bool update(State State)
        {
            try
            {
                _ctx.States.Attach(State);
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
