using AutoFixture;
using AutoMapper;
using fullstackBackEnd.Application.Test.Moq;
using fullstackBackend.Domain.Interfaces;
using Moq;
using fullstackBackend.Application.Common.Exceptions;
using Xunit;
using fullstackBackend.Application.Employee.Commands;
using fullstackBackend.Domain.Entities;
using System.Linq.Expressions;
using fullstackBackend.Application.Response;

namespace fullstackBackEnd.Application.Test.Employee.Commands
{
   public class UpdateEmployeeCommandHandlerTest
   {
      private readonly IMapper _mapper;
      private readonly Mock<IEmployeeRepository> _employeeRepository;
      private readonly Mock<IDepartmentRepository> _departmentRepository;
      private readonly Fixture _fixture;

      public UpdateEmployeeCommandHandlerTest()
      {
         _fixture = new Fixture();
         _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
         _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
         _mapper = MapperSetup.InitMappings();
         _employeeRepository = new Mock<IEmployeeRepository>();
         _departmentRepository = new Mock<IDepartmentRepository>();
      }

      [Fact]
      public async Task UpdateEmployeeCommandHandler_EmployeeNotExists()
      {
         //Arrange
         var request = _fixture.Create<UpdateEmployeeCommand>();

         _employeeRepository.Setup(u => u.GetAsync(It.IsAny<Expression<Func<EmployeeEntity, bool>>>())).ReturnsAsync((EmployeeEntity)null);

         //Act
         var handler = new UpdateEmployeeCommandHandler(
            _mapper,
            _employeeRepository.Object,
            _departmentRepository.Object
         );

         //Assert
         await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, CancellationToken.None));
      }

      [Fact]
      public async Task UpdateEmployeeCommandHandler_Ok()
      {
         //Arrange
         var employeeEntityMock = _fixture.Create<EmployeeEntity>();
         var request = _fixture.Create<UpdateEmployeeCommand>();

         _employeeRepository.Setup(u => u.GetEmployeeById(It.IsAny<int>())).ReturnsAsync(employeeEntityMock);
         _departmentRepository.Setup(u => u.Exists(It.IsAny<int>())).ReturnsAsync(true);

         //Act
         var handler = new UpdateEmployeeCommandHandler(
            _mapper,
            _employeeRepository.Object,
            _departmentRepository.Object
         );

         var response = await handler.Handle(request, CancellationToken.None);

         //Assert
         Assert.NotNull(response);
         Assert.IsType<GetEmployeeResponseModel>(response);
      }
   }
}
