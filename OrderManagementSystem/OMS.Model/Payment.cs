namespace OMS.Model
{
    public class Payment
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public PaymentStatus Status { get; set; }

        public bool isDebit { get; set; }

        public string TransactionReference { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;
    }
}