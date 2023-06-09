﻿using Microsoft.AspNetCore.Mvc;
using Models.DTOs.Input;
using Services;

namespace Ferreira_Challenge.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequest)
        {
            var result = await _authService.LoginAsync(loginRequest.Login, loginRequest.Password);

            if (result.Status)
            {
                return Ok(result.Token);
            }

            return Unauthorized();
        }

        [HttpPost("Password/Recover")]
        public async Task<IActionResult> RecoverPassword([FromBody] UserRecoveryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string newPassword = await _authService.RecoverPassword(request.Login, request.Email, request.DateOfBirth);

            return Ok(new { newPassword });
        }
    }
}
