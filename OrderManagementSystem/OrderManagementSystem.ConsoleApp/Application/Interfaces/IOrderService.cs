using OrderManagementSystem.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.ConsoleApp.Application.Interfaces
{
    public interface IOrderService
    {
        List<Order> GetAll();
        Order? GetById(int id);
        List<Order> GetByCustomerId(int customerId);
        void Add(Order order);
        void Update(Order order);
        void Delete(int id);
    } 
}
