using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OMS.DataAccess.Context;
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
            ICategoryService categoryService = new CategoryService(categoryRepository);

            CategoryMenu categoryMenu = new CategoryMenu(categoryService);

            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("=== Order Management System ===");
                Console.WriteLine("1. Manage Categories");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                string? option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        categoryMenu.Show();
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