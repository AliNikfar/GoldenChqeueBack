using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Persistence;
using GoldenChequeBack.Service.Contract;
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
    public bool delete(int cityId)
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

    public List<City> GetAll()
    {
        return _ctx.Cities.ToList();
    }

        public City GetById(int id)
        {
            return _ctx.Cities.Where(p => p.Id == id).FirstOrDefault();
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

        public List<City> GetByStateId(int id)
        {
            return _ctx.Cities.Where(p => p.Ostan.Id == id).ToList();
        }
    }
}
