namespace OrderManagementSystem.ConsoleApp.Domain.Entities
{
    public class Customer
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Customer(int id, string firstName, string lastName, string email, string phone)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name is required.");
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name is required.");
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required.");

            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone ?? string.Empty;
            CreatedAt = DateTime.Now;
        }

        public void UpdateProfile(string firstName, string lastName, string email, string phone)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name is required.");
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name is required.");
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required.");

            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone ?? string.Empty;
        }

        public void ChangeEmail(string newEmail)
        {
            if (string.IsNullOrWhiteSpace(newEmail))
                throw new ArgumentException("Email is required.");

            Email = newEmail;
        }

        public void ChangePhone(string newPhone)
        {
            Phone = newPhone ?? string.Empty;
        }
    }
}