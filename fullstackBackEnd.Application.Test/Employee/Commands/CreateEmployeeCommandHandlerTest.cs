using AutoFixture;
using AutoMapper;
using fullstackBackend.Application.Common.Exceptions;
using fullstackBackend.Application.Employee.Commands;
using fullstackBackend.Application.Response;
using fullstackBackend.Domain.Entities;
using fullstackBackend.Domain.Interfaces;
using fullstackBackEnd.Application.Test.Moq;
using Moq;
using Xunit;

namespace fullstackBackEnd.Application.Test.Employee.Commands
{
   public class CreateEmployeeCommandHandlerTest
   {
      private readonly IMapper _mapper;
      private readonly Mock<IEmployeeRepository> _employeeRepository;
      private readonly Fixture _fixture;

      public CreateEmployeeCommandHandlerTest()
      {
         _fixture = new Fixture();
         _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
         _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
         _mapper = MapperSetup.InitMappings();
         _employeeRepository = new Mock<IEmployeeRepository>();
      }

      [Fact]
      public async Task CreateEmployeeCommandHandler_EmployeeExists()
      {
         //Arrange
         var request = _fixture.Create<CreateEmployeeCommand>();

         _employeeRepository.Setup(u => u.EmployeeExists(It.IsAny<EmployeeEntity>())).ReturnsAsync(true);

         //Act
         var handler = new CreateEmployeeCommandHandler(
            _mapper,
            _employeeRepository.Object
         );

         //Assert
         await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(request, CancellationToken.None));
      }

      [Fact]
      public async Task CreateEmployeeCommandHandler_Created()
      {
         //Arrange
         var employeeEntityMock = _fixture.Create<EmployeeEntity>();

         var request = _fixture.Create<CreateEmployeeCommand>();
         request.Phone = "987654321";

         _employeeRepository.Setup(u => u.EmployeeExists(It.IsAny<EmployeeEntity>())).ReturnsAsync(false);
         _employeeRepository.Setup(u => u.AddAsync(employeeEntityMock)).ReturnsAsync(employeeEntityMock);

         //Act
         var handler = new CreateEmployeeCommandHandler(
            _mapper,
            _employeeRepository.Object
         );

         var response = await handler.Handle(request, CancellationToken.None);

         //Assert
         Assert.NotNull(response);
         Assert.IsType<GetEmployeeResponseModel>(response);
      }
   }
}
