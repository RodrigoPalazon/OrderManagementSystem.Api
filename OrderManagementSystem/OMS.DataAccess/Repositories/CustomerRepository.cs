using Microsoft.EntityFrameworkCore;
using OMS.DataAccess.Context;
using OMS.DataAccess.Interfaces;
using OMS.Model;

namespace OMS.DataAccess.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        //private readonly List<Customer> _customers = new();
        private readonly OmsDbContext _context; 

        public CustomerRepository(OmsDbContext context)
        {
            _context = context;
        }

        public List<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }

        public Customer? GetById(int id)
        {
            return _context.Customers.FirstOrDefault(c => c.Id == id);
        }

        public Customer? GetByEmail(string email)
        {
            return _context.Customers.FirstOrDefault(c =>
                c.Email == email);
        }

        public void Add(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void Update(Customer customer)
        {
            var existingCustomer = GetById(customer.Id);

            if (existingCustomer == null)
                return;

            existingCustomer.FirstName = customer.FirstName;
            existingCustomer.LastName = customer.LastName;
            existingCustomer.Email = customer.Email;
            existingCustomer.Phone = customer.Phone;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var customer = GetById(id);

            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
        }
    }
}