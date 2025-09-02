using AutoMapper;
using fullstackBackend.Application.Common.Resolvers;
using fullstackBackend.Application.Employee.Commands;
using fullstackBackend.Application.Response;
using fullstackBackend.Domain.Entities;

namespace fullstackBackend.Application.Common.Mapping
{
   public class EmployeeMaps : Profile
   {
      public EmployeeMaps() {
         CreateMap<EmployeeEntity, GetEmployeeResponseModel>()
            .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.EmployeeLastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.EmployeeAddress, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.EmployeeHireDate, opt => opt.MapFrom<HireDateResolver>())
            .ForMember(dest => dest.EmployeePhone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.EmployeeDepartmentId, opt => opt.MapFrom(src => src.DepartmentId))
            .ForMember(dest => dest.EmployeeDepartment, opt => opt.MapFrom(src => src.Department));

         CreateMap<CreateEmployeeCommand, EmployeeEntity>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name))
            .ReverseMap();

         CreateMap<DepartmentEntity, GetDepartmentResponseModel>()
            .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Description));
      }
   }
}
