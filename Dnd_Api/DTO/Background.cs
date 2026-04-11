namespace Dnd_Api.DTO
{
	public record BackgroundDto(
		int Id,
		string Name,
		string? SkillProficiencies,
		string? ToolProficiencies,
		string? Languages,
		string? Equipment
	);
}
