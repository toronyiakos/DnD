using Dnd_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Controllers
{
	[ApiController]
	[Authorize]
	[Route("api/Character/{characterId}/inventory")]
	public class InventoryController : ControllerBase
	{
		private readonly AppDbContext _db;

		public InventoryController(AppDbContext db)
		{
			_db = db;
		}

		[HttpGet]
		public async Task<IActionResult> GetInventory(int characterId)
		{
			var inventory = await _db.Dnd5Inventories
				.Where(i => i.PlayerId == characterId)
				.Include(i => i.Item)
					.ThenInclude(x => x.Type)
				.Include(i => i.Item)
					.ThenInclude(x => x.Rarity)
				.Select(i => new
				{
					i.Item.Id,
					i.Item.Name,
					i.Item.Price,
					i.Item.Weight,
					Type = i.Item.Type.Name,
					Rarity = i.Item.Rarity.Name
				})
				.ToListAsync();

			return Ok(inventory);
		}

		[HttpPost("{itemId}")]
		public async Task<IActionResult> AddItem(int characterId, int itemId)
		{
			var character = await _db.Dnd5Characters.FindAsync(characterId);
			if (character == null)
				return NotFound("Character not found");

			var item = await _db.Dnd5Items.FindAsync(itemId);
			if (item == null)
				return NotFound("Item not found");

			var inventory = new Dnd5Inventory
			{
				PlayerId = characterId,
				ItemId = itemId
			};

			_db.Dnd5Inventories.Add(inventory);
			await _db.SaveChangesAsync();

			return Ok(new { message = "Item added to inventory" });
		}

		[HttpDelete("{itemId}")]
		public async Task<IActionResult> RemoveItem(int characterId, int itemId)
		{
			var invenotry = await _db.Dnd5Inventories
				.FirstOrDefaultAsync(x =>
					x.PlayerId == characterId &&
					x.ItemId == itemId);

			if (invenotry == null)
				return NotFound();

			_db.Dnd5Inventories.Remove(invenotry);
			await _db.SaveChangesAsync();

			return Ok(new { message = "Item removed" });
		}
	}
}
