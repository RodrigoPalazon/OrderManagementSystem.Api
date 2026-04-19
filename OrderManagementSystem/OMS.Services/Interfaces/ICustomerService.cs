using OMS.Model;

namespace OMS.Services.Interfaces
{
    public interface ICustomerService
    {
        List<Customer> GetAllCustomers();
        Customer? GetCustomerById(int id);
        void CreateCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int id);
    }
}