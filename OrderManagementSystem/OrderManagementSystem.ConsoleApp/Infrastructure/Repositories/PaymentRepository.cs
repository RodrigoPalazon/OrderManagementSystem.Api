using OrderManagementSystem.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.ConsoleApp.Application.Interfaces
{
    public interface PaymentRepository
    {
        List<Payment> GetAll();
        Payment? GetById(int id);
        Payment? GetByOrderId(int orderId);
        void Add(Payment payment);
        void Update(Payment payment);
        void Delete(int id);
    }
}
