using OrderManagementSystem.ConsoleApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.ConsoleApp.Domain.Repositories
{
    internal interface ICategoryRepository
    {
        List<Category> GetAll();
        Category? GetById(int id);
        Category? GetByName(string name);
        void Add(Category category);
        void Update(Category category);
        void Delete(int id);
    }
}
