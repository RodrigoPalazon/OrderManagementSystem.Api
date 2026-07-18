using Moq;
using OMS.DataAccess.Interfaces;
using OMS.Model;
using OMS.Services.Services;

namespace OMS.Tests.Services
{
    public class CategoryServiceTests
    {
        [Fact]
        public void GetAllCategories_ShouldReturnAllCategories()
        {
            // Arrange
            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Electronics", Description = "Devices" },
                new Category { Id = 2, Name = "Pets", Description = "Pet supplies" }
            };

            categoryRepositoryMock
                .Setup(repo => repo.GetAll())
                .Returns(categories);

            var categoryService = new CategoryService(categoryRepositoryMock.Object);

            // Act
            var result = categoryService.GetAllCategories();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Electronics", result[0].Name);
            Assert.Equal("Pets", result[1].Name);
        }

        [Fact]
        public void GetCategoryById_ShouldReturnCategory_WhenCategoryExists()
        {
            // Arrange
            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            var category = new Category
            {
                Id = 1,
                Name = "Electronics",
                Description = "Devices"
            };

            categoryRepositoryMock
                .Setup(repo => repo.GetById(1))
                .Returns(category);

            var categoryService = new CategoryService(categoryRepositoryMock.Object);

            // Act
            var result = categoryService.GetCategoryById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result!.Id);
            Assert.Equal("Electronics", result.Name);
            Assert.Equal("Devices", result.Description);
        }

        [Fact]
        public void GetCategoryById_ShouldThrowInvalidOperationException_WhenCategoryDoesNotExist()
        {
            // Arrange
            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            categoryRepositoryMock
                .Setup(repo => repo.GetById(999))
                .Returns((Category?)null);

            var categoryService = new CategoryService(categoryRepositoryMock.Object);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() =>
                categoryService.GetCategoryById(999));

            Assert.Equal("Category not found.", exception.Message);
        }

        [Fact]
        public void CreateCategory_ShouldThrowArgumentException_WhenNameIsEmpty()
        {
            // Arrange
            var categoryRepositoryMock = new Mock<ICategoryRepository>();
            var categoryService = new CategoryService(categoryRepositoryMock.Object);

            var category = new Category
            {
                Name = "",
                Description = "Some description"
            };

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                categoryService.CreateCategory(category));

            Assert.Equal("Category name is required.", exception.Message);
        }

        [Fact]
        public void CreateCategory_ShouldThrowInvalidOperationException_WhenNameAlreadyExists()
        {
            // Arrange
            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            categoryRepositoryMock
                .Setup(repo => repo.GetByName("Electronics"))
                .Returns(new Category
                {
                    Id = 1,
                    Name = "Electronics",
                    Description = "Existing category"
                });

            var categoryService = new CategoryService(categoryRepositoryMock.Object);

            var category = new Category
            {
                Name = "Electronics",
                Description = "New description"
            };

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() =>
                categoryService.CreateCategory(category));

            Assert.Equal("A category with Electronics this name already exists.", exception.Message);
        }

        [Fact]
        public void CreateCategory_ShouldCallAdd_WhenCategoryIsValid()
        {
            // Arrange
            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            categoryRepositoryMock
                .Setup(repo => repo.GetByName("Pets"))
                .Returns((Category?)null);

            var categoryService = new CategoryService(categoryRepositoryMock.Object);

            var category = new Category
            {
                Name = "Pets",
                Description = "Pet accessories and food"
            };

            // Act
            categoryService.CreateCategory(category);

            // Assert
            categoryRepositoryMock.Verify(repo => repo.Add(category), Times.Once);
        }

        [Fact]
        public void UpdateCategory_ShouldThrowInvalidOperationException_WhenCategoryDoesNotExist()
        {
            // Arrange
            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            categoryRepositoryMock
                .Setup(repo => repo.GetById(100))
                .Returns((Category?)null);

            var categoryService = new CategoryService(categoryRepositoryMock.Object);

            var category = new Category
            {
                Id = 100,
                Name = "Updated Name",
                Description = "Updated Description"
            };

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() =>
                categoryService.UpdateCategory(category));

            Assert.Equal("Category not found.", exception.Message);
        }

        [Fact]
        public void UpdateCategory_ShouldThrowArgumentException_WhenNameIsEmpty()
        {
            // Arrange
            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            categoryRepositoryMock
                .Setup(repo => repo.GetById(1))
                .Returns(new Category
                {
                    Id = 1,
                    Name = "Old Name",
                    Description = "Old Description"
                });

            var categoryService = new CategoryService(categoryRepositoryMock.Object);

            var category = new Category
            {
                Id = 1,
                Name = "",
                Description = "Updated Description"
            };

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                categoryService.UpdateCategory(category));

            Assert.Equal("Category name is required.", exception.Message);
        }

        [Fact]
        public void UpdateCategory_ShouldCallUpdate_WhenCategoryIsValid()
        {
            // Arrange
            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            categoryRepositoryMock
                .Setup(repo => repo.GetById(1))
                .Returns(new Category
                {
                    Id = 1,
                    Name = "Old Name",
                    Description = "Old Description"
                });

            var categoryService = new CategoryService(categoryRepositoryMock.Object);

            var category = new Category
            {
                Id = 1,
                Name = "New Name",
                Description = "New Description"
            };

            // Act
            categoryService.UpdateCategory(category);

            // Assert
            categoryRepositoryMock.Verify(repo => repo.Update(category), Times.Once);
        }

        [Fact]
        public void DeleteCategory_ShouldThrowInvalidOperationException_WhenCategoryDoesNotExist()
        {
            // Arrange
            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            categoryRepositoryMock
                .Setup(repo => repo.GetById(123))
                .Returns((Category?)null);

            var categoryService = new CategoryService(categoryRepositoryMock.Object);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() =>
                categoryService.DeleteCategory(123));

            Assert.Equal("Category not found.", exception.Message);
        }

        [Fact]
        public void DeleteCategory_ShouldCallDelete_WhenCategoryExists()
        {
            // Arrange
            var categoryRepositoryMock = new Mock<ICategoryRepository>();

            categoryRepositoryMock
                .Setup(repo => repo.GetById(1))
                .Returns(new Category
                {
                    Id = 1,
                    Name = "Electronics",
                    Description = "Devices"
                });

            var categoryService = new CategoryService(categoryRepositoryMock.Object);

            // Act
            categoryService.DeleteCategory(1);

            // Assert
            categoryRepositoryMock.Verify(repo => repo.Delete(1), Times.Once);
        }
    }
}