using fullstackBackend.Domain.Entities;

namespace fullstackBackend.Infraestructure.Data.Seed
{
   public class DepartmentDataSeed
   {
      public static List<DepartmentEntity> GetDepartments()
      {
         return new List<DepartmentEntity> {
            new DepartmentEntity() { Id = 1, Description = "HR" },
            new DepartmentEntity() { Id = 2, Description = "IT" },
            new DepartmentEntity() { Id = 3, Description = "Other" }
         };
      }
   }
}
