using Recipi_API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipi_API.Service
{
    public class categoryService
    {
        private readonly ApplicationDbContext _context;
        public categoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            Category item = _context.Categories.Find(id);
            return item;
        }
        public Category CreateCategory(Category item)
        {
            _context.Categories.Add(item);
            _context.SaveChanges();
            return item;
        }
        public void DeleteCategory(int id)
        {
            Category toDelete = _context.Categories.Find(id);
            _context.Categories.Remove(toDelete);
            _context.SaveChanges();
        }
        public Category UpdateCategory(int id, Category item)
        {
            Category toUpdate = _context.Categories.Find(id);
            if (toUpdate == null)
            {
                return null;
            }
            toUpdate.Name = item.Name;
            _context.SaveChanges();
            return toUpdate;
        }
    }
}
