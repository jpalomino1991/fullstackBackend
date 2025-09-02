using fullstackBackend.Domain.Entities;
using fullstackBackend.Domain.Interfaces;

namespace fullstackBackend.Infraestructure.Data.Repositories
{
   public class DepartmentRepository : RepositoryBase<DepartmentEntity>, IDepartmentRepository
   {
      public DepartmentRepository(EmployeeContext dbContext) : base(dbContext)
      {
      }
   }
}
