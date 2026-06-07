using OMS.Model;
using OMS.Services.Interfaces;

namespace OMS.ConsoleApp.Cli
{
    public class CategoryCliHandler
    {
        private readonly ICategoryService _categoryService;

        public CategoryCliHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public bool Handle(string[] args)
        {
            (bool flowControl, bool value) = ValidateInput(args);

            if (!flowControl)
            {
                return value;
            }

            string name = string.Empty;
            string description = string.Empty;

            AssignNameAndDescription(args, ref name, ref description);

            (flowControl, value) = ValidateName(name);

            if (!flowControl)
            {
                return value;
            }

            AddCategory(name, description);

            Console.WriteLine($"Category '{name}' created successfully.");
            return true;
        }

        private static (bool flowControl, bool value) ValidateInput(string[] args)
        {
            if (args.Length == 0)
                return (flowControl: false, value: false);

            var command = args[0].ToLower();

            if (command != "add-category")
                return (flowControl: false, value: false);

            return (flowControl: true, value: false);
        }

        private static void AssignNameAndDescription(string[] args, ref string name, ref string description)
        {
            for (int i = 1; i < args.Length; i++)
            {
                if (args[i] == "--name" && i + 1 < args.Length)
                {
                    name = args[i + 1];
                    i++;
                }
                else if (args[i] == "--description" && i + 1 < args.Length)
                {
                    description = args[i + 1];
                    i++;
                }
            }
        }

        private void AddCategory(string name, string description)
        {
            var category = new Category
            {
                Name = name,
                Description = description
            };

            _categoryService.CreateCategory(category);
        }

        private static (bool flowControl, bool value) ValidateName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Error: --name is required.");
                return (flowControl: false, value: true);
            }

            return (flowControl: true, value: false);
        }
    }
}