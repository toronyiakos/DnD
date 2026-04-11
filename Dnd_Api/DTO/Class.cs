namespace Dnd_Api.DTO
{
	public record ClassDto(
		int ClassId,
		string ClassName,
		int HitDie,
		string? ArmorProf,
		string? WeaponProf,
		string? ToolProf,
		string? SkillProf,
		bool SavingThrowStr,
		bool SavingThrowDex,
		bool SavingThrowCon,
		bool SavingThrowInt,
		bool SavingThrowWis,
		bool SavingThrowCha
	);
}
