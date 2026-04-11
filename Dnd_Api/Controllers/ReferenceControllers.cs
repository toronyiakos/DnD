using Dnd_Api.DTO;
using Dnd_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Controllers
{
	// ── Races ─────────────────────────────────────────────────────────────────────

	[ApiController]
	[Route("api/[controller]")]
	[Authorize(Policy = "UserOnly")]
	public class RacesController : ControllerBase
	{
		private readonly AppDbContext _db;
		public RacesController(AppDbContext db) => _db = db;

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var races = await _db.Dnd5Races
				.Select(r => new RaceDto(
					r.Id, r.Name, r.Age, r.Alignment, r.Size,
					r.SpeedWalk, r.SpeedFly, r.SpeedBurrow,
					r.Language, r.Lore))
				.ToListAsync();
			return Ok(races);
		}

		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetById(int id)
		{
			var r = await _db.Dnd5Races.FindAsync(id);
			if (r is null) return NotFound();
			return Ok(new RaceDto(r.Id, r.Name, r.Age, r.Alignment, r.Size,
				r.SpeedWalk, r.SpeedFly, r.SpeedBurrow, r.Language, r.Lore));
		}

		[HttpGet("{id:int}/subraces")]
		public async Task<IActionResult> GetSubraces(int id)
		{
			var subraces = await _db.Dnd5Subraces
				.Where(s => s.RaceId == id)
				.Select(s => new { s.Id, s.Name, s.Lore })
				.ToListAsync();
			return Ok(subraces);
		}

		[HttpGet("{id:int}/racials")]
		public async Task<IActionResult> GetRacials(int id)
		{
			var racials = await _db.Dnd5Racials
				.Where(r => r.RaceId == id)
				.Select(r => new { r.Id, r.Name, r.Desc, r.SubraceId })
				.ToListAsync();
			return Ok(racials);
		}
	}

	// ── Backgrounds ───────────────────────────────────────────────────────────────

	[ApiController]
	[Route("api/[controller]")]
	[Authorize(Policy = "UserOnly")]
	public class BackgroundsController : ControllerBase
	{
		private readonly AppDbContext _db;
		public BackgroundsController(AppDbContext db) => _db = db;

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var list = await _db.Dnd5Backgrounds
				.Select(b => new BackgroundDto(
					b.Id, b.Name, b.SkillProficiencies,
					b.ToolProficiencies, b.Languages, b.Equipment))
				.ToListAsync();
			return Ok(list);
		}

		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetById(int id)
		{
			var b = await _db.Dnd5Backgrounds.FindAsync(id);
			if (b is null) return NotFound();
			return Ok(new BackgroundDto(b.Id, b.Name, b.SkillProficiencies,
				b.ToolProficiencies, b.Languages, b.Equipment));
		}
	}

	// ── Items ─────────────────────────────────────────────────────────────────────

	[ApiController]
	[Route("api/[controller]")]
	[Authorize(Policy = "UserOnly")]
	public class ItemsController : ControllerBase
	{
		private readonly AppDbContext _db;
		public ItemsController(AppDbContext db) => _db = db;

		private static ItemDto ToDto(Dnd5Item i) =>
			new(i.Id, i.Name, i.Type.Name, i.Rarity.Name,
				i.Price, i.Desc, i.Attuned, i.Weight);

		private IQueryable<Dnd5Item> BaseQuery() =>
			_db.Dnd5Items.Include(i => i.Type).Include(i => i.Rarity);

		[HttpGet]
		public async Task<IActionResult> GetAll(
			[FromQuery] short? typeId,
			[FromQuery] short? rarityId,
			[FromQuery] bool? attuned)
		{
			var query = BaseQuery();
			if (typeId is not null) query = query.Where(i => i.TypeId == typeId);
			if (rarityId is not null) query = query.Where(i => i.RarityId == rarityId);
			if (attuned is not null) query = query.Where(i => i.Attuned == attuned);

			return Ok(await query.Select(i => ToDto(i)).ToListAsync());
		}

		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetById(int id)
		{
			var item = await BaseQuery().FirstOrDefaultAsync(i => i.Id == id);
			return item is null ? NotFound() : Ok(ToDto(item));
		}

		[HttpPost]
		[Authorize(Policy = "GameMasterOnly")]
		public async Task<IActionResult> Create([FromBody] CreateItemDto dto)
		{
			var item = new Dnd5Item
			{
				Name = dto.Name,
				TypeId = dto.TypeId,
				RarityId = dto.RarityId,
				Price = dto.Price,
				Desc = dto.Description,
				Attuned = dto.Attuned,
				Weight = dto.Weight
			};
			_db.Dnd5Items.Add(item);
			await _db.SaveChangesAsync();

			await _db.Entry(item).Reference(i => i.Type).LoadAsync();
			await _db.Entry(item).Reference(i => i.Rarity).LoadAsync();

			return CreatedAtAction(nameof(GetById), new { id = item.Id }, ToDto(item));
		}

		[HttpPut("{id:int}")]
		[Authorize(Policy = "GameMasterOnly")]
		public async Task<IActionResult> Update(int id, [FromBody] CreateItemDto dto)
		{
			var item = await _db.Dnd5Items.FindAsync(id);
			if (item is null) return NotFound();

			item.Name = dto.Name;
			item.TypeId = dto.TypeId;
			item.RarityId = dto.RarityId;
			item.Price = dto.Price;
			item.Desc = dto.Description;
			item.Attuned = dto.Attuned;
			item.Weight = dto.Weight;

			await _db.SaveChangesAsync();
			return NoContent();
		}

		[HttpDelete("{id:int}")]
		[Authorize(Policy = "AdminOnly")]
		public async Task<IActionResult> Delete(int id)
		{
			var item = await _db.Dnd5Items.FindAsync(id);
			if (item is null) return NotFound();
			_db.Dnd5Items.Remove(item);
			await _db.SaveChangesAsync();
			return NoContent();
		}
	}

	// ── Feats ─────────────────────────────────────────────────────────────────────

	[ApiController]
	[Route("api/[controller]")]
	[Authorize(Policy = "UserOnly")]
	public class FeatsController : ControllerBase
	{
		private readonly AppDbContext _db;
		public FeatsController(AppDbContext db) => _db = db;

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var feats = await _db.Dnd5Feats
				.Select(f => new FeatDto(f.Id, f.Name, f.Desc, f.Prerequisite))
				.ToListAsync();
			return Ok(feats);
		}

		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetById(int id)
		{
			var f = await _db.Dnd5Feats.FindAsync(id);
			return f is null ? NotFound() : Ok(new FeatDto(f.Id, f.Name, f.Desc, f.Prerequisite));
		}

		[HttpPost]
		[Authorize(Policy = "GameMasterOnly")]
		public async Task<IActionResult> Create([FromBody] FeatDto dto)
		{
			var feat = new Dnd5Feat { Name = dto.Name, Desc = dto.Description, Prerequisite = dto.Prerequisite };
			_db.Dnd5Feats.Add(feat);
			await _db.SaveChangesAsync();
			return CreatedAtAction(nameof(GetById), new { id = feat.Id },
				new FeatDto(feat.Id, feat.Name, feat.Desc, feat.Prerequisite));
		}

		[HttpDelete("{id:int}")]
		[Authorize(Policy = "AdminOnly")]
		public async Task<IActionResult> Delete(int id)
		{
			var feat = await _db.Dnd5Feats.FindAsync(id);
			if (feat is null) return NotFound();
			_db.Dnd5Feats.Remove(feat);
			await _db.SaveChangesAsync();
			return NoContent();
		}
	}

	// ── Reference data (read-only for all authenticated users) ───────────────────

	[ApiController]
	[Route("api/reference")]
	[Authorize(Policy = "UserOnly")]
	public class ReferenceController : ControllerBase
	{
		private readonly AppDbContext _db;
		public ReferenceController(AppDbContext db) => _db = db;

		[HttpGet("alignments")]
		public async Task<IActionResult> GetAlignments() =>
			Ok(await _db.Dnd5Alignments.Select(a => new { a.Id, a.Alignment, a.Desc }).ToListAsync());

		[HttpGet("sizes")]
		public async Task<IActionResult> GetSizes() =>
			Ok(await _db.Dnd5Sizes.Select(s => new { s.Id, s.Name, s.Space }).ToListAsync());

		[HttpGet("monster-types")]
		public async Task<IActionResult> GetMonsterTypes() =>
			Ok(await _db.Dnd5MonsterTypes.Select(t => new { t.Id, t.Name }).ToListAsync());

		[HttpGet("item-types")]
		public async Task<IActionResult> GetItemTypes() =>
			Ok(await _db.Dnd5ItemTypes.Select(t => new { t.Id, t.Name }).ToListAsync());

		[HttpGet("item-rarities")]
		public async Task<IActionResult> GetItemRarities() =>
			Ok(await _db.Dnd5ItemRarities.Select(r => new { r.Id, r.Name }).ToListAsync());

		[HttpGet("metamagic")]
		public async Task<IActionResult> GetMetamagic() =>
			Ok(await _db.Dnd5Metamagics.Select(m => new { m.Id, m.Name, m.Desc, m.Cost }).ToListAsync());

		[HttpGet("fighting-styles")]
		public async Task<IActionResult> GetFightingStyles() =>
			Ok(await _db.Dnd5FightingStyles.Select(f => new { f.Id, f.Name, f.Effect, f.Fighter, f.Paladin, f.Ranger }).ToListAsync());

		[HttpGet("eldritch-invocations")]
		public async Task<IActionResult> GetEldritchInvocations() =>
			Ok(await _db.Dnd5EldrichtInvocations.Select(e => new { e.Id, e.Name, e.MinLevel, e.Prerequisite, e.Effect }).ToListAsync());

		[HttpGet("wild-magic-surge")]
		public async Task<IActionResult> GetWildMagicSurge() =>
			Ok(await _db.Dnd5WildMagicSurges.Select(w => new { w.Id, w.D100Range, w.Effect }).ToListAsync());

		[HttpGet("battle-master-maneuvers")]
		public async Task<IActionResult> GetBattleMasterManeuvers() =>
			Ok(await _db.Dnd5BattleMasterManeuvers.Select(b => new { b.Id, b.Name, b.Description }).ToListAsync());

		[HttpGet("druid-form-limits")]
		public async Task<IActionResult> GetDruidFormLimits() =>
			Ok(await _db.Dnd5DruidFormLimits.ToListAsync());

		[HttpGet("draconic-ancestries")]
		public async Task<IActionResult> GetDraconicAncestries() =>
			Ok(await _db.Dnd5DraconicAncestries.ToListAsync());

		[HttpGet("land-druid-domains")]
		public async Task<IActionResult> GetLandDruidDomains() =>
			Ok(await _db.Dnd5LandDruidDomains.Select(d => new { d.Id, d.Name }).ToListAsync());

		[HttpGet("land-druid-domains/{id:int}/spells")]
		public async Task<IActionResult> GetLandDruidDomainSpells(int id) =>
			Ok(await _db.Dnd5LandDruidDomainSpells
				.Where(d => d.LandId == id)
				.Select(d => new { d.Level, d.Spells })
				.ToListAsync());

		[HttpGet("prof-by-level")]
		public async Task<IActionResult> GetProfByLevel() =>
			Ok(await _db.Dnd5Profbylevels.ToListAsync());

		[HttpGet("sorcery-points")]
		public async Task<IActionResult> GetSorceryPoints() =>
			Ok(await _db.Dnd5SorceryPointConversations.ToListAsync());

		[HttpGet("warlock-spell-levels")]
		public async Task<IActionResult> GetWarlockSpellLevels() =>
			Ok(await _db.Dnd5WarlockSpellLevels.ToListAsync());

		[HttpGet("warlock-pact-boons")]
		public async Task<IActionResult> GetWarlockPactBoons() =>
			Ok(await _db.Dnd5WarlockPactBoons.ToListAsync());

		[HttpGet("sneak-attack")]
		public async Task<IActionResult> GetSneakAttack() =>
			Ok(await _db.Dnd5SneakAttackD6s.ToListAsync());
	}
}
