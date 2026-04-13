namespace OrderManagementSystem.ConsoleApp.Domain.Entities
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int StockQuantity { get; private set; }
        public bool IsActive { get; private set; }
        public int CategoryId { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Product(int id, string name, string description, decimal price, int stockQuantity, int categoryId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name is required.");
            if (price <= 0)
                throw new ArgumentException("Price must be greater than zero.");
            if (stockQuantity < 0)
                throw new ArgumentException("Stock cannot be negative.");

            Id = id;
            Name = name;
            Description = description ?? string.Empty;
            Price = price;
            StockQuantity = stockQuantity;
            CategoryId = categoryId;
            IsActive = true;
            CreatedAt = DateTime.Now;
        }

        public void UpdateDetails(string name, string description, decimal price, int categoryId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name is required.");
            if (price <= 0)
                throw new ArgumentException("Price must be greater than zero.");

            Name = name;
            Description = description ?? string.Empty;
            Price = price;
            CategoryId = categoryId;
        }

        public void IncreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            StockQuantity += quantity;
        }

        public void DecreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");
            if (quantity > StockQuantity)
                throw new InvalidOperationException("Not enough stock.");

            StockQuantity -= quantity;
        }

        public void ChangePrice(decimal newPrice)
        {
            if (newPrice <= 0)
                throw new ArgumentException("Price must be greater than zero.");

            Price = newPrice;
        }

        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;

        public bool HasEnoughStock(int quantity) => quantity > 0 && StockQuantity >= quantity;
    }
}