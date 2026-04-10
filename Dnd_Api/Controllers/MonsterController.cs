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
	public class MonsterController : ControllerBase
	{
		private readonly AppDbContext _db;

		public MonsterController(AppDbContext db)
		{
			_db = db;
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateMonsterDto dto)
		{
			var monster = new Dnd5Monster
			{
				Name = dto.Name,
				ArmorClass = dto.ArmorClass,
				Hp = dto.Hp,
				Speed = dto.Speed,

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

			return Ok(new { messsage = "Monster created", monster.Id });
		}

		[HttpGet]
		public async Task<IActionResult> GetAll(
			int page = 1,
			int pageSize = 10,
			string? name = null,
			short? sizeId = null,
			short? typeId = null,
			short? alignmentId = null)
		{
			var query = _db.Dnd5Monsters
				.Include(m => m.Size)
				.Include(m => m.Type)
				.Include(m => m.Alignment)
				.AsQueryable();

			if (!string.IsNullOrWhiteSpace(name))
				query = query.Where(x => x.Name.Contains(name));

			if (sizeId.HasValue)
				query = query.Where(x => x.SizeId == sizeId);

			if (typeId.HasValue)
				query = query.Where(x => x.TypeId == typeId);

			if (alignmentId.HasValue)
				query = query.Where(x => x.AlignmentId == alignmentId);

			var result = await query
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.Select(x => new
				{
					x.Id,
					x.Name,
					x.ArmorClass,
					x.Hp,
					x.Speed,
					Size = x.Size.Name,
					Type = x.Type.Name,
					Alignment = x.Alignment.Alignment
				})
				.ToListAsync();

			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var monster = await _db.Dnd5Monsters
				.Include(m => m.Size)
				.Include(m => m.Type)
				.Include(m => m.Alignment)
				.FirstOrDefaultAsync(m => m.Id == id);

			if (monster == null)
				return NotFound();

			return Ok(monster);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] CreateMonsterDto dto)
		{
			var monster = await _db.Dnd5Monsters.FindAsync(id);

			if (monster == null)
				return NotFound();

			monster.Name = dto.Name;
			monster.ArmorClass = dto.ArmorClass;
			monster.Hp = dto.Hp;
			monster.Speed = dto.Speed;

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

			return Ok(new { message = "Monster updated" });
		}

	}
}
