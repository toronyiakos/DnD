namespace Dnd_Api.DTO
{
	public record RaceDto(
		int Id,
		string Name,
		string Age,
		string Alignment,
		string Size,
		short SpeedWalk,
		short SpeedFly,
		short SpeedBurrow,
		string Language,
		string Lore
	);
}
