using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using OMS.DataAccess.Context;

namespace OMS.DataAccess
{
    public class OmsDbContextFactory : IDesignTimeDbContextFactory<OmsDbContext>
    {
        public OmsDbContext CreateDbContext(string[] args)
        {
            var consoleAppPath = Path.GetFullPath(
                Path.Combine(Directory.GetCurrentDirectory(), "..", "OrderManagementSystem.ConsoleApp"));

            if (!Directory.Exists(consoleAppPath))
            {
                throw new DirectoryNotFoundException(
                    $"Could not find OrderManagementSystem.ConsoleApp folder at: {consoleAppPath}");
            }

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(consoleAppPath)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("DefaultConnection was not found in appsettings.json.");

            var optionsBuilder = new DbContextOptionsBuilder<OmsDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new OmsDbContext(optionsBuilder.Options);
        }
    }
}