using AutoMapper;
using fullstackBackend.Application.Common.Exceptions;
using fullstackBackend.Application.Common.Validators.Custom;
using fullstackBackend.Application.Response;
using fullstackBackend.Domain.Entities;
using fullstackBackend.Domain.Interfaces;
using MediatR;

namespace fullstackBackend.Application.Employee.Commands
{
   public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, GetEmployeeResponseModel>
   {
      private readonly IMapper _mapper;
      private readonly IEmployeeRepository _employeeRepository;
      private readonly IDepartmentRepository _departmentRepository;

      public UpdateEmployeeCommandHandler(IMapper mapper, IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
      {
         _mapper = mapper;
         _employeeRepository = employeeRepository;
         _departmentRepository = departmentRepository;
      }

      public async Task<GetEmployeeResponseModel> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
      {
         EmployeeEntity? employee = await _employeeRepository.GetEmployeeById(request.Id);

         var validator = new UpdateEmployeeValidator<EmployeeEntity>(employee, _departmentRepository);
         var validationResult = await validator.ValidateAsync(request, cancellationToken);

         if (validationResult.IsValid == false) throw new NotFoundException(validationResult);

         employee.DepartmentId = request.DepartmentId;

         await _employeeRepository.UpdateAsync(employee);
         await _employeeRepository.SaveChangesAsync();

         return _mapper.Map<GetEmployeeResponseModel>(employee);
      }
   }
}
