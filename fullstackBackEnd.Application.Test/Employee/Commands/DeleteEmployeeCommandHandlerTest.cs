using AutoFixture;
using AutoMapper;
using fullstackBackEnd.Application.Test.Moq;
using fullstackBackend.Domain.Interfaces;
using Moq;
using fullstackBackend.Application.Employee.Commands;
using fullstackBackend.Domain.Entities;
using Xunit;
using fullstackBackend.Application.Common.Exceptions;

namespace fullstackBackEnd.Application.Test.Employee.Commands
{
   public class DeleteEmployeeCommandHandlerTest
   {
      private readonly IMapper _mapper;
      private readonly Mock<IEmployeeRepository> _employeeRepository;
      private readonly Fixture _fixture;

      public DeleteEmployeeCommandHandlerTest()
      {
         _fixture = new Fixture();
         _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
         _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
         _mapper = MapperSetup.InitMappings();
         _employeeRepository = new Mock<IEmployeeRepository>();
      }

      [Fact]
      public async Task DeleteEmployeeCommandHandler_EmployeeNotExists()
      {
         //Arrange
         var request = _fixture.Create<DeleteEmployeeCommand>();

         _employeeRepository.Setup(u => u.EmployeeExists(It.IsAny<EmployeeEntity>())).ReturnsAsync(false);

         //Act
         var handler = new DeleteEmployeeCommandHandler(
            _mapper,
            _employeeRepository.Object
         );

         //Assert
         await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, CancellationToken.None));
      }

      [Fact]
      public async Task DeleteEmployeeCommandHandler_Ok()
      {
         //Arrange
         var employeeEntityMock = _fixture.Create<EmployeeEntity>();
         var request = _fixture.Create<DeleteEmployeeCommand>();

         _employeeRepository.Setup(u => u.Get(It.IsAny<int>())).ReturnsAsync(employeeEntityMock);
         _employeeRepository.Setup(u => u.DeleteAsync(It.IsAny<EmployeeEntity>())).ReturnsAsync(true);

         //Act
         var handler = new DeleteEmployeeCommandHandler(
            _mapper,
            _employeeRepository.Object
         );

         var response = await handler.Handle(request, CancellationToken.None);

         //Assert
         Assert.IsType<int>(response);
      }
   }
}
