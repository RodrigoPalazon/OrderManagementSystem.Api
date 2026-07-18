
using Moq;
using OMS.Model;
using OMS.Services.Interfaces;
using OMS.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using OMS.Api.DTOs;

namespace OMS.Tests.Controllers
{
    public class CategoriesControllerTests
    {
        [Fact]
        public void GetAll_ShouldReturnOkObjectResult_WithListOfCategories()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryService>();

            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Electronics", Description = "Devices" },
                new Category { Id = 2, Name = "Pets", Description = "Pet supplies" }
            };

            categoryServiceMock
                .Setup(service => service.GetAllCategories())
                .Returns(categories);

            var controller = new CategoriesController(categoryServiceMock.Object);

            // Act
            var result = controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCategories = Assert.IsType<List<Category>>(okResult.Value);

            Assert.Equal(2, returnedCategories.Count);
            Assert.Equal("Electronics", returnedCategories[0].Name);
            Assert.Equal("Pets", returnedCategories[1].Name);
        }

        [Fact]
        public void GetById_ShouldReturnOkObjectResult_WhenCategoryExists()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryService>();

            var category = new Category
            {
                Id = 1,
                Name = "Electronics",
                Description = "Devices"
            };

            categoryServiceMock
                .Setup(service => service.GetCategoryById(1))
                .Returns(category);

            var controller = new CategoriesController(categoryServiceMock.Object);

            // Act
            var result = controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedCategory = Assert.IsType<Category>(okResult.Value);

            Assert.Equal(1, returnedCategory.Id);
            Assert.Equal("Electronics", returnedCategory.Name);
        }

        [Fact]
        public void GetById_ShouldReturnNotFoundObjectResult_WhenCategoryDoesNotExist()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryService>();

            categoryServiceMock
                .Setup(service => service.GetCategoryById(999))
                .Throws(new InvalidOperationException("Category not found."));

            var controller = new CategoriesController(categoryServiceMock.Object);

            // Act
            var result = controller.GetById(999);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("Category not found.", notFoundResult.Value);
        }

        [Fact]
        public void Create_ShouldReturnCreatedAtAction_WhenRequestIsValid()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryService>();

            var request = new CreateCategoryRequest
            {
                Name = "Housing",
                Description = "Furniture and decorations"
            };

            var controller = new CategoriesController(categoryServiceMock.Object);

            // Act
            var result = controller.Create(request);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);

            Assert.Equal(nameof(CategoriesController.GetById), createdAtActionResult.ActionName);

            var returnedCategory = Assert.IsType<Category>(createdAtActionResult.Value);
            Assert.Equal("Housing", returnedCategory.Name);
            Assert.Equal("Furniture and decorations", returnedCategory.Description);

            categoryServiceMock.Verify(service => service.CreateCategory(It.IsAny<Category>()), Times.Once);
        }

        [Fact]
        public void Create_ShouldReturnBadRequest_WhenServiceThrowsArgumentException()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryService>();

            categoryServiceMock
                .Setup(service => service.CreateCategory(It.IsAny<Category>()))
                .Throws(new ArgumentException("Category name is required."));

            var request = new CreateCategoryRequest
            {
                Name = "",
                Description = "Some description"
            };

            var controller = new CategoriesController(categoryServiceMock.Object);

            // Act
            var result = controller.Create(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Category name is required.", badRequestResult.Value);
        }

        [Fact]
        public void Create_ShouldReturnConflict_WhenServiceThrowsInvalidOperationException()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryService>();

            categoryServiceMock
                .Setup(service => service.CreateCategory(It.IsAny<Category>()))
                .Throws(new InvalidOperationException("A category with the name 'Electronics' already exists."));

            var request = new CreateCategoryRequest
            {
                Name = "Electronics",
                Description = "Devices"
            };

            var controller = new CategoriesController(categoryServiceMock.Object);

            // Act
            var result = controller.Create(request);

            // Assert
            var conflictResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.Equal("A category with the name 'Electronics' already exists.", conflictResult.Value);
        }

        [Fact]
        public void Update_ShouldReturnBadRequest_WhenRouteIdAndBodyIdDoNotMatch()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryService>();

            var category = new Category
            {
                Id = 2,
                Name = "Updated Name",
                Description = "Updated Description"
            };

            var controller = new CategoriesController(categoryServiceMock.Object);

            // Act
            var result = controller.Update(1, category);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Route id and category id must match.", badRequestResult.Value);
        }

        [Fact]
        public void Update_ShouldReturnNoContent_WhenCategoryIsValid()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryService>();

            var category = new Category
            {
                Id = 1,
                Name = "Updated Name",
                Description = "Updated Description"
            };

            var controller = new CategoriesController(categoryServiceMock.Object);

            // Act
            var result = controller.Update(1, category);

            // Assert
            Assert.IsType<NoContentResult>(result);
            categoryServiceMock.Verify(service => service.UpdateCategory(category), Times.Once);
        }

        [Fact]
        public void Update_ShouldReturnNotFound_WhenServiceThrowsInvalidOperationException()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryService>();

            categoryServiceMock
                .Setup(service => service.UpdateCategory(It.IsAny<Category>()))
                .Throws(new InvalidOperationException("Category not found."));

            var category = new Category
            {
                Id = 1,
                Name = "Updated Name",
                Description = "Updated Description"
            };

            var controller = new CategoriesController(categoryServiceMock.Object);

            // Act
            var result = controller.Update(1, category);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Category not found.", notFoundResult.Value);
        }

        [Fact]
        public void Update_ShouldReturnBadRequest_WhenServiceThrowsArgumentException()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryService>();

            categoryServiceMock
                .Setup(service => service.UpdateCategory(It.IsAny<Category>()))
                .Throws(new ArgumentException("Category name is required."));

            var category = new Category
            {
                Id = 1,
                Name = "",
                Description = "Updated Description"
            };

            var controller = new CategoriesController(categoryServiceMock.Object);

            // Act
            var result = controller.Update(1, category);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Category name is required.", badRequestResult.Value);
        }

        [Fact]
        public void Delete_ShouldReturnNoContent_WhenCategoryExists()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryService>();
            var controller = new CategoriesController(categoryServiceMock.Object);

            // Act
            var result = controller.Delete(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
            categoryServiceMock.Verify(service => service.DeleteCategory(1), Times.Once);
        }

        [Fact]
        public void Delete_ShouldReturnNotFound_WhenServiceThrowsInvalidOperationException()
        {
            // Arrange
            var categoryServiceMock = new Mock<ICategoryService>();

            categoryServiceMock
                .Setup(service => service.DeleteCategory(999))
                .Throws(new InvalidOperationException("Category not found."));

            var controller = new CategoriesController(categoryServiceMock.Object);

            // Act
            var result = controller.Delete(999);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Category not found.", notFoundResult.Value);
        }
    }
}