using Dnd_Api.DTO;
using Dnd_Api.Models;
using Dnd_Api.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly AppDbContext _db;
		private readonly JwtService _jwt;

		public AuthController(AppDbContext db, JwtService jwt)
		{
			_db = db;
			_jwt = jwt;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
		{
			if (await _db.AccountUsers.AnyAsync(u => u.Name == dto.Name))
				return BadRequest(new { message = "Name already exists" });

			if (string.IsNullOrEmpty(dto.Name))
				return BadRequest(new { message = "Name is required"});
			if (string.IsNullOrEmpty(dto.Password))
				return BadRequest(new { message = "Password is required" });
			if (dto.Password.Length < 6)
				return BadRequest(new { message = "Password is short" });

			var user = new AccountUser
			{
				Name = dto.Name,
				Pw = BCrypt.Net.BCrypt.HashPassword(dto.Password),
				RoleId = dto.RoleId
			};
			_db.AccountUsers.Add(user);
			await _db.SaveChangesAsync();

			return Ok(new { message = "User registered" });
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
		{
			if (string.IsNullOrEmpty(dto.Name))
				return BadRequest(new { message = "Name is required" });
			if (string.IsNullOrEmpty(dto.Password))
				return BadRequest(new { message = "Password is required" });

			var user = await _db.AccountUsers.Include(u => u.Role)
				.FirstOrDefaultAsync(u => u.Name == dto.Name);
			if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Pw))
				return Unauthorized();

			var role = user.Role.Name ?? "User";
			var token = _jwt.GenerateToken(user.Id, user.Name, role);

			return Ok(new { token });
		}
	}
}
