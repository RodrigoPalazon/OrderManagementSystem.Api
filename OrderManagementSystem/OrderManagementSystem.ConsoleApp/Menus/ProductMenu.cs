using OMS.Model;
using OMS.Services.Interfaces;

namespace OMS.ConsoleApp.Menus
{
    public class ProductMenu
    {
        private readonly IProductService _productService;

        public ProductMenu(IProductService productService)
        {
            _productService = productService;
        }

        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Product Menu ===");
                Console.WriteLine("1. List Products");
                Console.WriteLine("2. Add Product");
                Console.WriteLine("3. Update Product");
                Console.WriteLine("4. Delete Product");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                var option = Console.ReadLine();

                try
                {
                    switch (option)
                    {
                        case "1":
                            ListProducts();
                            break;
                        case "2":
                            AddProduct();
                            break;
                        case "3":
                            UpdateProduct();
                            break;
                        case "4":
                            DeleteProduct();
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Invalid option.");
                            Pause();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Pause();
                }
            }
        }

        private void ListProducts()
        {
            Console.Clear();
            var products = _productService.GetAllProducts();

            Console.WriteLine("=== Products ===");

            if (!products.Any())
            {
                Console.WriteLine("No products found.");
            }
            else
            {
                foreach (var product in products)
                {
                    Console.WriteLine($"Id: {product.Id} | Name: {product.Name} | Price: {product.Price} | Stock: {product.StockQuantity}");
                }
            }

            Pause();
        }

        private void AddProduct()
        {
            Console.Clear();
            Console.WriteLine("=== Add Product ===");

            Console.Write("Name: ");
            string name = Console.ReadLine()!;

            Console.Write("Description: ");
            string description = Console.ReadLine()!;

            Console.Write("Price: ");
            decimal price = decimal.Parse(Console.ReadLine()!);

            Console.Write("Stock Quantity: ");
            int stockQuantity = int.Parse(Console.ReadLine()!);

            Console.Write("Category Id: ");
            int categoryId = int.Parse(Console.ReadLine()!);

            var product = new Product
            {
                    Name = name,
                Description = description,
                Price = price,
                StockQuantity = stockQuantity,
                CategoryId = categoryId,
                IsActive = true
            };

            _productService.CreateProduct(product);

            Console.WriteLine("Product created successfully.");
            Pause();
        }

        private void UpdateProduct()
        {
            Console.Clear();
            Console.WriteLine("=== Update Product ===");

            Console.Write("Enter product Id: ");
            int id = int.Parse(Console.ReadLine()!);

            var existingProduct = _productService.GetProductById(id);
            if (existingProduct == null)
            {
                Console.WriteLine("Product not found.");
                Pause();
                return;
            }

            Console.Write("New Name: ");
            existingProduct.Name = Console.ReadLine()!;

            Console.Write("New Description: ");
            existingProduct.Description = Console.ReadLine()!;

            Console.Write("New Price: ");
            existingProduct.Price = decimal.Parse(Console.ReadLine()!);

            Console.Write("New Stock Quantity: ");
            existingProduct.StockQuantity = int.Parse(Console.ReadLine()!);

            Console.Write("New Category Id: ");
            existingProduct.CategoryId = int.Parse(Console.ReadLine()!);

            _productService.UpdateProduct(existingProduct);

            Console.WriteLine("Product updated successfully.");
            Pause();
        }

        private void DeleteProduct()
        {
            Console.Clear();
            Console.WriteLine("=== Delete Product ===");

            Console.Write("Enter product Id: ");
            int id = int.Parse(Console.ReadLine()!);

            _productService.DeleteProduct(id);

            Console.WriteLine("Product deleted successfully.");
            Pause();
        }

        private static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}