namespace OrderManagementSystem.ConsoleApp.Domain.Entities
{
    public class Category
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public Category(int id, string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Category name is required.");

            Id = id;
            Name = name;
            Description = description ?? string.Empty;
        }

        public void Rename(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Category name is required.");

            Name = newName;
        }

        public void UpdateDescription(string description)
        {
            Description = description ?? string.Empty;
        }
    }
}