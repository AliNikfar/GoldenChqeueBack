using GoldenChequeBack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Persistence
{
    public  class ApplicationDbContext  :DbContext
    {
        ApplicationDbContext()
        {

        }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<BaseInfo> BaseInfoes { get; set; }
        public DbSet<Cheque> Cheques { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerRate> CustomerRates { get; set; }
        public DbSet<Factor> Factors { get; set; }
        public DbSet<FactorObjects> FactorObjects { get; set; }
        public DbSet<Ghest> Ghests { get; set; }
        public DbSet<Domain.Entities.Object> Objectes { get; set; }
        public DbSet<Shobe> Branches { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                .UseSqlServer(@"Data Source=.;Initial Catalog=GoldenChequeWeb;Integrated Security=Trues");
            }

        }
    }
}
