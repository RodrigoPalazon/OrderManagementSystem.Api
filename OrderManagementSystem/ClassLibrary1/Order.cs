namespace OMS.Model
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public int CustomerId { get; set; }
        public bool IsPaid { get; set; }

        public Order()
        {
            OrderDate = DateTime.Now;
            Status = OrderStatus.Pending;
            IsPaid = false;
        }
    }
}