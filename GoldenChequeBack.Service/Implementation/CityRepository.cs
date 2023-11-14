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
    public  class CityRepository : ICityRepository
    { 
        private readonly ApplicationDbContext _ctx;
    public CityRepository(ApplicationDbContext ctx)
    {
        _ctx = ctx;
    }
        public async Task<City> DeleteAsync(Guid Id)
        {
            var existingCity = await _ctx.Cities.FirstOrDefaultAsync(p => p.Id == Id);
            if (existingCity == null)
            {
                return null;
            }
            _ctx.Cities.Remove(existingCity);
            await _ctx.SaveChangesAsync();
            return existingCity;
        }

        public async Task<IEnumerable<City>> GetAllAsync()
        {
            return await _ctx.Cities.ToListAsync(); ;
        }

        public async Task<City> GetById(Guid id)
        {
            return await _ctx.Cities.Where(p => p.Id == id).FirstOrDefaultAsync();
        }




        public async Task<City> InsertAsync(City city)
        {
            await _ctx.Cities.AddAsync(city);
            await _ctx.SaveChangesAsync();
            return city;
        }


        public async Task<City> UpdateAsync(City ct)
        {
            var existingct = await _ctx.Cities.FirstOrDefaultAsync(p => p.Id == ct.Id);
            if (existingct != null)
            {

                _ctx.Entry(existingct).CurrentValues.SetValues(ct);
                _ctx.SaveChangesAsync();
                return ct;
            }
            return null;


        }


        public async Task<IEnumerable<City>> GetByStateIdAsync(Guid stateId)
        {
            return await _ctx.Cities.Where(p => p.Ostan.Id == stateId).ToListAsync();
        }
    }
}
