using AutoFixture;
using AutoMapper;
using fullstackBackEnd.Application.Test.Moq;
using fullstackBackend.Domain.Interfaces;
using Moq;
using fullstackBackend.Application.Common.Exceptions;
using fullstackBackend.Application.Employee.Queries;
using fullstackBackend.Domain.Entities;
using Xunit;
using fullstackBackend.Application.Response;

namespace fullstackBackEnd.Application.Test.Employee.Queries
{
   public class GetAllEmployeesQueryHandlerTest
   {
      private readonly IMapper _mapper;
      private readonly Mock<IEmployeeRepository> _employeeRepository;
      private readonly Fixture _fixture;

      public GetAllEmployeesQueryHandlerTest()
      {
         _fixture = new Fixture();
         _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
         _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
         _mapper = MapperSetup.InitMappings();
         _employeeRepository = new Mock<IEmployeeRepository>();
      }

      [Fact]
      public async Task GetAllEmployeesQueryHandler_ReturnEmpty()
      {
         //Arrange
         var request = new GetAllEmployeesQuery();
         List<EmployeeEntity> employees = new List<EmployeeEntity>();

         _employeeRepository.Setup(u => u.GetAllEmployees()).ReturnsAsync(employees);

         //Act
         var handler = new GetAllEmployeesQueryHandler(
            _mapper,
            _employeeRepository.Object
         );

         var response = await handler.Handle(request, CancellationToken.None);

         //Assert
         Assert.NotNull(response);
         Assert.IsType<List<GetEmployeeResponseModel>>(response);
      }

      [Fact]
      public async Task GetAllEmployeesQueryHandler_ReturnListEmployees()
      {
         //Arrange
         var employeeEntityListMock = _fixture.Create<List<EmployeeEntity>>();
         var request = new GetAllEmployeesQuery();

         _employeeRepository.Setup(u => u.GetAllEmployees()).ReturnsAsync(employeeEntityListMock);

         //Act
         var handler = new GetAllEmployeesQueryHandler(
            _mapper,
            _employeeRepository.Object
         );

         var response = await handler.Handle(request, CancellationToken.None);

         //Assert
         Assert.NotNull(response);
         Assert.IsType<List<GetEmployeeResponseModel>>(response);
      }
   }
}
