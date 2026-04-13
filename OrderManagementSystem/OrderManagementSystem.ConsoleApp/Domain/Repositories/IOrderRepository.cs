using OrderManagementSystem.ConsoleApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.ConsoleApp.Domain.Repositories
{
    public interface IOrderRepository
    {
        List<OrderService> GetAll();
        OrderService? GetById(int id);
        List<OrderService> GetByCustomerId(int customerId);
        void Add(OrderService order);
        void Update(OrderService order);
        void Delete(int id);
    } 
}
