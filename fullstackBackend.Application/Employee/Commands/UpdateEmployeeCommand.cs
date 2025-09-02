using fullstackBackend.Application.Response;
using MediatR;
using System.Text.Json.Serialization;

namespace fullstackBackend.Application.Employee.Commands
{
   public class UpdateEmployeeCommand : IRequest<GetEmployeeResponseModel>
   {
      [JsonIgnore]
      public int Id { get; set; }
      public int DepartmentId { get; set; }
   }
}
