using fullstackBackend.Application.Response;
using MediatR;

namespace fullstackBackend.Application.Employee.Commands
{
   public class CreateEmployeeCommand : IRequest<GetEmployeeResponseModel>
   {
      public string Name { get; set; } = string.Empty;
      public string LastName { get; set; } = string.Empty;
      public string Phone { get; set; } = string.Empty;
      public string Address { get; set; } = string.Empty;
      public DateTime HireDate { get; set; }
      public int DepartmentId { get; set; }
   }
}
