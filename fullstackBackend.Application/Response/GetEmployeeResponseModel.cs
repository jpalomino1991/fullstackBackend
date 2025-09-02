using fullstackBackend.Domain.Entities;

namespace fullstackBackend.Application.Response
{
   public class GetEmployeeResponseModel
   {
      public int EmployeeId { get; set; }
      public string EmployeeName { get; set; } = string.Empty;
      public string EmployeeLastName { get; set; } = string.Empty;
      public string EmployeePhone { get; set; } = string.Empty;
      public string EmployeeAddress{ get; set; } = string.Empty;
      public string EmployeeHireDate { get; set; } = string.Empty;
      public int EmployeeDepartmentId { get; set; }
      public DepartmentEntity EmployeeDepartment { get; set; } = new DepartmentEntity();
   }
}
