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

			var user = new AccountUser
			{
				Name = dto.Name,
				Pw = BCrypt.Net.BCrypt.HashPassword(dto.Password),
				RoleId = 0
			};
			_db.AccountUsers.Add(user);
			await _db.SaveChangesAsync();

			return Ok(new { message = "User registered" });
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] UserRegisterDto dto)
		{
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
