using Dnd_Api.DTO;
using Dnd_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize(Policy = "UserOnly")]
	public class SpellsController : ControllerBase
	{
		private readonly AppDbContext _db;

		public SpellsController(AppDbContext db)
		{
			_db = db;
		}

		private static SpellDto ToDto(Dnd5Spell s) => new(
			s.SpellId, s.SpellName, s.SpellLevel, s.SpellType,
			s.CastingTime, s.SpellRange, s.Components,
			s.Duration, s.Description, s.HigherLevels);



		// -- Endpoints ----------------------------------------------

		[HttpGet]
		public async Task<IActionResult> GetAll(
			[FromQuery] int? level,
			[FromQuery] string? type,
			[FromQuery] int? classId)
		{
			var query = _db.Dnd5Spells.AsQueryable();

			if (level is not null)
				query = query.Where(s => s.SpellLevel == level);

			if (type is not null)
				query = query.Where(s => s.SpellType.Contains(type));

			if (classId is not null)
				query = query.Where(s =>
				_db.Dnd5ClassSpells.Any(cs => cs.SpellId == s.SpellId && cs.ClassId == classId));


			return Ok(await query.Select(s => ToDto(s)).ToListAsync());
		}

		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetById(int id)
		{
			var spell = await _db.Dnd5Spells.FindAsync(id);
			return spell is null ? NotFound() : Ok(ToDto(spell));
		}

		[HttpPost]
		[Authorize(Policy = "GameMasterOnly")]
		public async Task<IActionResult> Create([FromBody] CreateSpellDto dto)
		{
			var spell = new Dnd5Spell
			{
				SpellName = dto.SpellName,
				SpellLevel = dto.SpellLevel,
				SpellType = dto.SpellType,
				CastingTime = dto.CastingTime,
				SpellRange = dto.SpellRange,
				Components = dto.Components,
				Duration = dto.Duration,
				Description = dto.Description,
				HigherLevels = dto.HigherLevels
			};

			_db.Dnd5Spells.Add(spell);
			await _db.SaveChangesAsync();

			return CreatedAtAction(nameof(GetById), new { id = spell.SpellId }, ToDto(spell));
		}

		[HttpPut("{id:int}")]
		[Authorize(Policy = "GameMasterOnly")]
		public async Task<IActionResult> Update(int id, [FromBody] CreateSpellDto dto)
		{
			var spell = await _db.Dnd5Spells.FindAsync(id);
			if (spell is null) return NotFound("Spell not found.");

			spell.SpellName = dto.SpellName;
			spell.SpellLevel = dto.SpellLevel;
			spell.SpellType = dto.SpellType;
			spell.CastingTime = dto.CastingTime;
			spell.SpellRange = dto.SpellRange;
			spell.Components = dto.Components;
			spell.Duration = dto.Duration;
			spell.Description = dto.Description;
			spell.HigherLevels = dto.HigherLevels;

			await _db.SaveChangesAsync();
			return NoContent();
		}

		[HttpDelete("{id:int}")]
		[Authorize(Policy = "AdminOnly")]
		public async Task<IActionResult> Delete(int id)
		{
			var spell = await _db.Dnd5Spells.FindAsync(id);
			if (spell is null) return NotFound("Spell not found.");

			_db.Dnd5Spells.Remove(spell);
			await _db.SaveChangesAsync();
			return NoContent();
		}

	}
}
