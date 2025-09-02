using fullstackBackend.Domain.Interfaces;
using fullstackBackend.Infraestructure.Data;
using fullstackBackend.Infraestructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace fullstackBackend.WebApi.App.DependencyInjection
{
   public static class ServiceCollectionExtensions
   {
      public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
      {
         services.AddDbContext<EmployeeContext>(options => options
                  .EnableSensitiveDataLogging()
                  .UseMySQL(configuration.GetConnectionString("ConnectionString"), x => x.MigrationsAssembly(typeof(EmployeeContext).Assembly.FullName)));
      }

      public static void AddRepositories(this IServiceCollection services)
      {
         services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>))
            .AddScoped<IEmployeeRepository, EmployeeRepository>()
            .AddScoped<IDepartmentRepository, DepartmentRepository>();
      }
   }
}
