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
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Product product)
        {
            _context.Products.Add(product);
            return Save();
        }

        public bool Delete(Product product)
        {
            _context.Products.Remove(product);
            return Save();
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .ToListAsync();
        }


        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(x => x.ProductId == id);
        }


        public async Task<Product> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.ProductId == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Product product)
        {
            _context.Products.Update(product);
            return Save();
        }
    }
}
