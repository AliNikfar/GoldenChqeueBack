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
    public bool delete(Guid cityId)
    {
        try
        {
            City ct = _ctx.Cities.Where(p => p.Id == cityId).FirstOrDefault();
            _ctx.Cities.Remove(ct);
            _ctx.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
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


        public bool update(City city)
    {
        try
        {
            _ctx.Cities.Attach(city);
            _ctx.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }


        public async Task<IEnumerable<City>> GetByStateIdAsync(Guid stateId)
        {
            return await _ctx.Cities.Where(p => p.Ostan.Id == stateId).ToListAsync();
        }
    }
}
