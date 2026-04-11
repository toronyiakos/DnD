namespace Dnd_Api.DTO
{
	public record FeatDto(
		int Id,
		string Name,
		string Description,
		string? Prerequisite
	);
}
