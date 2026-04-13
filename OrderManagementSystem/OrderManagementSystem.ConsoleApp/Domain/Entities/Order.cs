using OrderManagementSystem.ConsoleApp.Domain.Enums;

namespace OrderManagementSystem.ConsoleApp.Domain.Entities
{
    public class Order
    {
        private readonly List<OrderItem> _items = new();

        public int Id { get; private set; }
        public int CustomerId { get; private set; }
        public DateTime OrderDate { get; private set; }
        public decimal TotalAmount { get; private set; }
        public OrderStatus Status { get; private set; }
        public bool IsPaid { get; private set; }

        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

        public Order(int id, int customerId)
        {
            Id = id;
            CustomerId = customerId;
            OrderDate = DateTime.Now;
            Status = OrderStatus.Pending;
            IsPaid = false;
            TotalAmount = 0;
        }

        public void AddItem(int orderItemId, Product product, int quantity)
        {
            if (!product.IsActive)
                throw new InvalidOperationException("Inactive products cannot be added.");
            if (!product.HasEnoughStock(quantity))
                throw new InvalidOperationException("Not enough stock.");

            var existingItem = _items.FirstOrDefault(i => i.ProductId == product.Id);
            if (existingItem != null)
            {
                existingItem.UpdateQuantity(existingItem.Quantity + quantity);
            }
            else
            {
                _items.Add(new OrderItem(orderItemId, product.Id, quantity, product.Price));
            }

            CalculateTotal();
        }

        public void RemoveItem(int productId)
        {
            var item = _items.FirstOrDefault(i => i.ProductId == productId);
            if (item == null)
                throw new InvalidOperationException("Order item not found.");

            _items.Remove(item);
            CalculateTotal();
        }

        public void UpdateItemQuantity(int productId, int quantity)
        {
            var item = _items.FirstOrDefault(i => i.ProductId == productId);
            if (item == null)
                throw new InvalidOperationException("Order item not found.");

            item.UpdateQuantity(quantity);
            CalculateTotal();
        }

        public void CalculateTotal()
        {
            TotalAmount = _items.Sum(i => i.LineTotal);
        }

        public void MarkAsPaid()
        {
            if (!_items.Any())
                throw new InvalidOperationException("Cannot pay an empty order.");

            IsPaid = true;
            Status = OrderStatus.Paid;
        }

        public void Cancel()
        {
            if (!CanBeCancelled())
                throw new InvalidOperationException("Order cannot be cancelled.");

            Status = OrderStatus.Cancelled;
        }

        public bool CanBeCancelled()
        {
            return Status != OrderStatus.Cancelled && !IsPaid;
        }

        public decimal GetRemainingBalance()
        {
            return IsPaid ? 0 : TotalAmount;
        }
    }
}