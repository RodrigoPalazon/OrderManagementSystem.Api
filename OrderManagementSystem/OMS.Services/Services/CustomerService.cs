using OMS.DataAccess.Interfaces;
using OMS.Model;
using OMS.Services.Interfaces;

namespace OMS.Services.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerRepository.GetAll();
        }

        public Customer? GetCustomerById(int id)
        {
            return _customerRepository.GetById(id);
        }

        public void CreateCustomer(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.FirstName))
                throw new ArgumentException("First name is required.");

            if (string.IsNullOrWhiteSpace(customer.LastName))
                throw new ArgumentException("Last name is required.");

            if (string.IsNullOrWhiteSpace(customer.Email))
                throw new ArgumentException("Email is required.");

            if (_customerRepository.GetByEmail(customer.Email) != null)
                throw new InvalidOperationException("A customer with this email already exists.");

            _customerRepository.Add(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            var existingCustomer = _customerRepository.GetById(customer.Id);

            if (existingCustomer == null)
                throw new InvalidOperationException("Customer not found.");

            if (string.IsNullOrWhiteSpace(customer.FirstName))
                throw new ArgumentException("First name is required.");

            if (string.IsNullOrWhiteSpace(customer.LastName))
                throw new ArgumentException("Last name is required.");

            if (string.IsNullOrWhiteSpace(customer.Email))
                throw new ArgumentException("Email is required.");

            _customerRepository.Update(customer);
        }

        public void DeleteCustomer(int id)
        {
            var existingCustomer = _customerRepository.GetById(id);

            if (existingCustomer == null)
                throw new InvalidOperationException("Customer not found.");

            _customerRepository.Delete(id);
        }
    }
}