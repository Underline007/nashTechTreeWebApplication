using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeWeb.Core.Entities;

namespace TreeWeb.Application.Interfaces
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetAll();
        Task<Supplier> GetByIdAsync(int id);
        Task<Supplier> GetByIdAsyncNoTracking(int id);
        //Task<IEnumerable<Supplier>> GetClubByCity(string city);
        bool Add(Supplier supplier);
        bool Update(Supplier supplier);
        bool Delete(Supplier supplier);
        bool Save();
    }
}
