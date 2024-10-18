using ExamenLenguajes.Services.Interfaces;

namespace ExamenLenguajes.Services
{
	public class AuditService : IAuditService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public AuditService(IHttpContextAccessor httpContextAccessor)
        {
			this._httpContextAccessor = httpContextAccessor;
		}

        public string GetUserId()
		{
			//var httpContext = _httpContextAccessor.HttpContext;
			//if (httpContext == null)
			//{
			//	return "53e2c7b8-3f58-4411-a83c-7c368792ade2";
			//}

			var idClaim = _httpContextAccessor.HttpContext
				.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault();

			return idClaim.Value;
		}
	}
}
