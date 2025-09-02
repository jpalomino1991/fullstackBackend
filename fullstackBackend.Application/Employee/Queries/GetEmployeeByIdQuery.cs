using fullstackBackend.Application.Response;
using MediatR;

namespace fullstackBackend.Application.Employee.Queries
{
   public class GetEmployeeByIdQuery : IRequest<GetEmployeeResponseModel>
   {
      public int Id { get; set; }

      public GetEmployeeByIdQuery(int id)
      {
         Id = id;
      }
   }
}
