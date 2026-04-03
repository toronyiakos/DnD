using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Dnd_Api.Security
{
	public class JwtService
	{
		private readonly IConfiguration _config;
		public JwtService(IConfiguration config)
		{
			_config = config;
		}

		public string GenerateToken(int userId, string name, string role)
		{
			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
				new Claim(JwtRegisteredClaimNames.Email, name),
				new Claim(ClaimTypes.Role,role)
			};

			var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);
			var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
			issuer: _config["Jwt:Issuer"],
			audience: _config["Jwt:Audience"],
			claims: claims,
			expires: DateTime.UtcNow.AddHours(10),
			signingCredentials: creds
		);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
