using OMS.Model;

namespace OMS.Api.DTOs
{
    public class CreatePaymentRequest
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string? TransactionReference { get; set; }
        public string? Notes { get; set; }
    }
}