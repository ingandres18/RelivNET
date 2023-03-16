using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductAsync();
        Task<Product> GetProductByIdAsync(int id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int Id);
        void Save();
    }
}
