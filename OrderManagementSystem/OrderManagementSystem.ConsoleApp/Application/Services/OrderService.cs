using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.ConsoleApp.Domain.Entities
{
    internal class OrderService
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = string.Empty;

        public int CustomerId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
