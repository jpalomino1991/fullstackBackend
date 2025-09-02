using fullstackBackend.Application.Employee.Commands;
using fullstackBackend.Application.Employee.Queries;
using fullstackBackend.Application.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fullstackBackend.WebApi.App.Controllers
{
   [ApiController]
   [Route("employee")]
   public class EmployeeController : Controller
   {
      private readonly IMediator _mediator;
      public EmployeeController(IMediator mediator) {
         _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
      }

      [HttpGet("{id:int}")]
      public async Task<ActionResult<GetEmployeeResponseModel>> GetEmployeeById(int id)
      {
         return await _mediator.Send(new GetEmployeeByIdQuery(id));
      }

      [HttpGet]
      public async Task<ActionResult<List<GetEmployeeResponseModel>>> GetAllEmployees()
      {
         return await _mediator.Send(new GetAllEmployeesQuery());
      }

      [HttpPost]
      public async Task<ActionResult<GetEmployeeResponseModel>> CreateEmployee(CreateEmployeeCommand command)
      {
         var result = await _mediator.Send(command);
         return StatusCode(201, result);
      }

      [HttpPut("{id}")]
      public async Task<ActionResult<GetEmployeeResponseModel>> UpdateEmployee(int id, UpdateEmployeeCommand command)
      {
         command.Id = id;
         return await _mediator.Send(command);
      }

      [HttpDelete("{id}")]
      public async Task<ActionResult<int>> DeleteEmployee(int id)
      {
         await _mediator.Send(new DeleteEmployeeCommand { Id = id });
         return Ok();
      }
   }
}
