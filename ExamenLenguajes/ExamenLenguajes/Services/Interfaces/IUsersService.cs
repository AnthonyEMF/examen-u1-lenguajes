using ExamenLenguajes.Dtos.Common;
using ExamenLenguajes.Dtos.Users;

namespace ExamenLenguajes.Services.Interfaces
{
	public interface IUsersService
	{
		Task<ResponseDto<PaginationDto<List<UserDto>>>> GetAllUsersAsync(string searchTerm = "", int page = 1);
		Task<ResponseDto<UserDto>> GetUserByIdAsync(Guid id);
		Task<ResponseDto<UserDto>> CreateAsync(UserCreateDto dto);
		Task<ResponseDto<UserDto>> EditAsync(UserEditDto dto, Guid id);
		Task<ResponseDto<UserDto>> DeleteAsync(Guid id);
	}
}
