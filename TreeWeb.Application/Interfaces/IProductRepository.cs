using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeWeb.Core.Entities;

namespace TreeWeb.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetByIdAsync(int id);
        Task<Product> GetByIdAsyncNoTracking(int id);
        //Task<IEnumerable<Product>> GetClubByCity(string city);
        bool Add(Product product);
        bool Update(Product product);
        bool Delete(Product product);
        bool Save();
    }
}
