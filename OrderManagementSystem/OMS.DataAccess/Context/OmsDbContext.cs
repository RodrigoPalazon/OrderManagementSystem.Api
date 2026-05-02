using Microsoft.EntityFrameworkCore;
using OMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.DataAccess.Context
{
    public class OmsDbContext : DbContext
    {
        public OmsDbContext(DbContextOptions<OmsDbContext> options) : base(options) // "This constructor is used to configure the DbContext with options, such as the connection string."
                                                                                    // What would be another example of options that could be passed in here?
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) // "This method is used to configure the entity mappings and relationships. It is called when the model is being created."
        {
            base.OnModelCreating(modelBuilder); //“Run the original OnModelCreating logic from the parent DbContext too.”

            modelBuilder.Entity<Category>().HasKey(c => c.Id);
            modelBuilder.Entity<Customer>().HasKey(c => c.Id);
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
           
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasOne<Category>()
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // 'Restrict' prevents deleting a category if products still reference it

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(10, 2);
        }
    }
}