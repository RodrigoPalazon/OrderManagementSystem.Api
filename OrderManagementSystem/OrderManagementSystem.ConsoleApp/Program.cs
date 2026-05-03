using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OMS.DataAccess.Context;
using OMS.ConsoleApp.Menus;
using OMS.DataAccess.Interfaces;
using OMS.DataAccess.Repositories;
using OMS.Services.Interfaces;
using OMS.Services.Services;
using System.Runtime.InteropServices;

namespace OMS.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
             .SetBasePath(AppContext.BaseDirectory)
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
             .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection")!;

            var options = new DbContextOptionsBuilder<OmsDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            using var context = new OmsDbContext(options);

            ICategoryRepository categoryRepository = new CategoryRepository(context);
            ICustomerRepository customerRepository = new CustomerRepository(context);
            IProductRepository productRepository = new ProductRepository(context);

            ICategoryService categoryService = new CategoryService(categoryRepository);
            ICustomerService customerService = new CustomerService(customerRepository);
            IProductService productService = new ProductService(productRepository, categoryRepository);

            CategoryMenu categoryMenu = new CategoryMenu(categoryService);
            CustomerMenu customerMenu = new CustomerMenu(customerService);
            ProductMenu productMenu = new ProductMenu(productService);

            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("=== Order Management System ===");
                Console.WriteLine("1. Manage Categories");
                Console.WriteLine("2. Manage Products");
                Console.WriteLine("3. Manage Customers");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                string? option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        categoryMenu.Show();
                        break;

                    case "2":
                        productMenu.Show();
                        break;

                    case "3":
                        customerMenu.Show();
                        break;

                    case "0":
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}