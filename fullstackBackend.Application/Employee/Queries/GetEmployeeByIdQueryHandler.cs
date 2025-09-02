using AutoMapper;
using fullstackBackend.Application.Common.Exceptions;
using fullstackBackend.Application.Common.Validators.Custom;
using fullstackBackend.Application.Response;
using fullstackBackend.Domain.Entities;
using fullstackBackend.Domain.Interfaces;
using MediatR;

namespace fullstackBackend.Application.Employee.Queries
{
   public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, GetEmployeeResponseModel>
   {
      private readonly IMapper _mapper;
      private readonly IEmployeeRepository _employeeRepository;

      public GetEmployeeByIdQueryHandler(IMapper mapper, IEmployeeRepository employeeRepository)
      {
         _mapper = mapper;
         _employeeRepository = employeeRepository;
      }

      public async Task<GetEmployeeResponseModel> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
      {
         EmployeeEntity? employee = await _employeeRepository.GetEmployeeById(request.Id);

         var validator = new GetEmployeeByIdValidator<EmployeeEntity>(employee);
         var validationResult = await validator.ValidateAsync(request.Id, cancellationToken);

         if(!validationResult.IsValid) throw new NotFoundException(validationResult);

         return _mapper.Map<GetEmployeeResponseModel>(employee);
      }
   }
}
