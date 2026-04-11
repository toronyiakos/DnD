using Dnd_Api.DTO;
using Dnd_Api.Models;
using Dnd_Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Dnd_Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize(Policy = "UserOnly")]
	public class CharactersController : ControllerBase
	{
		private readonly AppDbContext _db;
		private readonly IJwtService _jwt;

		public CharactersController(AppDbContext db, IJwtService jwt)
		{
			_db = db;
			_jwt = jwt;
		}

		private int CurrentUserId => _jwt.GetUserIdFromToken(User);
		private string CurrentUserRole => _jwt.GetRoleFromToken(User);
		private bool IsPrivileged => CurrentUserRole is "game_master" or "admin";

		private static CharacterDetailDto ToDetail(Dnd5Character c) => new(
			c.Id, c.OwnerId, c.Name,
			c.ClassId, c.Class.ClassName,
			c.SubclassId,
			c.Level,
			c.Strength, c.Dexterity, c.Constitution,
			c.Intelligence, c.Wisidom, c.Charisma,
			c.BackgroundId, c.Background.Name,
			c.RaceId, c.Race.Name,
			c.ArmorClass, c.MaxHp, c.CurrentHp, c.Speed,
			c.HitDiceType, c.HitDiceNumber,
			c.Alignment.Alignment, c.Size.Name,
			c.Money, c.Language, c.Description,
			new SkillsDto(
				c.AcrobaticsDexProf, c.AcrobaticsDexExp,
				c.AnimalHandlingWisProf, c.AnimalHandlingWisExp,
				c.ArcanaIntProf, c.ArcanaIntExp,
				c.AthleticsStrProf, c.AthleticsStrExp,
				c.DeceptionChaProf, c.DeceptionChaExp,
				c.HistoryIntProf, c.HistoryIntExp,
				c.InsightWisProf, c.InsightWisExp,
				c.IntimidationChaProf, c.IntimidationChaExp,
				c.InvestigationIntProf, c.InvestigationIntExp,
				c.MedicineWisProf, c.MedicineWisExp,
				c.NatureWisProf, c.NatureWisExp,
				c.PerceptionWisProf, c.PerceptionWisExp,
				c.PerforanceChaProf, c.PersuasionChaExp,
				c.PersuasionChaProf, c.PersuasionChaExp,
				c.ReligionIntProf, c.ReligionIntExp,
				c.SleightOfHandDexProf, c.SleightOfHandDexExp,
				c.StealthDexProf, c.StealthDexExp,
				c.SurvivalWisProf, c.SurvivalWisExp,
				c.JackOfAllTrades
			)
		);

		private IQueryable<Dnd5Character> BaseQuery() =>
			_db.Dnd5Characters
				.Include(c => c.Class)
				.Include(c => c.Background)
				.Include(c => c.Race)
				.Include(c => c.Alignment)
				.Include(c => c.Size);



		// -- Endpoints ----------------------------------------------
		// -- Character ---------------------------

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var query = BaseQuery();

			if (!IsPrivileged)
				query = query.Where(c => c.OwnerId == CurrentUserId);

			var list = await query
				.Select(c => new CharacterSummaryDto(
					c.Id, c.Name, c.Class.ClassName, c.Race.Name,
					c.Level, c.CurrentHp, c.MaxHp))
				.ToListAsync();

			return Ok(list);
		}


		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetById(int id)
		{
			var character = await BaseQuery().FirstOrDefaultAsync(c => c.Id == id);
			if (character is null) return NotFound("Character not found.");

			if (!IsPrivileged && character.OwnerId != CurrentUserId)
				return Forbid();

			return Ok(ToDetail(character));
		}


		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateCharacterDto dto)
		{
			var character = new Dnd5Character
			{
				OwnerId = CurrentUserId,
				Name = dto.Name,
				ClassId = dto.ClassId,
				SubclassId = dto.SubclassId,
				Level = dto.Level,
				Strength = dto.Strength,
				Dexterity = dto.Dexterity,
				Constitution = dto.Constitution,
				Intelligence = dto.Intelligence,
				Wisidom = dto.Wisdom,
				Charisma = dto.Charisma,
				BackgroundId = dto.BackgroundId,
				RaceId = dto.RaceId,
				ArmorClass = dto.ArmorClass,
				MaxHp = dto.MaxHp,
				CurrentHp = dto.CurrentHp,
				Speed = dto.Speed,
				HitDiceType = dto.HitDiceType,
				HitDiceNumber = dto.Level,
				AlignmentId = dto.AlignmentId,
				SizeId = dto.SizeId,
				Money = dto.Money,
				Language = dto.Language,
				Description = dto.Description
			};

			_db.Dnd5Characters.Add(character);
			await _db.SaveChangesAsync();

			return CreatedAtAction(nameof(GetById), new { id = character.Id }, new { id = character.Id });
		}


		[HttpPatch("{id:int}")]
		public async Task<IActionResult> Update(int id, [FromBody] UpdateCharacterDto dto)
		{
			var character = await _db.Dnd5Characters.FindAsync(id);
			if (character is null) return NotFound("Character not found.");

			if (!IsPrivileged && character.OwnerId != CurrentUserId)
				return Forbid();

			if (dto.Name is not null) character.Name = dto.Name;
			if (dto.Level is not null) character.Level = dto.Level.Value;
			if (dto.Strength is not null) character.Strength = dto.Strength.Value;
			if (dto.Dexterity is not null) character.Dexterity = dto.Dexterity.Value;
			if (dto.Constitution is not null) character.Constitution = dto.Constitution.Value;
			if (dto.Intelligence is not null) character.Intelligence = dto.Intelligence.Value;
			if (dto.Wisdom is not null) character.Wisidom = dto.Wisdom.Value;
			if (dto.Charisma is not null) character.Charisma = dto.Charisma.Value;
			if (dto.ArmorClass is not null) character.ArmorClass = dto.ArmorClass.Value;
			if (dto.MaxHp is not null) character.MaxHp = dto.MaxHp.Value;
			if (dto.CurrentHp is not null) character.CurrentHp = dto.CurrentHp.Value;
			if (dto.Speed is not null) character.Speed = dto.Speed.Value;
			if (dto.Money is not null) character.Money = dto.Money.Value;
			if (dto.Description is not null) character.Description = dto.Description;

			await _db.SaveChangesAsync();
			return NoContent();
		}


		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			var character = await _db.Dnd5Characters.FindAsync(id);
			if(character is null) return NotFound("Character not found.");

			if (!IsPrivileged && character.OwnerId != CurrentUserId)
				return Forbid();

			_db.Dnd5Characters.Remove(character);
			await _db.SaveChangesAsync();
			return NoContent();
		}


		// -- Inventory ---------------------------

		[HttpGet("{id:int}/inventory")]
		public async Task<IActionResult> GetInventory(int id)
		{
			var character = await _db.Dnd5Characters.FindAsync(id);
			if(character is null) return NotFound("Character not found.");

			if (!IsPrivileged && character.OwnerId != CurrentUserId)
				return Forbid();

			var items = await _db.Dnd5Inventories
				.Where(i => i.PlayerId == id)
				.Include(i => i.Item).ThenInclude(i => i.Type)
				.Include(i => i.Item).ThenInclude(i => i.Rarity)
				.Select(i => new ItemDto(
					i.Item.Id, i.Item.Name,
					i.Item.Type.Name, i.Item.Rarity.Name,
					i.Item.Price, i.Item.Desc,
					i.Item.Attuned, i.Item.Weight))
				.ToListAsync();

			return Ok(items);

		}


		[HttpPost("{id:int}/inventory/{itemId:int}")]
		public async Task<IActionResult> AddItem(int id, int itemId)
		{
			var character = await _db.Dnd5Characters.FindAsync(id);
			if (character is null) return NotFound("Character not found.");

			if (!IsPrivileged && character.OwnerId != CurrentUserId)
				return Forbid();

			var item = await _db.Dnd5Items.FindAsync(itemId);
			if(item is null) return NotFound("Item not found.");

			var exists = await _db.Dnd5Inventories.AnyAsync(i => i.PlayerId == id && i.ItemId == itemId);
			if (exists) return Conflict("Item already in inventory.");

			_db.Dnd5Inventories.Add(new Dnd5Inventory { PlayerId = id, ItemId = itemId });
			await _db.SaveChangesAsync();
			return NoContent();
		}

		[HttpDelete("{id:int}/inventory/{itemId:int}")]
		public async Task<IActionResult> RemoveItem(int id, int itemId)
		{
			var character = await _db.Dnd5Characters.FindAsync(id);
			if (character is null) return NotFound("Character not found.");

			if (!IsPrivileged && character.OwnerId != CurrentUserId)
				return Forbid();

			var entry = await _db.Dnd5Inventories.FirstOrDefaultAsync(i => i.PlayerId == id && i.ItemId == itemId);
			if (entry is null) return NotFound("Item not in inventory.");

			_db.Dnd5Inventories.Remove(entry);
			await _db.SaveChangesAsync();
			return NoContent();
		}

	}
}
