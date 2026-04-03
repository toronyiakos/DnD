using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Models;

[Table("dnd5_spells")]
public partial class Dnd5Spell
{
    [Key]
    [Column("spell_id", TypeName = "int(11)")]
    public int SpellId { get; set; }

    [Column("spell_name")]
    [StringLength(128)]
    public string SpellName { get; set; } = null!;

    [Column("spell_level", TypeName = "int(2)")]
    public int SpellLevel { get; set; }

    [Column("spell_type")]
    [StringLength(128)]
    public string SpellType { get; set; } = null!;

    [Column("casting_time")]
    [StringLength(128)]
    public string CastingTime { get; set; } = null!;

    [Column("spell_range")]
    [StringLength(128)]
    public string SpellRange { get; set; } = null!;

    [Column("components")]
    [StringLength(128)]
    public string Components { get; set; } = null!;

    [Column("duration")]
    [StringLength(128)]
    public string Duration { get; set; } = null!;

    [Column("description", TypeName = "text")]
    public string Description { get; set; } = null!;

    [Column("higher_levels", TypeName = "text")]
    public string HigherLevels { get; set; } = null!;
}
