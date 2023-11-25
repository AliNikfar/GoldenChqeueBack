
using GoldenChequeBack.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product = GoldenChequeBack.Domain.Entities.Product;

namespace GoldenChequeBack.Service.Contract
{
    public interface IProductRepository
    {
        Task<Product>  InsertAsync(Product objectt, IFormFile file, string fileName);

        Task<Product?> UpdateAsync(Product bank);

        Task<Product?> DeleteAsync(Guid Id);

        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product?> GetById(Guid id);
    }
}
