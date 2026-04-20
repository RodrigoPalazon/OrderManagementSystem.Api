using OMS.Model;
using OMS.Services.Interfaces;

namespace OMS.ConsoleApp.Menus
{
    public class CategoryMenu
    {
        private readonly ICategoryService _categoryService;

        public CategoryMenu(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Category Menu ===");
                Console.WriteLine("1. List Categories");
                Console.WriteLine("2. Add Category");
                Console.WriteLine("3. Update Category");
                Console.WriteLine("4. Delete Category");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                var option = Console.ReadLine();

                try
                {
                    switch (option)
                    {
                        case "1":
                            ListCategories();
                            break;
                        case "2":
                            AddCategory();
                            break;
                        case "3":
                            UpdateCategory();
                            break;
                        case "4":
                            DeleteCategory();
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

        private void ListCategories()
        {
            Console.Clear();
            var categories = _categoryService.GetAllCategories();

            Console.WriteLine("=== Categories ===");

            if (!categories.Any())
            {
                Console.WriteLine("No categories found.");
            }
            else
            {
                foreach (var category in categories)
                {
                    Console.WriteLine($"Id: {category.Id} | Name: {category.Name} | Description: {category.Description}");
                }
            }

            Pause();
        }

        private void AddCategory()
        {
            Console.Clear();
            Console.WriteLine("=== Add Category ===");

            Console.Write("Id: ");
            int id = int.Parse(Console.ReadLine()!);

            Console.Write("Name: ");
            string name = Console.ReadLine()!;

            Console.Write("Description: ");
            string description = Console.ReadLine()!;

            var category = new Category
            {
                Id = id,
                Name = name,
                Description = description
            };

            _categoryService.CreateCategory(category);

            Console.WriteLine("Category created successfully.");
            Pause();
        }

        private void UpdateCategory()
        {
            Console.Clear();
            Console.WriteLine("=== Update Category ===");

            Console.Write("Enter category Id: ");
            int id = int.Parse(Console.ReadLine()!);

            var existingCategory = _categoryService.GetCategoryById(id);
            if (existingCategory == null)
            {
                Console.WriteLine("Category not found.");
                Pause();
                return;
            }

            Console.Write("New name: ");
            string name = Console.ReadLine()!;

            Console.Write("New description: ");
            string description = Console.ReadLine()!;

            existingCategory.Name = name;
            existingCategory.Description = description;

            _categoryService.UpdateCategory(existingCategory);

            Console.WriteLine("Category updated successfully.");
            Pause();
        }

        private void DeleteCategory()
        {
            Console.Clear();
            Console.WriteLine("=== Delete Category ===");

            Console.Write("Enter category Id: ");
            int id = int.Parse(Console.ReadLine()!);

            _categoryService.DeleteCategory(id);

            Console.WriteLine("Category deleted successfully.");
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