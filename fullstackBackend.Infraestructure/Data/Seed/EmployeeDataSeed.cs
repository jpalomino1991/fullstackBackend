using fullstackBackend.Domain.Entities;

namespace fullstackBackend.Infraestructure.Data.Seed
{
   public class EmployeeDataSeed
   {
      public static List<EmployeeEntity> GetEmployees()
      {
         return new List<EmployeeEntity>() {
            new EmployeeEntity()
            {
               Id = 1, FirstName = "Juan", LastName = "Perez", Address = "No Address",
               Phone = "123456789", HireDate = DateTime.Now.AddDays(-30) , DepartmentId = 1
            },
            new EmployeeEntity()
            {
               Id = 2, FirstName = "John", LastName = "Doe", Address = "No Address",
               Phone = "123456789", HireDate = DateTime.Now.AddDays(-60) , DepartmentId = 2
            },
            new EmployeeEntity()
            {
               Id = 3, FirstName = "Mark", LastName = "Dwain", Address = "No Address",
               Phone = "123456789", HireDate = DateTime.Now.AddDays(-90) , DepartmentId = 3
            }
         };
      }
   }
}
