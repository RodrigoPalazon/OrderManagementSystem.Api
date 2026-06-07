using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OMS.ConsoleApp.Cli;
using OMS.ConsoleApp.Menus;
using OMS.DataAccess.Context;
using OMS.DataAccess.Interfaces;
using OMS.DataAccess.Repositories;
using OMS.Services.Interfaces;
using OMS.Services.Services;
using System.Runtime.CompilerServices;


namespace OMS.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection")!;

            var services = new ServiceCollection();

            // Configuration
            services.AddSingleton(configuration);

            // DbContext
            services.AddDbContext<OmsDbContext>(options =>
                options.UseSqlServer(connectionString)); //DI knows how to build your EF database context.

            // Repositories
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            // Services
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IProductService, ProductService>();

            // CLI handlers
            services.AddScoped<CategoryCliHandler>();

            // Menus
            services.AddScoped<CategoryMenu>();
            services.AddScoped<CustomerMenu>();
            services.AddScoped<ProductMenu>();

            var serviceProvider = services.BuildServiceProvider();//This turns the list of registrations into a real DI container.

            using var scope = serviceProvider.CreateScope();
            var provider = scope.ServiceProvider;

            var categoryCliHandler = provider.GetRequiredService<CategoryCliHandler>();

            bool handled = categoryCliHandler.Handle(args);
            if (handled)
            {
                return;
            }

            var categoryMenu = provider.GetRequiredService<CategoryMenu>();
            var customerMenu = provider.GetRequiredService<CustomerMenu>();
            var productMenu = provider.GetRequiredService<ProductMenu>();

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