using OMS.DataAccess.Interfaces;
using OMS.Model;
using OMS.Services.Interfaces;

namespace OMS.Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;

        public OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }

        public List<Order> GetAllOrders()
        {
            return _orderRepository.GetAll();
        }

        public Order? GetOrderById(int id)
        {
            return _orderRepository.GetById(id);
        }

        public List<Order> GetOrdersByCustomerId(int customerId)
        {
            return _orderRepository.GetByCustomerId(customerId);
        }

        public void CreateOrder(Order order)
        {
            if (_customerRepository.GetById(order.CustomerId) == null)
                throw new InvalidOperationException("Customer does not exist.");

            if (order.TotalAmount < 0)
                throw new ArgumentException("Order total cannot be negative.");

            _orderRepository.Add(order);
        }

        public void UpdateOrder(Order order)
        {
            var existingOrder = _orderRepository.GetById(order.Id);

            if (existingOrder == null)
                throw new InvalidOperationException("Order not found.");

            if (_customerRepository.GetById(order.CustomerId) == null)
                throw new InvalidOperationException("Customer does not exist.");

            _orderRepository.Update(order);
        }

        public void DeleteOrder(int id)
        {
            var existingOrder = _orderRepository.GetById(id);

            if (existingOrder == null)
                throw new InvalidOperationException("Order not found.");

            _orderRepository.Delete(id);
        }
    }
}