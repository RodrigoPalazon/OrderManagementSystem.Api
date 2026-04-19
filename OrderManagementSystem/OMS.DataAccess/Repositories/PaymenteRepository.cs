using OMS.DataAccess.Interfaces;
using OMS.Model;

namespace OMS.DataAccess.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly List<Payment> _payments = new();

        public List<Payment> GetAll()
        {
            return _payments;
        }

        public Payment? GetById(int id)
        {
            return _payments.FirstOrDefault(p => p.Id == id);
        }

        public Payment? GetByOrderId(int orderId)
        {
            return _payments.FirstOrDefault(p => p.OrderId == orderId);
        }

        public void Add(Payment payment)
        {
            _payments.Add(payment);
        }

        public void Update(Payment payment)
        {
            var existingPayment = GetById(payment.Id);

            if (existingPayment == null)
                return;

            existingPayment.OrderId = payment.OrderId;
            existingPayment.Amount = payment.Amount;
            existingPayment.PaymentDate = payment.PaymentDate;
            existingPayment.PaymentMethod = payment.PaymentMethod;
            existingPayment.Status = payment.Status;
        }

        public void Delete(int id)
        {
            var payment = GetById(id);

            if (payment != null)
            {
                _payments.Remove(payment);
            }
        }
    }
}