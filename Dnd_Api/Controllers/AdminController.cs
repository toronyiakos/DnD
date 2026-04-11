using Dnd_Api.DTO;
using Dnd_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize(Policy = "AdminOnly")]
	public class AdminController : ControllerBase
	{
		private readonly AppDbContext _db;

		public AdminController(AppDbContext db)
		{
			_db = db;
		}

		[HttpGet("user")]
		public async Task<IActionResult> GetUsers()
		{
			var users = await _db.AccountUsers
				.Include(u => u.Role)
				.Select(u => new UserDto(u.Id, u.Name, u.RoleId, u.Role.Name))
				.ToListAsync();

			return Ok(users);
		}

		[HttpGet("users/{id:int}")]
		public async Task<IActionResult> GetUser(int id)
		{
			var user = await _db.AccountUsers
				.Include(u => u.Role)
				.FirstOrDefaultAsync(u => u.Id == id);

			if (user is null) return NotFound();

			return Ok(new UserDto(user.Id, user.Name, user.RoleId, user.Role.Name));
		}

		[HttpPatch("users/{id:int}/role")]
		public async Task<IActionResult> UpdateRole(int id, [FromBody] UpdateUserRoleDto dto)
		{
			if(dto.RoleId is < 0 or > 2)
				return BadRequest("RoleId must be 0 (user), 1 (game_master), or 2 (admin).");

			var user = await _db.AccountUsers.FindAsync(id);
			if (user is null) return NotFound();

			user.RoleId = dto.RoleId;
			await _db.SaveChangesAsync();

			return NoContent();
		}

		[HttpDelete("users/{id:int}")]
		public async Task<IActionResult> DeleteUser(int id)
		{
			var user = await _db.AccountUsers.FindAsync(id);
			if (user is null) return NotFound();

			_db.AccountUsers.Remove(user);
			await _db.SaveChangesAsync();

			return NoContent();
		}
	}
}
