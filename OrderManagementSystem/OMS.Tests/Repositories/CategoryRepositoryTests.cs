using Microsoft.EntityFrameworkCore;
using OMS.DataAccess.Context;
using OMS.DataAccess.Repositories;
using OMS.Model;

namespace OMS.Tests.Repositories
{
    public class CategoryRepositoryTests
    {
        private OmsDbContext CreateDbContext()
        {
            //initialize builder
            var builder = new DbContextOptionsBuilder<OmsDbContext>();

            // run its method to configure options
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            // get the options
            DbContextOptions<OmsDbContext> options = builder.Options;

            // create new DbContext with options created
            return new OmsDbContext(options);
        }

        [Fact]
        public void GetAll_ShouldReturnAllCategories()
        {
            // Arrange
            using var context = CreateDbContext();

            context.Categories.AddRange(
                new Category { Id = 1, Name = "Electronics", Description = "Devices" },// add  "milions of data" and check the RAM memory, set a break point
                new Category { Id = 2, Name = "Pets", Description = "Pet products" }
            );
            context.SaveChanges();

            var repository = new CategoryRepository(context);

            // Act
            var result = repository.GetAll();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Electronics", result[0].Name);
            Assert.Equal("Pets", result[1].Name);
        }

        [Fact]
        public void GetById_ShouldReturnCategory_WhenCategoryExists()
        {
            // Arrange
            using var context = CreateDbContext();

            context.Categories.Add(new Category
            {
                Id = 1,
                Name = "Electronics",
                Description = "Devices"
            });
            context.SaveChanges();

            var repository = new CategoryRepository(context);

            // Act
            var result = repository.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result!.Id);
            Assert.Equal("Electronics", result.Name);
        }

        [Fact]
        public void GetById_ShouldReturnNull_WhenCategoryDoesNotExist()
        {
            // Arrange
            using var context = CreateDbContext();
            var repository = new CategoryRepository(context);

            // Act
            var result = repository.GetById(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetByName_ShouldReturnCategory_WhenCategoryExists()
        {
            // Arrange
            using var context = CreateDbContext();

            context.Categories.Add(new Category
            {
                Id = 1,
                Name = "Electronics",
                Description = "Devices"
            });
            context.SaveChanges();

            var repository = new CategoryRepository(context);

            // Act
            var result = repository.GetByName("Electronics");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Electronics", result!.Name);
        }

        [Fact]
        public void Add_ShouldInsertCategoryIntoDatabase()
        {
            // Arrange
            using var context = CreateDbContext();
            var repository = new CategoryRepository(context);

            var category = new Category
            {
                Name = "Office",
                Description = "Office supplies"
            };

            // Act
            repository.Add(category);

            // Assert
            var savedCategory = context.Categories.FirstOrDefault(c => c.Name == "Office");
            Assert.NotNull(savedCategory);
            Assert.Equal("Office supplies", savedCategory!.Description);
        }

        [Fact]
        public void Update_ShouldModifyExistingCategory()
        {
            // Arrange
            using var context = CreateDbContext();

            context.Categories.Add(new Category
            {
                Id = 1,
                Name = "Old Name",
                Description = "Old Description"
            });
            context.SaveChanges();

            var repository = new CategoryRepository(context);

            var updatedCategory = new Category
            {
                Id = 1,
                Name = "New Name",
                Description = "New Description"
            };

            // Act
            repository.Update(updatedCategory);

            // Assert
            var categoryFromDb = context.Categories.FirstOrDefault(c => c.Id == 1);
            Assert.NotNull(categoryFromDb);
            Assert.Equal("New Name", categoryFromDb!.Name);
            Assert.Equal("New Description", categoryFromDb.Description);
        }

        [Fact]
        public void Delete_ShouldRemoveCategory_WhenCategoryExists()
        {
            // Arrange
            using var context = CreateDbContext();

            context.Categories.Add(new Category
            {
                Id = 1,
                Name = "Electronics",
                Description = "Devices"
            });
            context.SaveChanges();

            var repository = new CategoryRepository(context);

            // Act
            repository.Delete(1);

            // Assert
            var deletedCategory = context.Categories.FirstOrDefault(c => c.Id == 1);
            Assert.Null(deletedCategory);
        }
    }
}