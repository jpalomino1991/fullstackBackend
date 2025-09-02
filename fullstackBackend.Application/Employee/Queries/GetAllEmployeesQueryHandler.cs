using AutoMapper;
using fullstackBackend.Application.Response;
using fullstackBackend.Domain.Entities;
using fullstackBackend.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fullstackBackend.Application.Employee.Queries
{
   public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, List<GetEmployeeResponseModel>>
   {
      private readonly IMapper _mapper;
      private readonly IEmployeeRepository _employeeRepository;

      public GetAllEmployeesQueryHandler(IMapper mapper, IEmployeeRepository employeeRepository)
      {
         _mapper = mapper;
         _employeeRepository = employeeRepository;
      }

      public async Task<List<GetEmployeeResponseModel>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
      {
         List<EmployeeEntity> employees = await _employeeRepository.GetAllEmployees();

         List<GetEmployeeResponseModel> listEmployees = new List<GetEmployeeResponseModel>();

         listEmployees = _mapper.Map<List<GetEmployeeResponseModel>>(employees);

         return listEmployees;
      }
   }
}
