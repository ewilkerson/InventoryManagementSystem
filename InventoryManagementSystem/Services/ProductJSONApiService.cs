using InventoryManagementSystem.Models;
using System.Text.Json;

namespace InventoryManagementSystem.Services
{
    public class ProductJSONApiService : IProductApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Products.json");

        // Fixed constructor name to match the class
        public ProductJSONApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            if (!File.Exists(_jsonFilePath)) return new List<Product>();

            var json = await File.ReadAllTextAsync(_jsonFilePath);
            return JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            var products = await GetAllProductsAsync();
            return products.FirstOrDefault(p => p.Id == id);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            var products = (await GetAllProductsAsync()).ToList();

            // Generate a new ID
            product.Id = products.Any() ? products.Max(p => p.Id) + 1 : 1;
            products.Add(product);

            await SaveProductsAsync(products);
            return product;
        }

        public async Task UpdateProductAsync(Product product)
        {
            var products = (await GetAllProductsAsync()).ToList();
            var index = products.FindIndex(p => p.Id == product.Id);

            if (index != -1)
            {
                products[index] = product;
                await SaveProductsAsync(products);
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            var products = (await GetAllProductsAsync()).ToList();
            var productToRemove = products.FirstOrDefault(p => p.Id == id);

            if (productToRemove != null)
            {
                products.Remove(productToRemove);
                await SaveProductsAsync(products);
            }
        }

        // Renamed to match products and added the missing semicolon
        private async Task SaveProductsAsync(List<Product> products)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(products, options);

            // Ensure the directory exists before writing
            var directory = Path.GetDirectoryName(_jsonFilePath);
            if (directory != null && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            await File.WriteAllTextAsync(_jsonFilePath, json);
        }
    }
}