using OMS.DataAccess.Context;
using OMS.DataAccess.Interfaces;
using OMS.Model;

namespace OMS.DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        //private readonly List<Category> _categories = new();
        private readonly OmsDbContext _context;

        public CategoryRepository(OmsDbContext context)
        {
            _context = context;
        }

        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }


        public Category? GetById(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }

        public Category? GetByName(string name)
        {
            return _context.Categories.FirstOrDefault(c =>
                c.Name == name);
        }


        public void Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var category = GetById(id);

            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }
    }
}