using ExamenLenguajes.Dtos.Auth;
using ExamenLenguajes.Dtos.Common;
using ExamenLenguajes.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamenLenguajes.Controllers
{
	[Route("api/auth")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
        {
			this._authService = authService;
		}

		[HttpPost("login")]
		[AllowAnonymous]
		public async Task<ActionResult<ResponseDto<LoginResponseDto>>> Login(LoginDto dto)
		{
			var response = await _authService.LoginAsync(dto);
			return StatusCode(response.StatusCode, response);
		}
	}
}
