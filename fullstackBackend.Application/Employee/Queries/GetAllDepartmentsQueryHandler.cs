using AutoMapper;
using fullstackBackend.Application.Response;
using fullstackBackend.Domain.Entities;
using fullstackBackend.Domain.Interfaces;
using MediatR;

namespace fullstackBackend.Application.Employee.Queries
{
   public class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, List<GetDepartmentResponseModel>>
   {
      private readonly IMapper _mapper;
      private readonly IDepartmentRepository _departmentRepository;

      public GetAllDepartmentsQueryHandler(IMapper mapper, IDepartmentRepository departmentRepository)
      {
         _mapper = mapper;
         _departmentRepository = departmentRepository;
      }

      public async Task<List<GetDepartmentResponseModel>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
      {
         List<DepartmentEntity> employees = await _departmentRepository.GetAllAsync();

         List<GetDepartmentResponseModel> listEmployees = new List<GetDepartmentResponseModel>();

         listEmployees = _mapper.Map<List<GetDepartmentResponseModel>>(employees);

         return listEmployees;
      }
   }
}
