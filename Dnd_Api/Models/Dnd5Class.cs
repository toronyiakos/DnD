using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Models;

[Table("dnd5_classes")]
public partial class Dnd5Class
{
    [Key]
    [Column("class_id", TypeName = "int(11)")]
    public int ClassId { get; set; }

    [Column("class_name")]
    [StringLength(9)]
    public string ClassName { get; set; } = null!;

    [Column("hit_die", TypeName = "int(11)")]
    public int HitDie { get; set; }

    [Column("armor_prof")]
    [StringLength(255)]
    public string? ArmorProf { get; set; }

    [Column("weapon_prof")]
    [StringLength(255)]
    public string? WeaponProf { get; set; }

    [Column("tool_prof")]
    [StringLength(255)]
    public string? ToolProf { get; set; }

    [Column("skill_prof")]
    [StringLength(255)]
    public string? SkillProf { get; set; }

    [Column("saving_throw_str")]
    public bool SavingThrowStr { get; set; }

    [Column("saving_throw_dex")]
    public bool SavingThrowDex { get; set; }

    [Column("saving_throw_con")]
    public bool SavingThrowCon { get; set; }

    [Column("saving_throw_int")]
    public bool SavingThrowInt { get; set; }

    [Column("saving_throw_wis")]
    public bool SavingThrowWis { get; set; }

    [Column("saving_throw_cha")]
    public bool SavingThrowCha { get; set; }

    [InverseProperty("Class")]
    public virtual ICollection<Dnd5Character> Dnd5Characters { get; set; } = new List<Dnd5Character>();

    [InverseProperty("Class")]
    public virtual ICollection<Dnd5ClassSkill> Dnd5ClassSkills { get; set; } = new List<Dnd5ClassSkill>();

    [InverseProperty("MainClass")]
    public virtual ICollection<Dnd5SubclassName> Dnd5SubclassNames { get; set; } = new List<Dnd5SubclassName>();
}
