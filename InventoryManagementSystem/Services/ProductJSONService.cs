using InventoryManagementSystem.Models;
using System.Text.Json;

namespace InventoryManagementSystem.Services
{
    public class ProductJSONService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly string _jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Products.json");

        // Fixed constructor name to match the class
        public ProductJSONService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            if (!File.Exists(_jsonFilePath)) return new List<Product>();

            var json = File.ReadAllText(_jsonFilePath);
            return JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
        }

        public  Product? GetProductById(int id)
        {
            var products = GetAllProducts();
            return products.FirstOrDefault(p => p.Id == id);
        }

        public Product CreateProduct(Product product)
        {
            var products = GetAllProducts().ToList();

            // Generate a new ID
            product.Id = products.Any() ? products.Max(p => p.Id) + 1 : 1;
            products.Add(product);

            SaveProducts(products);
            return product;
        }

        public void UpdateProduct(Product product)
        {
            var products = GetAllProducts().ToList();
            var index = products.FindIndex(p => p.Id == product.Id);

            if (index != -1)
            {
                products[index] = product;
                SaveProducts(products);
            }
        }

        public void DeleteProduct(int id)
        {
            var products = GetAllProducts().ToList();
            var productToRemove = products.FirstOrDefault(p => p.Id == id);

            if (productToRemove != null)
            {
                products.Remove(productToRemove);
                SaveProducts(products);
            }
        }

        // Renamed to match products and added the missing semicolon
        private void SaveProducts(List<Product> products)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(products, options);

            // Ensure the directory exists before writing
            var directory = Path.GetDirectoryName(_jsonFilePath);
            if (directory != null && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(_jsonFilePath, json);
        }
    }
}