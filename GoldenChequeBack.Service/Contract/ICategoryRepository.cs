using GoldenChequeBack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenChequeBack.Service.Contract
{
    public interface ICategoryRepository
    {
            Task<Category> InsertAsync(Category categ);

            Task<Category?> UpdateAsync(Category categ);

            Task<Category?> DeleteAsync(Guid Id);

            Task<IEnumerable<Category>> GetAllAsync();

            Task<Category?> GetById(Guid id);
        }
}
