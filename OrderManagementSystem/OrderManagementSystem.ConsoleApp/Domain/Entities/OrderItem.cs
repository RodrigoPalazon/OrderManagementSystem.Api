namespace OrderManagementSystem.ConsoleApp.Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal LineTotal { get; private set; }

        public OrderItem(int id, int productId, int quantity, decimal unitPrice)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");
            if (unitPrice <= 0)
                throw new ArgumentException("Unit price must be greater than zero.");

            Id = id;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            LineTotal = quantity * unitPrice;
        }

        public void UpdateQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            Quantity = quantity;
            Recalculate();
        }

        public void UpdatePrice(decimal unitPrice)
        {
            if (unitPrice <= 0)
                throw new ArgumentException("Unit price must be greater than zero.");

            UnitPrice = unitPrice;
            Recalculate();
        }

        private void Recalculate()
        {
            LineTotal = Quantity * UnitPrice;
        }
    }
}