using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeWeb.Application.Interfaces;
using TreeWeb.Core.Entities;
using TreeWeb.Infrastructure.Data;

namespace TreeWeb.Application.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ApplicationDbContext _context;

        public SupplierRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Supplier supplier)
        {
            _context.Add(supplier);
            return Save();
        }

        public bool Delete(Supplier supplier)
        {
            _context.Remove(supplier);
            return Save();
        }

        public async Task<IEnumerable<Supplier>> GetAll()
        {
            return await _context.Suppliers.ToListAsync();
        }

        //public async Task<IEnumerable<Supplier>> GetAllRacesByCity(string city)
        //{
        //    return await _context.Suppliers.Where(c => c.Address.City.Contains(city)).ToListAsync();
        //}

        public async Task<Supplier> GetByIdAsync(int id)
        {
            return await _context.Suppliers.FirstOrDefaultAsync(x => x.SupplierId == id);
        }

        public async Task<Supplier> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Suppliers.FirstOrDefaultAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Supplier supplier)
        {
            _context.Update(supplier);
            return Save();
        }
    }
}
