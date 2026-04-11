using Dnd_Api.DTO;
using Dnd_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class MonstersController : ControllerBase
	{
		private readonly AppDbContext _db;

		public MonstersController(AppDbContext db)
		{
			_db = db;
		}

		private static MonsterDto ToDto(Dnd5Monster m) => new(
		m.Id, m.Name, m.ArmorClass, m.Hp,
		m.Speed, m.SpeedFly, m.SpeedBurrow,
		m.Str, m.Dex, m.Con, m.Int, m.Wis, m.Cha,
		m.Size.Name, m.Type.Name, m.Alignment.Alignment);

		private IQueryable<Dnd5Monster> BaseQuery() =>
			_db.Dnd5Monsters
				.Include(m => m.Size)
				.Include(m => m.Type)
				.Include(m => m.Alignment);



		// -- Endpoints ----------------------------------------------

		[HttpGet]
		public async Task<IActionResult> GetAll(
			[FromQuery] string? name,
			[FromQuery] short? typeId,
			[FromQuery] short? sizeId)
		{
			var query = BaseQuery();

			if (name is not null) query = query.Where(m => m.Name.Contains(name));
			if (typeId is not null) query = query.Where(m => m.TypeId == typeId);
			if (sizeId is not null) query = query.Where(m => m.SizeId == sizeId);

			return Ok(await query.Select(m => ToDto(m)).ToListAsync());
		}

		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetById(int id)
		{
			var monster = await BaseQuery().FirstOrDefaultAsync(m => m.Id == id);
			return monster is null ? NotFound() : Ok(ToDto(monster));
		}

		[HttpPost]
		[Authorize(Policy = "GameMasterOnly")]
		public async Task<IActionResult> Create([FromBody] CreateMonsterDto dto)
		{
			var monster = new Dnd5Monster
			{
				Name = dto.Name,
				TokenId = dto.TokenId,
				ArmorClass = dto.ArmorClass,
				Hp = dto.Hp,
				Speed = dto.Speed,
				SpeedFly = dto.SpeedFly,
				SpeedBurrow = dto.SpeedBurrow,
				Str = dto.Str,
				Dex = dto.Dex,
				Con = dto.Con,
				Int = dto.Int,
				Wis = dto.Wis,
				Cha = dto.Cha,
				SizeId = dto.SizeId,
				TypeId = dto.TypeId,
				AlignmentId = dto.AlignmentId
			};

			_db.Dnd5Monsters.Add(monster);
			await _db.SaveChangesAsync();

			await _db.Entry(monster).Reference(m => m.Size).LoadAsync();
			await _db.Entry(monster).Reference(m => m.Type).LoadAsync();
			await _db.Entry(monster).Reference(m => m.Alignment).LoadAsync();

			return CreatedAtAction(nameof(GetById), new { id = monster.Id }, ToDto(monster));
		}

		[HttpPut("{id:int}")]
		[Authorize(Policy = "GameMasterOnly")]
		public async Task<IActionResult> Update(int id, [FromBody] CreateMonsterDto dto)
		{
			var monster = await _db.Dnd5Monsters.FindAsync(id);
			if (monster is null) return NotFound();

			monster.Name = dto.Name;
			monster.TokenId = dto.TokenId;
			monster.ArmorClass = dto.ArmorClass;
			monster.Hp = dto.Hp;
			monster.Speed = dto.Speed;
			monster.SpeedFly = dto.SpeedFly;
			monster.SpeedBurrow = dto.SpeedBurrow;
			monster.Str = dto.Str;
			monster.Dex = dto.Dex;
			monster.Con = dto.Con;
			monster.Int = dto.Int;
			monster.Wis = dto.Wis;
			monster.Cha = dto.Cha;
			monster.SizeId = dto.SizeId;
			monster.TypeId = dto.TypeId;
			monster.AlignmentId = dto.AlignmentId;

			await _db.SaveChangesAsync();
			return NoContent();
		}

		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			var monster = await _db.Dnd5Monsters.FindAsync(id);
			if(monster is null) return NotFound();

			_db.Dnd5Monsters.Remove(monster);
			await _db.SaveChangesAsync();
			return NoContent();
		}

	}
}
