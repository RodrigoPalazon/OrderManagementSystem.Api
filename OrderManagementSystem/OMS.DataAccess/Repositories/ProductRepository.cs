using OMS.DataAccess.Context;
using OMS.DataAccess.Interfaces;
using OMS.Model;

namespace OMS.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        //private readonly List<Product> _products = new();
        private readonly OmsDbContext _context;

        public ProductRepository(OmsDbContext context)
        {
            _context = context;
        }

        public List<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public Product? GetById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

        public List<Product> GetByCategoryId(int categoryId)
        {
            return _context.Products.Where(p => p.CategoryId == categoryId).ToList();
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            var existingProduct = GetById(product.Id);

            if (existingProduct == null)
                return;

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.StockQuantity = product.StockQuantity;
            existingProduct.IsActive = product.IsActive;
            existingProduct.CategoryId = product.CategoryId;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = GetById(id);

            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}