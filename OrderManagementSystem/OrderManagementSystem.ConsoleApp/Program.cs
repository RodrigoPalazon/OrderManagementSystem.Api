using OMS.ConsoleApp.Menus;
using OMS.DataAccess.Interfaces;
using OMS.DataAccess.Repositories;
using OMS.Services.Interfaces;
using OMS.Services.Services;

namespace OMS.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ICategoryRepository categoryRepository = new CategoryRepository();
            ICategoryService categoryService = new CategoryService(categoryRepository);

            var categoryMenu = new CategoryMenu(categoryService);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Order Management System ===");
                Console.WriteLine("1. Manage Categories");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        categoryMenu.Show();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}