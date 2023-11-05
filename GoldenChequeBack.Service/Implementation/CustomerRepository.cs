using GoldenChequeBack.Domain.Entities;
using GoldenChequeBack.Persistence;
using GoldenChequeBack.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Implementation
{
    public class CustomerRepository : ICustomerRepository
    { 
        private readonly ApplicationDbContext _ctx;
    public CustomerRepository(ApplicationDbContext ctx)
    {
        _ctx = ctx;
    }
    public bool delete(int customerId)
    {
        try
        {
            Customer cus = _ctx.Customers.Where(p => p.Id == customerId).FirstOrDefault();
            _ctx.Customers.Remove(cus);
            _ctx.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public List<Customer> GetAll()
    {
        return _ctx.Customers.ToList();
    }

    public Customer GetById(int id)
    {
        return _ctx.Customers.Where(p => p.Id == id).FirstOrDefault();
    }

    public bool Insert(Customer cus)
    {
        try
        {
            _ctx.Customers.Add(cus);
            _ctx.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }


    public bool update(Customer cus)
    {
        try
        {
            _ctx.Customers.Attach(cus);
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