using OrderManagementSystem.ConsoleApp.Domain.Enums;

namespace OrderManagementSystem.ConsoleApp.Domain.Entities
{
    public class Payment
    {
        public int Id { get; private set; }
        public int OrderId { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime PaymentDate { get; private set; }
        public string PaymentMethod { get; private set; }
        public PaymentStatus Status { get; private set; }

        public Payment(int id, int orderId, decimal amount, string paymentMethod)
        {
            if (amount <= 0)
                throw new ArgumentException("Payment amount must be greater than zero.");
            if (string.IsNullOrWhiteSpace(paymentMethod))
                throw new ArgumentException("Payment method is required.");

            Id = id;
            OrderId = orderId;
            Amount = amount;
            PaymentMethod = paymentMethod;
            PaymentDate = DateTime.Now;
            Status = PaymentStatus.Pending;
        }

        public void MarkAsFailed()
        {
            Status = PaymentStatus.Failed;
        }

        public bool IsSuccessful()
        {
            return Status == PaymentStatus.Completed;
        }
    }
}