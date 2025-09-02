using AutoMapper;
using fullstackBackend.Application.Common.Exceptions;
using fullstackBackend.Application.Common.Validators.Custom;
using fullstackBackend.Application.Response;
using fullstackBackend.Domain.Entities;
using fullstackBackend.Domain.Interfaces;
using MediatR;

namespace fullstackBackend.Application.Employee.Commands
{
   public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, GetEmployeeResponseModel>
   {
      private readonly IMapper _mapper;
      private readonly IEmployeeRepository _employeeRepository;

      public CreateEmployeeCommandHandler(IMapper mapper, IEmployeeRepository employeeRepository)
      {
         _mapper = mapper;
         _employeeRepository = employeeRepository;
      }

      public async Task<GetEmployeeResponseModel> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
      {
         EmployeeEntity employee = _mapper.Map<EmployeeEntity>(request);

         var validator = new CreateEmployeeValidator(_employeeRepository);
         var validationResult = await validator.ValidateAsync(employee);

         if (!validationResult.IsValid) throw new ValidationException(validationResult);

         await _employeeRepository.AddAsync(employee);
         await _employeeRepository.SaveChangesAsync();

         employee = await _employeeRepository.GetEmployeeById(employee.Id);

         return _mapper.Map<GetEmployeeResponseModel>(employee);
      }
   }
}
