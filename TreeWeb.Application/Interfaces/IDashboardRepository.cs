using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeWeb.Core.Entities;

namespace TreeWeb.Application.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Category>> GetAllCategories();
        Task<List<Supplier>> GetAllSuppliers();
        Task<List<Product>> GetAllProducts();
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetByIdNoTracking(string id);
        bool Update(AppUser user);
        bool Save();
    }
}
