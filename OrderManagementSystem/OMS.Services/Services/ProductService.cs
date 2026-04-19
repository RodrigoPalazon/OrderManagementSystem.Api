using OMS.DataAccess.Interfaces;
using OMS.Model;
using OMS.Services.Interfaces;

namespace OMS.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAll();
        }

        public Product? GetProductById(int id)
        {
            return _productRepository.GetById(id);
        }

        public List<Product> GetProductsByCategoryId(int categoryId)
        {
            return _productRepository.GetByCategoryId(categoryId);
        }

        public void CreateProduct(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("Product name is required.");

            if (product.Price <= 0)
                throw new ArgumentException("Product price must be greater than zero.");

            if (product.StockQuantity < 0)
                throw new ArgumentException("Stock quantity cannot be negative.");

            if (_categoryRepository.GetById(product.CategoryId) == null)
                throw new InvalidOperationException("Category does not exist.");

            _productRepository.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            var existingProduct = _productRepository.GetById(product.Id);

            if (existingProduct == null)
                throw new InvalidOperationException("Product not found.");

            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("Product name is required.");

            if (product.Price <= 0)
                throw new ArgumentException("Product price must be greater than zero.");

            if (product.StockQuantity < 0)
                throw new ArgumentException("Stock quantity cannot be negative.");

            if (_categoryRepository.GetById(product.CategoryId) == null)
                throw new InvalidOperationException("Category does not exist.");

            _productRepository.Update(product);
        }

        public void DeleteProduct(int id)
        {
            var existingProduct = _productRepository.GetById(id);

            if (existingProduct == null)
                throw new InvalidOperationException("Product not found.");

            _productRepository.Delete(id);
        }
    }
}