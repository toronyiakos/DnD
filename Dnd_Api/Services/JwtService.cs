using Dnd_Api.Models;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Dnd_Api.Services
{
	public interface IJwtService
	{
		string GenerateToken(AccountUser user);
		int GetUserIdFromToken(ClaimsPrincipal principal);
		string GetRoleFromToken(ClaimsPrincipal principal);
	}

	public class JwtService : IJwtService
	{
		private readonly IConfiguration _config;
		public JwtService(IConfiguration config)
		{
			_config = config;
		}

		public string GenerateToken(AccountUser user)
		{
			var jwtSettings = _config.GetSection("Jwt");
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiresInMinutes"]!));

			var roleName = user.Role?.Name ?? "user";

			/*
			var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);
			var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
			*/

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.Name, user.Name),
				new Claim(ClaimTypes.Role, roleName),
				new Claim("role_id", user.RoleId.ToString())
			};


			var token = new JwtSecurityToken(
				issuer: jwtSettings["Issuer"],
				audience: jwtSettings["Audience"],
				claims: claims,
				expires: expires,
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		public int GetUserIdFromToken(ClaimsPrincipal principal)
		{
			var sub = principal.FindFirstValue(JwtRegisteredClaimNames.Sub)
				?? principal.FindFirstValue(ClaimTypes.NameIdentifier);

			return int.TryParse(sub, out var id) ? id : 0;
		}

		public string GetRoleFromToken(ClaimsPrincipal principal)
			=> principal.FindFirstValue(ClaimTypes.Role) ?? "user";
	}
}
