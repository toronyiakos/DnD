namespace Dnd_Api.DTO
{
	public record ItemDto(
		int Id,
		string Name,
		string TypeName,
		string RarityName,
		int? Price,
		string Description,
		bool Attuned,
		int Weight
	);

	public record CreateItemDto(
		string Name,
		short TypeId,
		short RarityId,
		int? Price,
		string Description,
		bool Attuned,
		int Weight
	);
}
