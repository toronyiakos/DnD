using Dnd_Api.DTO;
using Dnd_Api.Models;
using Dnd_Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Principal;

namespace Dnd_Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _auth;
		private readonly AppDbContext _db;

		public AuthController(IAuthService auth, AppDbContext db)
		{
			_auth = auth;
			_db = db;
		}

		[HttpGet("authcheck")]
		public IActionResult AuthCheck()
		{
			var principal = User;
			foreach (var claim in principal.Claims)
			{
				Console.WriteLine(claim.Type + " : " + claim.Value);
			}
			return Ok(new
			{
				Authenticated = User.Identity?.IsAuthenticated,
				Claims = User.Claims.Select(x => new { x.Type, x.Value })
			});
		}



		[HttpPost("register")]
		[ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		public async Task<IActionResult> Register([FromBody] RegisterDto dto)
		{
			if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Password))
				return BadRequest("Name and Password are required.");

			var result = await _auth.RegisterAsync(dto);

			if (result is null)
				return Conflict("Username already taken.");

			return CreatedAtAction(nameof(Register), result);
		}

		[HttpPost("login")]
		[ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> Login([FromBody] LoginDto dto)
		{
			if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Password))
				return BadRequest("Name and Password are required.");

			var result = await _auth.LoginAsync(dto);

			if (result is null)
				return Unauthorized("Invalid credentials.");

			return Ok(result);
		}
	}
}
