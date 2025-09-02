using fullstackBackend.Domain.Entities;

namespace fullstackBackend.Domain.Interfaces
{
   public interface IEmployeeRepository : IAsyncRepository<EmployeeEntity>
   {
      Task<EmployeeEntity?> GetEmployeeById(int id);
      Task<List<EmployeeEntity>> GetAllEmployees();
      Task<bool> EmployeeExists(EmployeeEntity employee);
   }
}
