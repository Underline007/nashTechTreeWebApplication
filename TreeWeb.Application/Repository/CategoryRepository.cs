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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Category category)
        {
            _context.Add(category);
            return Save();
        }

        public bool Delete(Category category)
        {
            _context.Remove(category);
            return Save();
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
        }

        public async Task<Category> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.CategoryId == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Category category)
        {
            _context.Update(category);
            return Save();
        }
    }
}
