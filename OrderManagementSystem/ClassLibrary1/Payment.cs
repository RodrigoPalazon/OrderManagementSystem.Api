namespace OMS.Model
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public PaymentStatus Status { get; set; }

        public Payment()
        {
            PaymentDate = DateTime.Now;
            PaymentMethod = string.Empty;
            Status = PaymentStatus.Pending;
        }
    }
}