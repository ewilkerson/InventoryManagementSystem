using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string SKU { get; set; } = string.Empty;

        [Range(0.01, 10000.00)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        [Range(0, 1000)]
        public int ReorderLevel { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;
        public ICollection<Product>? Products { get; set; }
    }

    public class Supplier
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string CompanyName { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string ContactEmail { get; set; } = string.Empty;
    }

    public class Transaction
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        [Required]
        public string TransactionType { get; set; } = "Sale"; // or "Restock"
        public int Quantity { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    }
}
