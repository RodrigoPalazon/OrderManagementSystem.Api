using OMS.DataAccess.Interfaces;
using OMS.Model;
using OMS.Services.Interfaces;

namespace OMS.Services.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;

        public PaymentService(IPaymentRepository paymentRepository, IOrderRepository orderRepository)
        {
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
        }

        public List<Payment> GetAllPayments()
        {
            return _paymentRepository.GetAll();
        }

        public Payment? GetPaymentById(int id)
        {
            return _paymentRepository.GetById(id);
        }

        public Payment? GetPaymentByOrderId(int orderId)
        {
            return _paymentRepository.GetByOrderId(orderId);
        }

        public void CreatePayment(Payment payment)
        {
            var order = _orderRepository.GetById(payment.OrderId);

            if (order == null)
                throw new InvalidOperationException("Order does not exist.");

            if (_paymentRepository.GetByOrderId(payment.OrderId) != null)
                throw new InvalidOperationException("A payment already exists for this order.");

            if (payment.Amount <= 0)
                throw new ArgumentException("Payment amount must be greater than zero.");

            _paymentRepository.Add(payment);
        }

        public void UpdatePayment(Payment payment)
        {
            var existingPayment = _paymentRepository.GetById(payment.Id);

            if (existingPayment == null)
                throw new InvalidOperationException("Payment not found.");

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