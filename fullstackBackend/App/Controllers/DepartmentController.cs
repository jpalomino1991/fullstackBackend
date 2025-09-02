using fullstackBackend.Application.Employee.Queries;
using fullstackBackend.Application.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fullstackBackend.WebApi.App.Controllers
{
   [Authorize]
   [ApiController]
   [Route("department")]
   public class DepartmentController : Controller
   {
      private readonly IMediator _mediator;

      public DepartmentController(IMediator mediator)
      {
         _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
      }

      [HttpGet]
      public async Task<ActionResult<List<GetDepartmentResponseModel>>> GetAllDepartments()
      {
         return await _mediator.Send(new GetAllDepartmentsQuery());
      }
   }
}
