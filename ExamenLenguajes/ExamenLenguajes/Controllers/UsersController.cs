using ExamenLenguajes.Constants;
using ExamenLenguajes.Dtos.Common;
using ExamenLenguajes.Dtos.Users;
using ExamenLenguajes.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamenLenguajes.Controllers
{
	[Route("api/users")]
	[ApiController]
	[Authorize(AuthenticationSchemes = "Bearer")]
	public class UsersController : ControllerBase
	{
		private readonly IUsersService _usersService;

		public UsersController(IUsersService usersService)
        {
			this._usersService = usersService;
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<ActionResult<ResponseDto<List<UserDto>>>> GetAll(string searchTerm = "", int page = 1)
		{
			var response = await _usersService.GetAllUsersAsync(searchTerm, page);
			return StatusCode(response.StatusCode, response);
		}

		[HttpGet("{id}")]
		[AllowAnonymous]
		public async Task<ActionResult<ResponseDto<UserDto>>> Get(string id)
		{
			var response = await _usersService.GetUserByIdAsync(id);
			return StatusCode(response.StatusCode, response);
		}

		[HttpPost]
		[Authorize(Roles = $"{RolesConstant.ADMIN}, {RolesConstant.HUMAN_RESOURCES}")]
		public async Task<ActionResult<ResponseDto<UserDto>>> Create(UserCreateDto dto)
		{
			var response = await _usersService.CreateAsync(dto);
			return StatusCode(response.StatusCode, response);
		}

		[HttpPut("{id}")]
		[Authorize(Roles = $"{RolesConstant.ADMIN}, {RolesConstant.HUMAN_RESOURCES}")]
		public async Task<ActionResult<ResponseDto<UserDto>>> Edit(UserEditDto dto, string id)
		{
			var response = await _usersService.EditAsync(dto, id);
			return StatusCode(response.StatusCode, response);
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = $"{RolesConstant.ADMIN}, {RolesConstant.HUMAN_RESOURCES}")]
		public async Task<ActionResult<ResponseDto<UserDto>>> Delete(string id)
		{
			var response = await _usersService.DeleteAsync(id);
			return StatusCode(response.StatusCode, response);
		}
	}
}
