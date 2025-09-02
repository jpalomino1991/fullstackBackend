using AutoMapper;
using fullstackBackend.Application.Common.Exceptions;
using fullstackBackend.Application.Common.Validators.Custom;
using fullstackBackend.Domain.Entities;
using fullstackBackend.Domain.Interfaces;
using MediatR;

namespace fullstackBackend.Application.Employee.Commands
{
   public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, int>
   {
      private readonly IMapper _mapper;
      private readonly IEmployeeRepository _employeeRepository;

      public DeleteEmployeeCommandHandler(IMapper mapper, IEmployeeRepository employeeRepository)
      {
         _mapper = mapper;
         _employeeRepository = employeeRepository;
      }

      public async Task<int> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
      {
         EmployeeEntity? employee = await _employeeRepository.Get(request.Id);

         var validator = new DeleteEmployeeValidator<EmployeeEntity>(employee);
         var validationResult = await validator.ValidateAsync(request.Id);

         if (!validationResult.IsValid) throw new NotFoundException(validationResult);

         await _employeeRepository.DeleteAsync(employee);
         await _employeeRepository.SaveChangesAsync();

         return 0;
      }
   }
}
