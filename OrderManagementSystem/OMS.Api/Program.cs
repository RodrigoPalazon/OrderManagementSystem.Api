using Microsoft.EntityFrameworkCore;
using OMS.DataAccess.Context;
using OMS.DataAccess.Interfaces;
using OMS.DataAccess.Repositories;
using OMS.Services.Interfaces;
using OMS.Services.Services;

namespace OMS.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Swagger / OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // DbContext
            builder.Services.AddDbContext<OmsDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Repositories
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

            // Services
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();

            //Enabling CORS for the React app running on localhost:1234
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", policy =>
                {
                    policy
                        .WithOrigins("http://localhost:1234")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            var app = builder.Build();
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowReactApp");

            app.UseAuthorization(); // Where, how can I manage this Authorization?
            //Create a FE.

            app.MapControllers();

            app.Run();
        }
    }
}