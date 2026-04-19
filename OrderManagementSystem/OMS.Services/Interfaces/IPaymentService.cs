using OMS.Model;

namespace OMS.Services.Interfaces
{
    public interface IPaymentService
    {
        List<Payment> GetAllPayments();
        Payment? GetPaymentById(int id);
        Payment? GetPaymentByOrderId(int orderId);
        void CreatePayment(Payment payment);
        void UpdatePayment(Payment payment);
        void DeletePayment(int id);
    }
}