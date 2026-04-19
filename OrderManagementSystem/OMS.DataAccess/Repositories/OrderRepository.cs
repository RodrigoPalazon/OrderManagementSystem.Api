using OMS.DataAccess.Interfaces;
using OMS.Model;

namespace OMS.DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders = new();

        public List<Order> GetAll()
        {
            return _orders;
        }

        public Order? GetById(int id)
        {
            return _orders.FirstOrDefault(o => o.Id == id);
        }

        public List<Order> GetByCustomerId(int customerId)
        {
            return _orders.Where(o => o.CustomerId == customerId).ToList();
        }

        public void Add(Order order)
        {
            _orders.Add(order);
        }

        public void Update(Order order)
        {
            var existingOrder = GetById(order.Id);

            if (existingOrder == null)
                return;

            existingOrder.OrderDate = order.OrderDate;
            existingOrder.TotalAmount = order.TotalAmount;
            existingOrder.Status = order.Status;
            existingOrder.CustomerId = order.CustomerId;
            existingOrder.IsPaid = order.IsPaid;
        }

        public void Delete(int id)
        {
            var order = GetById(id);

            if (order != null)
            {
                _orders.Remove(order);
            }
        }
    }
}