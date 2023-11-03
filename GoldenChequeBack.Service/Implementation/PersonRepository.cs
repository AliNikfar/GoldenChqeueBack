using System;
using System.Collections.Generic;
using GoldenChequeBack.Service.Contract;
using System.Linq;
using System.Threading.Tasks;
using GoldenChequeBack.Domain.Entities;
 

namespace GoldenChequeBack.Service.Implementation
{
    public class PersonRepository 
    {
    //    private readonly ApplicationDbContext _ctx;
    //    public PersonRepository(ApplicationDbContext ctx)
    //    {
    //        _ctx = ctx;
    //    }

    //    public bool delete(int personId)
    //    {
    //        try
    //        {
    //            Person bsi = _ctx.Persons.Where(p => p.Id == personId).FirstOrDefault();
    //            _ctx.Persons.Remove(bsi);
    //            _ctx.SaveChanges();
    //            return true;
    //        }
    //        catch (Exception ex)
    //        {
    //            return false;
    //        }
    //    }
    //    public List<Person> GetAll()
    //    {
    //        return _ctx.Persons.ToList();
    //    }

    //    public Person GetById(int id)
    //    {
    //        return _ctx.Persons.Where(p => p.Id == id).FirstOrDefault();
    //    }

    //    public bool Insert(Person person)
    //    {
    //        try
    //        {
    //            _ctx.Persons.Add(person);
    //            _ctx.SaveChanges();
    //            return true;
    //        }
    //        catch (Exception ex)
    //        {
    //            return false;
    //        }
    //    }

    //    public bool update(Person person)
    //    {
    //        try
    //        {
    //            _ctx.Persons.Attach(person);
    //            _ctx.SaveChanges();
    //            return true;
    //        }
    //        catch (Exception ex)
    //        {
    //            return false;
    //        }
    //    }
    }
}
