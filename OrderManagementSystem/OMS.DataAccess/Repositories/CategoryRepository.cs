using OMS.DataAccess.Interfaces;
using OMS.Model;

namespace OMS.DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly List<Category> _categories = new();

        public List<Category> GetAll()
        {
            return _categories;
        }

        public Category? GetById(int id)
        {
            return _categories.FirstOrDefault(c => c.Id == id);
        }

        public Category? GetByName(string name)
        {
            return _categories.FirstOrDefault(c =>
                c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public void Add(Category category)
        {
            _categories.Add(category);
        }

        public void Update(Category category)
        {
            var existingCategory = GetById(category.Id);

            if (existingCategory == null)
                return;

            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;
        }

        public void Delete(int id)
        {
            var category = GetById(id);

            if (category != null)
            {
                _categories.Remove(category);
            }
        }
    }
}