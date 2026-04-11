namespace Dnd_Api.DTO
{
	public record SpellDto(
		int SpellId,
		string SpellName,
		int SpellLevel,
		string SpellType,
		string CastingTime,
		string SpellRange,
		string Components,
		string Duration,
		string Description,
		string HigherLevels
	);

	public record CreateSpellDto(
		string SpellName,
		int SpellLevel,
		string SpellType,
		string CastingTime,
		string SpellRange,
		string Components,
		string Duration,
		string Description,
		string HigherLevels
	);
}
