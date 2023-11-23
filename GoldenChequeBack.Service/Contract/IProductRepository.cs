
using GoldenChequeBack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product = GoldenChequeBack.Domain.Entities.Product;

namespace GoldenChequeBack.Service.Contract
{
    public interface IProductRepository
    {
        Task<Product> InsertAsync(Product objectt);

        Task<Product?> UpdateAsync(Product bank);

        Task<Product?> DeleteAsync(Guid Id);

        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product?> GetById(Guid id);
    }
}
