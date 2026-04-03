using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Models;

[Table("dnd5_characters")]
[Index("AlignmentId", Name = "FK_dnd5_characters_alignment")]
[Index("BackgroundId", Name = "FK_dnd5_characters_background_id")]
[Index("ClassId", Name = "FK_dnd5_characters_class_id")]
[Index("OwnerId", Name = "FK_dnd5_characters_owner_id")]
public partial class Dnd5Character
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("token_id", TypeName = "int(11)")]
    public int? TokenId { get; set; }

    [Column("owner_id", TypeName = "int(11)")]
    public int? OwnerId { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("class_id", TypeName = "int(11)")]
    public int ClassId { get; set; }

    [Column("subclass_id", TypeName = "int(11)")]
    public int? SubclassId { get; set; }

    [Column("level", TypeName = "int(11) unsigned")]
    public uint Level { get; set; }

    [Column("strength", TypeName = "int(11) unsigned")]
    public uint Strength { get; set; }

    [Column("dexterity", TypeName = "int(11) unsigned")]
    public uint Dexterity { get; set; }

    [Column("constitution", TypeName = "int(11) unsigned")]
    public uint Constitution { get; set; }

    [Column("intelligence", TypeName = "int(11) unsigned")]
    public uint Intelligence { get; set; }

    [Column("wisidom", TypeName = "int(11) unsigned")]
    public uint Wisidom { get; set; }

    [Column("charisma", TypeName = "int(11) unsigned")]
    public uint Charisma { get; set; }

    [Column("background_id", TypeName = "int(11)")]
    public int BackgroundId { get; set; }

    [Column("race_id", TypeName = "int(11)")]
    public int RaceId { get; set; }

    [Column("armor_class", TypeName = "int(11) unsigned")]
    public uint ArmorClass { get; set; }

    [Column("max_hp", TypeName = "int(11) unsigned")]
    public uint MaxHp { get; set; }

    [Column("current_hp", TypeName = "int(11)")]
    public int CurrentHp { get; set; }

    [Column("speed", TypeName = "int(11) unsigned")]
    public uint Speed { get; set; }

    [Column("hit_dice_type", TypeName = "int(11) unsigned")]
    public uint HitDiceType { get; set; }

    [Column("hit_dice_number", TypeName = "int(11) unsigned")]
    public uint HitDiceNumber { get; set; }

    [Column("saving_throw_str_prof")]
    public bool SavingThrowStrProf { get; set; }

    [Column("saving_throw_con_prof")]
    public bool SavingThrowConProf { get; set; }

    [Column("saving_throw_dex_prof")]
    public bool SavingThrowDexProf { get; set; }

    [Column("saving_throw_int_prof")]
    public bool SavingThrowIntProf { get; set; }

    [Column("saving_throw_wis_prof")]
    public bool SavingThrowWisProf { get; set; }

    [Column("saving_throw_cha_prof")]
    public bool SavingThrowChaProf { get; set; }

    [Column("item_proficiency")]
    [StringLength(255)]
    public string? ItemProficiency { get; set; }

    [Column("weapon_proficiency")]
    [StringLength(255)]
    public string? WeaponProficiency { get; set; }

    [Column("language")]
    [StringLength(255)]
    public string Language { get; set; } = null!;

    [Column("description")]
    [StringLength(100)]
    public string? Description { get; set; }

    [Column("money", TypeName = "int(11)")]
    public int Money { get; set; }

    [Column("Acrobatics(dex)_prof")]
    public bool AcrobaticsDexProf { get; set; }

    [Column("Animal_Handling(wis)_prof")]
    public bool AnimalHandlingWisProf { get; set; }

    [Column("Arcana(int)_prof")]
    public bool ArcanaIntProf { get; set; }

    [Column("Athletics(str)_prof")]
    public bool AthleticsStrProf { get; set; }

    [Column("Deception(cha)_prof")]
    public bool DeceptionChaProf { get; set; }

    [Column("History(int)_prof")]
    public bool HistoryIntProf { get; set; }

    [Column("Insight(wis)_prof")]
    public bool InsightWisProf { get; set; }

    [Column("Intimidation(cha)_prof")]
    public bool IntimidationChaProf { get; set; }

    [Column("Investigation(int)_prof")]
    public bool InvestigationIntProf { get; set; }

    [Column("Medicine(wis)_prof")]
    public bool MedicineWisProf { get; set; }

    [Column("Nature(wis)_prof")]
    public bool NatureWisProf { get; set; }

    [Column("Perception(wis)_prof")]
    public bool PerceptionWisProf { get; set; }

    [Column("Perforance(cha)_prof")]
    public bool PerforanceChaProf { get; set; }

    [Column("Persuasion(cha)_prof")]
    public bool PersuasionChaProf { get; set; }

    [Column("Religion(int)_prof")]
    public bool ReligionIntProf { get; set; }

    [Column("Sleight_of_Hand(dex)_prof")]
    public bool SleightOfHandDexProf { get; set; }

    [Column("Stealth(dex)_prof")]
    public bool StealthDexProf { get; set; }

    [Column("Survival(wis)_prof")]
    public bool SurvivalWisProf { get; set; }

    [Column("Acrobatics(dex)_exp")]
    public bool AcrobaticsDexExp { get; set; }

    [Column("Animal_Handling(wis)_exp")]
    public bool AnimalHandlingWisExp { get; set; }

    [Column("Arcana(int)_exp")]
    public bool ArcanaIntExp { get; set; }

    [Column("Athletics(str)_exp")]
    public bool AthleticsStrExp { get; set; }

    [Column("Deception(cha)_exp")]
    public bool DeceptionChaExp { get; set; }

    [Column("History(int)_exp")]
    public bool HistoryIntExp { get; set; }

    [Column("Insight(wis)_exp")]
    public bool InsightWisExp { get; set; }

    [Column("Intimidation(cha)_exp")]
    public bool IntimidationChaExp { get; set; }

    [Column("Investigation(int)_exp")]
    public bool InvestigationIntExp { get; set; }

    [Column("Medicine(wis)_exp")]
    public bool MedicineWisExp { get; set; }

    [Column("Nature(wis)_exp")]
    public bool NatureWisExp { get; set; }

    [Column("Perception(wis)_exp")]
    public bool PerceptionWisExp { get; set; }

    [Column("Perforance(cha)_exp")]
    public bool PerforanceChaExp { get; set; }

    [Column("Persuasion(cha)_exp")]
    public bool PersuasionChaExp { get; set; }

    [Column("Religion(int)_exp")]
    public bool ReligionIntExp { get; set; }

    [Column("Sleight_of_Hand(dex)_exp")]
    public bool SleightOfHandDexExp { get; set; }

    [Column("Stealth(dex)_exp")]
    public bool StealthDexExp { get; set; }

    [Column("Survival(wis)_exp")]
    public bool SurvivalWisExp { get; set; }

    [Column("jack_of_all_trades")]
    public bool JackOfAllTrades { get; set; }

    [Column("asi(4)_id", TypeName = "int(11)")]
    public int? Asi4Id { get; set; }

    [Column("asi(6)_id", TypeName = "int(11)")]
    public int? Asi6Id { get; set; }

    [Column("asi(8)_id", TypeName = "int(11)")]
    public int? Asi8Id { get; set; }

    [Column("asi(12)_id", TypeName = "int(11)")]
    public int? Asi12Id { get; set; }

    [Column("asi(14)_id", TypeName = "int(11)")]
    public int? Asi14Id { get; set; }

    [Column("asi(16)_id", TypeName = "int(11)")]
    public int? Asi16Id { get; set; }

    [Column("asi(19)_id", TypeName = "int(11)")]
    public int? Asi19Id { get; set; }

    [Column("alignment_id", TypeName = "smallint(6)")]
    public short AlignmentId { get; set; }

    [Column("size_id", TypeName = "smallint(6)")]
    public short SizeId { get; set; }

    [ForeignKey("AlignmentId")]
    [InverseProperty("Dnd5Characters")]
    public virtual Dnd5Alignment Alignment { get; set; } = null!;

    [ForeignKey("BackgroundId")]
    [InverseProperty("Dnd5Characters")]
    public virtual Dnd5Background Background { get; set; } = null!;

    [ForeignKey("ClassId")]
    [InverseProperty("Dnd5Characters")]
    public virtual Dnd5Class Class { get; set; } = null!;

    [ForeignKey("OwnerId")]
    [InverseProperty("Dnd5Characters")]
    public virtual AccountUser? Owner { get; set; }
}
