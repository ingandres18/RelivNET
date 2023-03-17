using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Logic
{
    public class ProductRepository : IProductRepository
    {
        private readonly RelivDbContext _context;
        public ProductRepository(RelivDbContext context)
        {
            _context = context;
        }
        public async void DeleteProduct(int Id)
        {
            var productToDelete = _context.Products.Find(Id);

           if(productToDelete!=null) _context.Remove(productToDelete);
            Save();
        }

        public async Task<IEnumerable<Product>> GetProductAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int Id)
        {
            return await _context.Products.FindAsync(Id);
        }

        public async void AddProduct(Product product)
        {
            _context.Products.Add(product);
            Save();
        }

        public async void UpdateProduct(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            Save();
        }

        public void Save()
        {
           _context.SaveChanges();
        }
    }
}
