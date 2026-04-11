using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Dnd_Api.Helpers
{
	public static class ClaimsPrincipalExtensions
	{
		public static int GetUserId(this ClaimsPrincipal principal)
		{
			var value = principal.FindFirstValue(JwtRegisteredClaimNames.Sub)
				?? principal.FindFirstValue(ClaimTypes.NameIdentifier);
			return int.TryParse(value, out var id) ? id : 0;
		}

		public static string GetRole(this ClaimsPrincipal principal)
			=> principal.FindFirstValue(ClaimTypes.Role) ?? "user";

		public static string GetUsername(this ClaimsPrincipal principal)
			=> principal.FindFirstValue(JwtRegisteredClaimNames.Name)
			?? principal.FindFirstValue(ClaimTypes.Name)
			?? string.Empty;

		public static bool IsAdminOrGameMaster(this ClaimsPrincipal principal)
		{
			var role = principal.GetRole();
			return role is "admin" or "game_master";
		}

		public static bool IsAdmin(this ClaimsPrincipal principal)
			=> principal.GetRole() is "admin";
	}
}
