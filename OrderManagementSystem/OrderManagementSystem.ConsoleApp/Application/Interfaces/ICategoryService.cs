using OrderManagementSystem.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.ConsoleApp.Interfaces
{
    internal interface ICategoryService
    {
        List<Category> GetAll();
        Category? GetById(int id);
        Category? GetByName(string name);
        void Add(Category category);
        void Update(Category category);
        void Delete(int id);
    }
}
