using fullstackBackend.Application.Response;
using MediatR;

namespace fullstackBackend.Application.Employee.Queries
{
   public class GetAllEmployeesQuery : IRequest<List<GetEmployeeResponseModel>>
   {
      public GetAllEmployeesQuery() { }
   }
}
