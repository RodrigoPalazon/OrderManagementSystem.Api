using OrderManagementSystem.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.ConsoleApp.Interfaces
{
    internal class IProductRepository
    {
        List<Product> GetAll();
        Product? GetById(int id);
        List<Product> GetByCategoryId(int categoryId);
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
    }
}
