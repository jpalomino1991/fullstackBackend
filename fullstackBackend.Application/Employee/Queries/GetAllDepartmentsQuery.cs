using fullstackBackend.Application.Response;
using MediatR;

namespace fullstackBackend.Application.Employee.Queries
{
   public class GetAllDepartmentsQuery : IRequest<List<GetDepartmentResponseModel>>
   {
      public GetAllDepartmentsQuery() { }
   }
}
