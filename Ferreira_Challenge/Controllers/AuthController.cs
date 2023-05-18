using Microsoft.AspNetCore.Mvc;
using Models.DTOs.Input;
using Services;

namespace Ferreira_Challenge.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequest)
        {
            var result = await _authService.LoginAsync(loginRequest.Login, loginRequest.Password);

            if (result.Status)
            {
                return Ok(result.Token);
            }

            return Unauthorized();
        }
    }
}
