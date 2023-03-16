using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Logic
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly RelivDbContext _context;
        public CategoryRepository(RelivDbContext context)
        {
            _context = context;
        }
        public  void DeleteCategory(int Id)
        {
            var categoryToDelete =  _context.Categories.Find(Id);

            if(categoryToDelete!=null) _context.Remove(categoryToDelete);
            Save();

            //_context.Categories.Remove(categoryToDelete);

            //return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Category>> GetCategoryAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int Id)
        {
            return await _context.Categories.FindAsync(Id);
        }

        public async void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            Save();
        }

        public async void UpdateCategory(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            Save();
            //var cat = await _context.Categories.FindAsync(category.CategoryId);

            //cat.Description = category.Description;

            //_context.Update(cat);
            //return await _context.SaveChangesAsync() > 0;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
