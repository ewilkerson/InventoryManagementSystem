using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories;

namespace InventoryManagementSystem.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
    }

    public class ProductService
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<ProductService> _logger;

        public ProductService()
        {
        }

        public ProductService(IProductRepository repository, ILogger<ProductService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            _logger.LogInformation("Fetching all products from the database.");
            return  _repository.GetAll();
        }

    }
}
