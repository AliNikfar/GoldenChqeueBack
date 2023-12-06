using GoldenChequeBack.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace GoldenChequeBack.Persistence
{
    public  class ApplicationDbContext  :DbContext
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Unit>().HasData(new Unit
            {
                Id = Guid.NewGuid(),
                Name = "بسته",
                QuantityPerUnit = 10,
                Author = true,
                LastChangeDate = DateTime.Now,
                LastChangeUser = 1,
                RegisterDate = DateTime.Now,
                RegisterUser = 1,
                Visable = true
            });
            var Parent = Guid.NewGuid();
            builder.Entity<Category>().HasData(new Category
            {
                Id = Parent,
                Title = "محصولات",
                ParentId = null,
                Author = true,
                LastChangeDate = DateTime.Now,
                LastChangeUser = 1,
                RegisterDate = DateTime.Now,
                RegisterUser = 1,
                Visable = true
            });
            builder.Entity<Category>().HasData(new Category
            {
                Id = Guid.NewGuid(),
                Title = "الکترونیکی",
                ParentId = Parent,
                Author = true,
                LastChangeDate = DateTime.Now,
                LastChangeUser = 1,
                RegisterDate = DateTime.Now,
                RegisterUser = 1,
                Visable = true
            });
            builder.Entity<Category>().HasData(new Category
            {
                Id = Guid.NewGuid(),
                Title = "غذایی",
                ParentId = Parent,
                Author = true,
                LastChangeDate = DateTime.Now,
                LastChangeUser = 1,
                RegisterDate = DateTime.Now,
                RegisterUser = 1,
                Visable = true
            });
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                .UseSqlServer(@"Data Source=DPK-158\\SQL2019;Initial Catalog=GoldenCheque;User ID=sa;Password=Dpk@12345");
            }

        }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
        public DbSet<ImageSelector> Images { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<BaseInfo> BaseInfoes { get; set; }
        public DbSet<Cheque> Cheques { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerRate> CustomerRates { get; set; }
        public DbSet<Factor> Factors { get; set; }
        public DbSet<FactorObjects> FactorObjects { get; set; }
        public DbSet<Ghest> Ghests { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Shobe> Shobes { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
