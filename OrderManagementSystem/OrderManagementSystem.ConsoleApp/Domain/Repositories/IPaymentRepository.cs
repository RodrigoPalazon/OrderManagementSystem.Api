using OrderManagementSystem.ConsoleApp.Domain.Entities;

namespace OrderManagementSystem.ConsoleApp.Domain.Repositories
{
    public interface IPaymentRepository
    {
        List<Payment> GetAll();
        Payment? GetById(int id);
        Payment? GetByOrderId(int orderId);
        void Add(Payment payment);
        void Update(Payment payment);
        void Delete(int id);
    }
}