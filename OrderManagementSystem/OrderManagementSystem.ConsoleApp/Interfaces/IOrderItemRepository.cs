using OrderManagementSystem.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.ConsoleApp.Interfaces
{
    public interface IOrderItemRepository
    {
        List<OrderItem> GetAll();
        OrderItem? GetById(int id);
        List<OrderItem> GetByOrderId(int orderId);
        void Add(OrderItem orderItem);
        void Update(OrderItem orderItem);
        void Delete(int id);
    }
}
