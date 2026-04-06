using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.ConsoleApp.Models
{
    internal class Payment
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.Now;

        public string PaymentMethod { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
    }
}
