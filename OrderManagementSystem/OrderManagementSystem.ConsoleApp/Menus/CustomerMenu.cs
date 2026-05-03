using OMS.Model;
using OMS.Services.Interfaces;

namespace OMS.ConsoleApp.Menus
{
    public class CustomerMenu
    {
        private readonly ICustomerService _customerService;

        public CustomerMenu(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Customer Menu ===");
                Console.WriteLine("1. List Customers");
                Console.WriteLine("2. Add Customer");
                Console.WriteLine("3. Update Customer");
                Console.WriteLine("4. Delete Customer");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");

                var option = Console.ReadLine();

                try
                {
                    switch (option)
                    {
                        case "1":
                            ListCustomers();
                            break;
                        case "2":
                            AddCustomer();
                            break;
                        case "3":
                            UpdateCustomer();
                            break;
                        case "4":
                            DeleteCustomer();
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Invalid option.");
                            Pause();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Pause();
                }
            }
        }

        private void ListCustomers()
        {
            Console.Clear();
            var customers = _customerService.GetAllCustomers();

            Console.WriteLine("=== Customers ===");

            if (!customers.Any())
            {
                Console.WriteLine("No customers found.");
            }
            else
            {
                foreach (var customer in customers)
                {
                    Console.WriteLine($"Id: {customer.Id} | {customer.FirstName} {customer.LastName} | Email: {customer.Email}");
                }
            }

            Pause();
        }

        private void AddCustomer()
        {
            Console.Clear();
            Console.WriteLine("=== Add Customer ===");

            Console.Write("First Name: ");
            string firstName = Console.ReadLine()!;

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine()!;

            Console.Write("Email: ");
            string email = Console.ReadLine()!;

            Console.Write("Phone: ");
            string phone = Console.ReadLine()!;

            var customer = new Customer
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone,
                CreatedAt = DateTime.Now
            };

            _customerService.CreateCustomer(customer);

            Console.WriteLine("Customer created successfully.");
            Pause();
        }

        private void UpdateCustomer()
        {
            try
            {

            Console.Clear();
            Console.WriteLine("=== Update Customer ===");

            Console.Write("Enter customer Id: ");
            int id = int.Parse(Console.ReadLine()!);

            var existingCustomer = _customerService.GetCustomerById(id);
            if (existingCustomer == null)
            {
                Console.WriteLine("Customer not found.");
                Pause();
                return;
            }

            Console.Write("New First Name: ");
            existingCustomer.FirstName = Console.ReadLine()!;

            Console.Write("New Last Name: ");
            existingCustomer.LastName = Console.ReadLine()!;

            Console.Write("New Email: ");
            existingCustomer.Email = Console.ReadLine()!;

            Console.Write("New Phone: ");
            existingCustomer.Phone = Console.ReadLine()!;

            _customerService.UpdateCustomer(existingCustomer);

            Console.WriteLine("Customer updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Pause();
        }

        private void DeleteCustomer()
        {
            Console.Clear();
            Console.WriteLine("=== Delete Customer ===");

            Console.Write("Enter customer Id: ");
            int id = int.Parse(Console.ReadLine()!);

            _customerService.DeleteCustomer(id);

            Console.WriteLine("Customer deleted successfully.");
            Pause();
        }

        private static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}