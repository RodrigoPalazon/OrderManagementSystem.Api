using OMS.DataAccess.Context;
using OMS.DataAccess.Interfaces;
using OMS.Model;

namespace OMS.DataAccess.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly OmsDbContext _context;

        public PaymentRepository(OmsDbContext context)
        {
            _context = context;
        }

        public List<Payment> GetAll()
        {
            return _context.Payments.ToList();
        }

        public Payment? GetById(int id)
        {
            return _context.Payments.FirstOrDefault(p => p.Id == id);
        }

        public List<Payment> GetByOrderId(int orderId)
        {
            return _context.Payments.Where(p => p.OrderId == orderId).ToList();
        }

        public void Add(Payment payment)
        {
            _context.Payments.Add(payment);
            _context.SaveChanges();
        }

        public void Update(Payment payment)
        {
            var existingPayment = _context.Payments.FirstOrDefault(p => p.Id == payment.Id);

            if (existingPayment == null)
                return;

            existingPayment.Amount = payment.Amount;
            existingPayment.PaymentDate = payment.PaymentDate;
            existingPayment.PaymentMethod = payment.PaymentMethod;
            existingPayment.Status = payment.Status;
            existingPayment.TransactionReference = payment.TransactionReference;
            existingPayment.Notes = payment.Notes;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var payment = _context.Payments.FirstOrDefault(p => p.Id == id);

            if (payment != null)
            {
                _context.Payments.Remove(payment);
                _context.SaveChanges();
            }
        }
    }
}