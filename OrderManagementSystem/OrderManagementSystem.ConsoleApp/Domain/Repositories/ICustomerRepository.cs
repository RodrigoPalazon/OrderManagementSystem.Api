using OrderManagementSystem.ConsoleApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.ConsoleApp.Domain.Repositories
{
    internal interface ICustomerRepository
    {
        List<CustomerService> GetAll();
        CustomerService? GetById(int id);
        CustomerService? GetByEmail(string email);
        void Add(CustomerService customer);
        void Update(CustomerService customer);
        void Delete(int id);
    }
}
