using ExamenLenguajes.Dtos.Auth;
using ExamenLenguajes.Dtos.Common;

namespace ExamenLenguajes.Services.Interfaces
{
	public interface IAuthService
	{
		Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginDto dto);
	}
}
