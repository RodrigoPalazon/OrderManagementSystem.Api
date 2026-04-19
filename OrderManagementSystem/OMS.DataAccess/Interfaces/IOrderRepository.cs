using OMS.Model;

namespace OMS.DataAccess.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> GetAll();
        Order? GetById(int id);
        List<Order> GetByCustomerId(int customerId);
        void Add(Order order);
        void Update(Order order);
        void Delete(int id);
    }
}