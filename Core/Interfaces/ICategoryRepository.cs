using Core.Entities;

namespace Core.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoryAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int Id);
        void Save();
    }
}
