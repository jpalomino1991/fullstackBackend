using MediatR;

namespace fullstackBackend.Application.Employee.Commands
{
   public class DeleteEmployeeCommand : IRequest<int>
   {
      public int Id { get; set; }
   }
}
