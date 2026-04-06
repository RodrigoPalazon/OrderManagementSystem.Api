using OrderManagementSystem.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.ConsoleApp.Interfaces
{
    internal class ICustomerRepository
    {
        List<Customer> GetAll();
        Customer? GetById(int id);
        Customer? GetByEmail(string email);
        void Add(Customer customer);
        void Update(Customer customer);
        void Delete(int id);
    }
}
