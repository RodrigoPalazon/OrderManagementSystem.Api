using OMS.DataAccess.Interfaces;
using OMS.Model;

namespace OMS.DataAccess.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly List<Customer> _customers = new();

        public List<Customer> GetAll()
        {
            return _customers;
        }

        public Customer? GetById(int id)
        {
            return _customers.FirstOrDefault(c => c.Id == id);
        }

        public Customer? GetByEmail(string email)
        {
            return _customers.FirstOrDefault(c =>
                c.Email == email);
        }

        public void Add(Customer customer)
        {
            _customers.Add(customer);
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
        }

        public void Delete(int id)
        {
            var customer = GetById(id);

            if (customer != null)
            {
                _customers.Remove(customer);
            }
        }
    }
}