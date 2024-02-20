using Application.Filters;
using Core.DTO;
using Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
   [Route("api/[controller]")]
   [TypeFilter(typeof(ValidationActionFilter))]
   public class AuthController : ControllerBase
   {
      private readonly IAuthenticationService _authenticationService;
      private readonly IJwtService _jwtService;
      public AuthController(IAuthenticationService authenticationService, IJwtService jwtService)
      {
         _authenticationService = authenticationService;
         _jwtService = jwtService;
      }

      [HttpPost("register")]
      public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
      {
         await _authenticationService.CreateAccountAsync(registerDTO);
         return NoContent();
      }

      [HttpPost("login")]
      public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
      {
         var token = await _authenticationService.SignInAsync(loginDTO);
         return Ok(token);
      }
   }
}