using fullstackBackend.Domain.Interfaces;
using fullstackBackend.WebApi.App.Services;
using Microsoft.AspNetCore.Mvc;

namespace fullstackBackend.WebApi.App.Controllers
{
   [ApiController]
   [Route("login")]
   public class AuthController : ControllerBase
   {
      private readonly TokenService _tokenService;
      private readonly IEmployeeRepository _employeeRepository;

      public AuthController(TokenService tokenService, IEmployeeRepository employeeRepository)
      {
         _tokenService = tokenService;
         _employeeRepository = employeeRepository;
      }

      [HttpGet("{id:int}")]
      public async Task<IActionResult> Login(int id)
      {
         // Validate user credentials (this is just an example, use a proper user validation)
         if (await _employeeRepository.Exists(id))
         {
            var token = _tokenService.GenerateToken("userId");
            return Ok(new { Token = token });
         }

         return Unauthorized();
      }
   }
}
