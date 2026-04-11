using Dnd_Api.DTO;
using Dnd_Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Dnd_Api.Services
{
	public interface IAuthService
	{
		Task<AuthResponseDto?> RegisterAsync(RegisterDto dto);
		Task<AuthResponseDto?> LoginAsync(LoginDto dto);
	}

	public class AuthService : IAuthService
	{
		private readonly AppDbContext _db;
		private readonly IJwtService _jwt;
		private readonly IConfiguration _config;

		public AuthService(AppDbContext db, IJwtService jwt, IConfiguration config)
		{
			_db = db;
			_jwt = jwt;
			_config = config;
		}

		public async Task<AuthResponseDto?> RegisterAsync(RegisterDto dto)
		{
			if (await _db.AccountUsers.AnyAsync(u => u.Name == dto.Name))
				return null;

			var user = new AccountUser
			{
				Name = dto.Name,
				Pw = BCrypt.Net.BCrypt.HashPassword(dto.Password),
				RoleId = 0
			};

			_db.AccountUsers.Add(user);
			await _db.SaveChangesAsync();

			await _db.Entry(user).Reference(u => u.Role).LoadAsync();

			var token = _jwt.GenerateToken(user);
			var expires = DateTime.UtcNow.AddMinutes(
				double.Parse(_config["Jwt:ExpiresInMinutes"]!));

			return new AuthResponseDto(token, user.Name, user.Role.Name, expires);
		}

		public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
		{
			var user = await _db.AccountUsers
				.Include(u => u.Role)
				.FirstOrDefaultAsync(u => u.Name == dto.Name);

			if (user is null)
				return null;

			Debug.WriteLine($"Role Type: {user.Role.Name}");

			if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Pw))
				return null;

			var token = _jwt.GenerateToken(user);
			var expires = DateTime.UtcNow.AddMinutes(
				double.Parse(_config["Jwt:ExpiresInMinutes"]!));

			return new AuthResponseDto(token, user.Name, user.Role.Name, expires);
		}
	}
}
