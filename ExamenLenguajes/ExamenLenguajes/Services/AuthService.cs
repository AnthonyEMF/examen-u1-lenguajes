using ExamenLenguajes.Dtos.Auth;
using ExamenLenguajes.Dtos.Common;
using ExamenLenguajes.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExamenLenguajes.Services
{
	public class AuthService : IAuthService
	{
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly IConfiguration _configuration;

		public AuthService(
			SignInManager<IdentityUser> signInManager,
			UserManager<IdentityUser> userManager,
			IConfiguration configuration)
        {
			this._signInManager = signInManager;
			this._userManager = userManager;
			this._configuration = configuration;
		}

        public Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginDto dto)
		{
			throw new NotImplementedException();
		}

		private JwtSecurityToken GetToken(List<Claim> authClaims)
		{
			var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8
				.GetBytes(_configuration["JWT:Secret"]));

			return new JwtSecurityToken(
				issuer: _configuration["JWT:ValidIssuer"],
				audience: _configuration["JWT:ValidAudience"],
				expires: DateTime.Now.AddHours(1),
				claims: authClaims,
				signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
			);
		}
	}
}
