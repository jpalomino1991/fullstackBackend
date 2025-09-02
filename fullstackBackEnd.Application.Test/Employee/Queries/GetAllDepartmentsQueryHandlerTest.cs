using AutoFixture;
using AutoMapper;
using fullstackBackEnd.Application.Test.Moq;
using fullstackBackend.Domain.Interfaces;
using Moq;
using fullstackBackend.Application.Employee.Queries;
using fullstackBackend.Application.Response;
using fullstackBackend.Domain.Entities;
using Xunit;

namespace fullstackBackEnd.Application.Test.Employee.Queries
{
   public class GetAllDepartmentsQueryHandlerTest
   {
      private readonly IMapper _mapper;
      private readonly Mock<IDepartmentRepository> _departmentRepository;
      private readonly Fixture _fixture;

      public GetAllDepartmentsQueryHandlerTest()
      {
         _fixture = new Fixture();
         _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
         _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
         _mapper = MapperSetup.InitMappings();
         _departmentRepository = new Mock<IDepartmentRepository>();
      }

      [Fact]
      public async Task GetAllDepartmentsQueryHandler_ReturnEmpty()
      {
         //Arrange
         var request = new GetAllDepartmentsQuery();
         List<DepartmentEntity> departments = new List<DepartmentEntity>();

         _departmentRepository.Setup(u => u.GetAllAsync()).ReturnsAsync(departments);

         //Act
         var handler = new GetAllDepartmentsQueryHandler(
            _mapper,
            _departmentRepository.Object
         );

         var response = await handler.Handle(request, CancellationToken.None);

         //Assert
         Assert.NotNull(response);
         Assert.IsType<List<GetDepartmentResponseModel>>(response);
      }

      [Fact]
      public async Task GetAllDepartmentsQueryHandler_ReturnListDepartments()
      {
         //Arrange
         var request = new GetAllDepartmentsQuery();
         var departmentEntityListMock = _fixture.Create<List<DepartmentEntity>>();

         _departmentRepository.Setup(u => u.GetAllAsync()).ReturnsAsync(departmentEntityListMock);

         //Act
         var handler = new GetAllDepartmentsQueryHandler(
            _mapper,
            _departmentRepository.Object
         );

         var response = await handler.Handle(request, CancellationToken.None);

         //Assert
         Assert.NotNull(response);
         Assert.IsType<List<GetDepartmentResponseModel>>(response);
      }
   }
}
