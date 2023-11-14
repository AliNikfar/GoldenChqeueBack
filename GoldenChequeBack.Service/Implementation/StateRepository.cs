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
        public async Task<State> DeleteAsync(Guid Id)
        {
            var existingState = await _ctx.States.FirstOrDefaultAsync(p => p.Id == Id);
            if (existingState == null)
            {
                return null;
            }
            _ctx.States.Remove(existingState);
            await _ctx.SaveChangesAsync();
            return existingState;
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


        public async Task<State> UpdateAsync(State state)
        {
            var existingState = await _ctx.States.FirstOrDefaultAsync(p => p.Id == state.Id);
            if (existingState != null)
            {

                _ctx.Entry(existingState).CurrentValues.SetValues(state);
                _ctx.SaveChangesAsync();
                return state;
            }
            return null;
        }



    }
}
