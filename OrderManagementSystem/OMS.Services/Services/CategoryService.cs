using OMS.DataAccess.Interfaces;
using OMS.Model;
using OMS.Services.Interfaces;

namespace OMS.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public List<Category> GetAllCategories()
        {
            return _categoryRepository.GetAll();
        }

        public Category? GetCategoryById(int id)
        {
            return _categoryRepository.GetById(id);
        }

        public void CreateCategory(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.Name))
                throw new ArgumentException("Category name is required.");

            if (_categoryRepository.GetByName(category.Name) != null)
                throw new InvalidOperationException("A category with this name already exists.");

            _categoryRepository.Add(category);
        }

        public void UpdateCategory(Category category)
        {
            var existingCategory = _categoryRepository.GetById(category.Id);

            if (existingCategory == null)
                throw new InvalidOperationException("Category not found.");

            if (string.IsNullOrWhiteSpace(category.Name))
                throw new ArgumentException("Category name is required.");

            _categoryRepository.Update(category);
        }

        public void DeleteCategory(int id)
        {
            var existingCategory = _categoryRepository.GetById(id);

            if (existingCategory == null)
                throw new InvalidOperationException("Category not found.");

            _categoryRepository.Delete(id);
        }
    }
}