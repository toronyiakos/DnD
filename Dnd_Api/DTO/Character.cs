namespace Dnd_Api.DTO
{
	public record CharacterSummaryDto(
		int Id,
		string Name,
		string ClassName,
		string RaceName,
		int Level,
		int CurrentHp,
		uint MaxHp
	);

	public record CharacterDetailDto(
		int Id,
		int? OwnerId,
		string Name,
		int ClassId,
		string ClassName,
		int? SubclassId,
		string? SubclassName,
		int Level,
		uint Strength,
		uint Dexterity,
		uint Constitution,
		uint Intelligence,
		uint Wisdom,
		uint Charisma,
		int BackgroundId,
		string BackgroundName,
		int RaceId,
		string RaceName,
		uint ArmorClass,
		uint MaxHp,
		int CurrentHp,
		uint Speed,
		uint HitDiceType,
		uint HitDiceNumber,
		string AlignmentName,
		string SizeName,
		int Money,
		string Language,
		string? Description,
		SkillsDto Skills
	);

	public record SkillsDto(
		bool AcrobaticsDexProf, bool AcrobaticsDexExp,
		bool AnimalHandlingWisProf, bool AnimalHandlingWisExp,
		bool ArcanaIntProf, bool ArcanaIntExp,
		bool AthleticsStrProf, bool AthleticsStrExp,
		bool DeceptionChaProf, bool DeceptionChaExp,
		bool HistoryIntProf, bool HistoryIntExp,
		bool InsightWisProf, bool InsightWisExp,
		bool IntimidationChaProf, bool IntimidationChaExp,
		bool InvestigationIntProf, bool InvestigationIntExp,
		bool MedicineWisProf, bool MedicineWisExp,
		bool NatureWisProf, bool NatureWisExp,
		bool PerceptionWisProf, bool PerceptionWisExp,
		bool PerformanceChaProf, bool PerformanceChaExp,
		bool PersuasionChaProf, bool PersuasionChaExp,
		bool ReligionIntProf, bool ReligionIntExp,
		bool SleightOfHandDexProf, bool SleightOfHandDexExp,
		bool StealthDexProf, bool StealthDexExp,
		bool SurvivalWisProf, bool SurvivalWisExp,
		bool JackOfAllTrades
	);

	public record CreateCharacterDto(
		string Name,
		int ClassId,
		int? SubclassId,
		int Level,
		uint Strength,
		uint Dexterity,
		uint Constitution,
		uint Intelligence,
		uint Wisdom,
		uint Charisma,
		int BackgroundId,
		int RaceId,
		uint ArmorClass,
		uint MaxHp,
		int CurrentHp,
		uint Speed,
		uint HitDiceType,
		uint HitDiceNumber,
		short AlignmentId,
		short SizeId,
		int Money,
		string Language = "common",
		string? Description = null
	);

	public record UpdateCharacterDto(
		string? Name,
		int? Level,
		uint? Strength,
		uint? Dexterity,
		uint? Constitution,
		uint? Intelligence,
		uint? Wisdom,
		uint? Charisma,
		uint? ArmorClass,
		uint? MaxHp,
		int? CurrentHp,
		uint? Speed,
		int? Money,
		string? Description
	);
}
