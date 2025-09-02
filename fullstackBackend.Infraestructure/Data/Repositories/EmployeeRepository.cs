using fullstackBackend.Domain.Entities;
using fullstackBackend.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace fullstackBackend.Infraestructure.Data.Repositories
{
   public class EmployeeRepository : RepositoryBase<EmployeeEntity>, IEmployeeRepository
   {
      public EmployeeRepository(EmployeeContext dbContext) : base(dbContext)
      {
      }

      public async Task<bool> EmployeeExists(EmployeeEntity employee)
      {
         return await DbSet.AnyAsync(e => e.Phone.Equals(employee.Phone) || (e.FirstName.Equals(employee.FirstName) && e.LastName.Equals(employee.LastName)));
      }

      public async Task<List<EmployeeEntity>> GetAllEmployees()
      {
         return await DbSet.Include(e => e.Department).ToListAsync();
      }

      public async Task<EmployeeEntity?> GetEmployeeById(int id)
      {
         return await DbSet.Include(e => e.Department).Where(e => e.Id == id).FirstOrDefaultAsync();
      }
   }
}
