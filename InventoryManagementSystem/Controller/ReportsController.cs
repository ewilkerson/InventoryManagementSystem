using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using InventoryManagementSystem.Repositories;
using InventoryManagementSystem.Shared.Models;
using Microsoft.Extensions.Logging;

namespace InventoryManagementSystem.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Manager")] // Secure the reporting endpoints
    public class ReportsController : ControllerBase
    {
        private readonly InventoryReportingRepository _repository;
        private readonly ILogger<ReportsController> _logger;

        public ReportsController(InventoryReportingRepository repository, ILogger<ReportsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("inventory-value")]
        public async Task<IActionResult> GetInventoryValueByCategory()
        {
            try
            {
                var data = await _repository.GetInventoryValueByCategoryAsync();

                // Map the anonymous object to our DTO
                var report = data.Select(d => new InventoryValueReportDto
                {
                    CategoryName = (string)((dynamic)d).CategoryName,
                    TotalItemsInStock = (int)((dynamic)d).TotalItemsInStock,
                    TotalInventoryValue = (decimal)((dynamic)d).TotalInventoryValue
                });

                return Ok(report);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating inventory value report.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("restock-alerts")]
        public async Task<IActionResult> GetRestockAlerts()
        {
            try
            {
                var products = await _repository.GetProductsToRestockAsync();

                var alerts = products.Select(p => new RestockReportDto
                {
                    ProductName = p.Name,
                    SKU = p.SKU,
                    StockQuantity = p.StockQuantity,
                    ReorderLevel = p.ReorderLevel,
                    CategoryName = p.Category?.Name ?? "Uncategorized"
                });

                return Ok(alerts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating restock alerts.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
