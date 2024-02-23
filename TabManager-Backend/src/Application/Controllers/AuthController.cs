using Core.DTO;
using Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{

   public class AuthController : ControllerBaseAPI
   {
      private readonly IAuthenticationService _authenticationService;
      public AuthController(IAuthenticationService authenticationService)
      {
         _authenticationService = authenticationService;
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