using OrderManagementSystem.ConsoleApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.ConsoleApp.Domain.Repositories
{
    internal interface IProductRepository
    {
        List<Product> GetAll();
        Product? GetById(int id);
        List<Product> GetByCategoryId(int categoryId);
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
    }
}
