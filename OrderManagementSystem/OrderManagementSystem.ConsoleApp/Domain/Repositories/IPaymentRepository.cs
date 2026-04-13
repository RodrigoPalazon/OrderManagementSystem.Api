using OrderManagementSystem.ConsoleApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.ConsoleApp.Domain.Repositories
{
    public interface IPaymentRepository
    {
        List<PaymentService> GetAll();
        PaymentService? GetById(int id);
        PaymentService? GetByOrderId(int orderId);
        void Add(PaymentService payment);
        void Update(PaymentService payment);
        void Delete(int id);
    }
}
