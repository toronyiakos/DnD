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
	public class ClassesController : ControllerBase
	{
		private readonly AppDbContext _db;

		public ClassesController(AppDbContext db)
		{
			_db = db;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var classes = await _db.Dnd5Classes
				.Select(c => new ClassDto(
					c.ClassId, c.ClassName, c.HitDie,
					c.ArmorProf, c.WeaponProf, c.ToolProf, c.SkillProf,
					c.SavingThrowStr, c.SavingThrowDex, c.SavingThrowCon,
					c.SavingThrowInt, c.SavingThrowWis, c.SavingThrowCha))
				.ToListAsync();

			return Ok(classes);
		}

		// ── GET /api/classes/{id} ────────────────────────────────────────────────

		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetById(int id)
		{
			var c = await _db.Dnd5Classes.FindAsync(id);
			if (c is null) return NotFound();

			return Ok(new ClassDto(
				c.ClassId, c.ClassName, c.HitDie,
				c.ArmorProf, c.WeaponProf, c.ToolProf, c.SkillProf,
				c.SavingThrowStr, c.SavingThrowDex, c.SavingThrowCon,
				c.SavingThrowInt, c.SavingThrowWis, c.SavingThrowCha));
		}

		// ── GET /api/classes/{id}/subclasses ─────────────────────────────────────

		[HttpGet("{id:int}/subclasses")]
		public async Task<IActionResult> GetSubclasses(int id)
		{
			var subclasses = await _db.Dnd5SubclassNames
				.Where(s => s.MainClassId == id)
				.Select(s => new { s.Id, s.Name })
				.ToListAsync();

			return Ok(subclasses);
		}

		// ── GET /api/classes/{id}/skills ─────────────────────────────────────────

		[HttpGet("{id:int}/skills")]
		public async Task<IActionResult> GetSkills(int id, [FromQuery] int? subclassId)
		{
			var query = _db.Dnd5ClassSkills.Where(s => s.ClassId == id);

			if (subclassId is not null)
				query = query.Where(s => s.SubclassId == subclassId);

			var skills = await query
				.OrderBy(s => s.UnlockLevel)
				.Select(s => new { s.Id, s.Name, s.Desc, s.UnlockLevel, s.SubclassId })
				.ToListAsync();

			return Ok(skills);
		}

		// ── GET /api/classes/{id}/spells ─────────────────────────────────────────

		[HttpGet("{id:int}/spells")]
		public async Task<IActionResult> GetSpells(int id, [FromQuery] int? level)
		{
			var query = _db.Dnd5ClassSpells
				.Where(cs => cs.ClassId == id)
				.Include(cs => cs.Spell)
				.Select(cs => cs.Spell);

			if (level is not null)
				query = query.Where(s => s.SpellLevel == level);

			var spells = await query
				.Select(s => new SpellDto(
					s.SpellId, s.SpellName, s.SpellLevel, s.SpellType,
					s.CastingTime, s.SpellRange, s.Components,
					s.Duration, s.Description, s.HigherLevels))
				.ToListAsync();

			return Ok(spells);
		}

		// ── GET /api/classes/{id}/spellslots ─────────────────────────────────────

		[HttpGet("{id:int}/spellslots")]
		public async Task<IActionResult> GetSpellSlots(int id)
		{
			var slots = await _db.Dnd5SpellSlots
				.Where(ss => ss.ClassId == id)
				.OrderBy(ss => ss.Level)
				.ToListAsync();

			return Ok(slots);
		}
	}
}
