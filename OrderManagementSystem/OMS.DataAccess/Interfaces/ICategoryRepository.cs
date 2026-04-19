using OMS.Model;

namespace OMS.DataAccess.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        Category? GetById(int id);
        Category? GetByName(string name);
        void Add(Category category);
        void Update(Category category);
        void Delete(int id);
    }
}