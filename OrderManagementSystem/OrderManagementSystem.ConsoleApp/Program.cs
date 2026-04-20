using OMS.ConsoleApp.Menus;
using OMS.DataAccess.Interfaces;
using OMS.DataAccess.Repositories;
using OMS.Services.Interfaces;
using OMS.Services.Services;

namespace OMS.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ICategoryRepository categoryRepository = new CategoryRepository();
            ICustomerRepository customerRepository = new CustomerRepository();
            IProductRepository productRepository = new ProductRepository();

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
                Console.WriteLine("2. Manage Customers");
                Console.WriteLine("3. Manage Products");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                string? option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        categoryMenu.Show();
                        break;

                    case "2":
                        customerMenu.Show();
                        break;

                    case "3":
                        productMenu.Show();
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