namespace Dnd_Api.DTO
{
	public record MonsterDto(
		int Id,
		string Name,
		int ArmorClass,
		int Hp,
		short Speed,
		short SpeedFly,
		short SpeedBurrow,
		short Str,
		short Dex,
		short Con,
		short Int,
		short Wis,
		short Cha,
		string SizeName,
		string TypeName,
		string AlignmentName
	);

	public record CreateMonsterDto(
		string Name,
		int? TokenId,
		int ArmorClass,
		int Hp,
		short Speed,
		short SpeedFly,
		short SpeedBurrow,
		short Str,
		short Dex,
		short Con,
		short Int,
		short Wis,
		short Cha,
		short SizeId,
		short TypeId,
		short AlignmentId
	);
}
