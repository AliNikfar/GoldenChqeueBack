using GoldenChequeBack.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace GoldenChequeBack
{
    public  class ApplicationDbContext  :DbContext
    {

        public DbSet<Bank> Banks { get; set; }
        public DbSet<BaseInfo> BaseInfoes { get; set; }
        public DbSet<Cheque> Cheques { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerRate> CustomerRates { get; set; }
        public DbSet<Factor> Factors { get; set; }
        public DbSet<FactorObjects> FactorObjects { get; set; }
        public DbSet<Ghest> Ghests { get; set; }
        public DbSet<Domain.Entities.Object> Objects { get; set; }
        public DbSet<Shobe> Shobes { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=GoldenCheque;Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
            

        }
    }
}
