using OMS.Model;

namespace OMS.Services.Interfaces
{
    public interface IOrderService
    {
        List<Order> GetAllOrders();
        Order? GetOrderById(int id);
        List<Order> GetOrdersByCustomerId(int customerId);
        void CreateOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(int id);
    }
}