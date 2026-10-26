using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
    }

    public class ProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll()
        {
            // AsNoTracking optimizes performance for read-only data
            return _context.Products.Include(p => p.Category).AsNoTracking().ToList();
        }

    }
}
