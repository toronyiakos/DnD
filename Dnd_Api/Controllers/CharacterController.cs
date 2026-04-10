using Dnd_Api.DTO;
using Dnd_Api.Models;
using Dnd_Api.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Dnd_Api.Controllers
{
	[ApiController]
	[Authorize]
	[Route("api/[controller]")]

	public class CharacterController : ControllerBase
	{
		private readonly AppDbContext _db;

		public CharacterController(AppDbContext db)
		{
			_db = db;
		}

		[HttpPost]
		public async Task<IActionResult> CreateCharacter([FromBody] CreateCharacterDto dto)
		{
			if (!await _db.Dnd5Classes.AnyAsync(x => x.ClassId == dto.ClassId))
				return BadRequest("Invalid class");

			var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

			var character = new Dnd5Character
			{
				Name = dto.Name,
				OwnerId = userId,
				ClassId = dto.ClassId,
				RaceId = dto.RaceId,
				BackgroundId = dto.BackgroundId,
				AlignmentId = dto.AlignmentId,
				SizeId = dto.SizeId,

				Strength = dto.Strength,
				Dexterity = dto.Dexterity,
				Constitution = dto.Constitution,
				Intelligence = dto.Intelligence,
				Wisidom = dto.Wisdom,
				Charisma = dto.Charisma,

				Level = 1
			};

			_db.Dnd5Characters.Add(character);
			int result = await _db.SaveChangesAsync();
			if (result == 0)
				return BadRequest();
			return Created($"/api/characters/{character.Id}", character);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetCharacter(int id)
		{
			var character = await _db.Dnd5Characters
				.Include(c => c.Class)
				.Include(c => c.Race)
				.Include(c => c.Background)
				.Include(c => c.Alignment)
				.Include(c => c.Owner)
				.Include(c => c.Token)
				.FirstOrDefaultAsync(c => c.Id == id);

			if (character == null)
				return NotFound();

			return Ok(character);
		}

		[HttpGet("own")]
		public async Task<IActionResult> GetCharacters(int page = 1, int pageSize = 10)
		{
			var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

			var query = _db.Dnd5Characters
				.Where(x => x.OwnerId == userId)
				.AsNoTracking()
				.OrderBy(x => x.Id)
				.Skip((page - 1) * pageSize)
				.Take(pageSize);

			return Ok(await query.ToListAsync());
		}
	}
}
