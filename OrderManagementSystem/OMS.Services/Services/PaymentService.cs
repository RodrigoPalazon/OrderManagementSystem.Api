using OMS.DataAccess.Interfaces;
using OMS.Model;
using OMS.Services.Interfaces;

namespace OMS.Services.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public List<Payment> GetAllPayments()
        {
            return _paymentRepository.GetAll();
        }

        public Payment? GetPaymentById(int id)
        {
            return _paymentRepository.GetById(id);
        }

        public List<Payment> GetPaymentsByOrderId(int orderId)
        {
            return _paymentRepository.GetByOrderId(orderId);
        }

        public void CreatePayment(Payment payment)
        {
            if (payment.OrderId <= 0)
                throw new ArgumentException("OrderId is required.");

            if (payment.Amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.");

            _paymentRepository.Add(payment);
        }

        public void UpdatePayment(Payment payment)
        {
            var existingPayment = _paymentRepository.GetById(payment.Id);

            if (existingPayment == null)
                throw new InvalidOperationException("Payment not found.");

            if (payment.Amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.");

            _paymentRepository.Update(payment);
        }

        public void DeletePayment(int id)
        {
            var existingPayment = _paymentRepository.GetById(id);

            if (existingPayment == null)
                throw new InvalidOperationException("Payment not found.");

            _paymentRepository.Delete(id);
        }
    }
}