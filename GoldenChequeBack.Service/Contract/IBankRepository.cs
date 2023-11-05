
using GoldenChequeBack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract
{
    public interface IBankRepository
    {
        //IEnumerable<Bank> Reservations { get; }



        bool Insert (Bank bank);

        bool update (Bank bank);

        bool delete(int bankId);
 
        List<Bank> GetAll();

        Bank GetById(int id);
    }
}
