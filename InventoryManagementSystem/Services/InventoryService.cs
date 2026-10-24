using InventoryManagementSystem.Models;
using InventoryManagementSystem.Repositories;
using Microsoft.Extensions.Logging;

namespace InventoryManagementSystem.Services
{
    public interface IInventoryReportingService
    {
        Task<IEnumerable<Product>> GetProductsWithCategoriesAsync();
        Task<IEnumerable<object>> GetTransactionHistoryAsync();
        Task<IEnumerable<Product>> GetProductsToRestockAsync();
        Task<IEnumerable<object>> GetInventoryValueByCategoryAsync();
    }

    public class InventoryReportingService: IInventoryReportingService
    {
        private readonly IInventoryReportingRepository _repository;
        private readonly ILogger<InventoryReportingService> _logger;

        public InventoryReportingService(IInventoryReportingRepository repository, ILogger<InventoryReportingService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> GetProductsWithCategoriesAsync()
        {
            _logger.LogWarning("GetProductsWithCategoriesAsync()");
            return await _repository.GetProductsWithCategoriesAsync();
        }
        public async Task<IEnumerable<object>> GetTransactionHistoryAsync() 
        {
            _logger.LogWarning("GetTransactionHistoryAsync()");
            return await _repository.GetTransactionHistoryAsync();

        }
        public async Task<IEnumerable<Product>> GetProductsToRestockAsync() 
        {
            _logger.LogWarning("GetProductsToRestockAsync()");
            return await _repository.GetProductsToRestockAsync();
        }
        public async Task<IEnumerable<object>> GetInventoryValueByCategoryAsync()
        {
            _logger.LogWarning("GetInventoryValueByCategoryAsync()");
            return await _repository.GetInventoryValueByCategoryAsync();
        }


    }
}
