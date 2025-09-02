using AutoFixture;
using AutoMapper;
using fullstackBackend.Application.Common.Exceptions;
using fullstackBackend.Application.Employee.Queries;
using fullstackBackend.Application.Response;
using fullstackBackend.Domain.Entities;
using fullstackBackend.Domain.Interfaces;
using fullstackBackEnd.Application.Test.Moq;
using Moq;
using Xunit;

namespace fullstackBackEnd.Application.Test.Employee.Queries
{
   public class GetEmployeeByIdQueryHandlerTest
   {
      private readonly IMapper _mapper;
      private readonly Mock<IEmployeeRepository> _employeeRepository;
      private readonly Fixture _fixture;

      public GetEmployeeByIdQueryHandlerTest()
      {
         _fixture = new Fixture();
         _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
         _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
         _mapper = MapperSetup.InitMappings();
         _employeeRepository = new Mock<IEmployeeRepository>();
      }

      [Fact]
      public async Task GetEmployeeByIdQueryHandler_EmployeeNotExists()
      {
         //Arrange
         var id = 1;
         var request = new GetEmployeeByIdQuery(id);

         _employeeRepository.Setup(u => u.GetEmployeeById(It.IsAny<int>())).ReturnsAsync((EmployeeEntity)null);

         //Act
         var handler = new GetEmployeeByIdQueryHandler(
            _mapper,
            _employeeRepository.Object
         );

         //Assert
         await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, CancellationToken.None));
      }

      [Fact]
      public async Task GetEmployeeByIdQueryHandler_Ok()
      {
         //Arrange
         var employeeEntityMock = _fixture.Create<EmployeeEntity>();

         var request = new GetEmployeeByIdQuery(1);

         _employeeRepository.Setup(u => u.GetEmployeeById(It.IsAny<int>())).ReturnsAsync(employeeEntityMock);

         //Act
         var handler = new GetEmployeeByIdQueryHandler(
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
