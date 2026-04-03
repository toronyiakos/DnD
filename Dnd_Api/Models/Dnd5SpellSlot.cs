using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Models;

[Keyless]
[Table("dnd5_spell_slots")]
[Index("ClassId", Name = "FK_dnd5_spell_slots_class_id")]
public partial class Dnd5SpellSlot
{
    [Column("class_id", TypeName = "int(11)")]
    public int ClassId { get; set; }

    [Column("subclass_id", TypeName = "int(11)")]
    public int? SubclassId { get; set; }

    [Column("level", TypeName = "int(11)")]
    public int Level { get; set; }

    [Column("spells_known", TypeName = "int(11)")]
    public int? SpellsKnown { get; set; }

    [Column("cantrip", TypeName = "int(11)")]
    public int? Cantrip { get; set; }

    [Column("1st", TypeName = "int(11)")]
    public int? _1st { get; set; }

    [Column("2nd", TypeName = "int(11)")]
    public int? _2nd { get; set; }

    [Column("3rd", TypeName = "int(11)")]
    public int? _3rd { get; set; }

    [Column("4th", TypeName = "int(11)")]
    public int? _4th { get; set; }

    [Column("5th", TypeName = "int(11)")]
    public int? _5th { get; set; }

    [Column("6th", TypeName = "int(11)")]
    public int? _6th { get; set; }

    [Column("7th", TypeName = "int(11)")]
    public int? _7th { get; set; }

    [Column("8th", TypeName = "int(11)")]
    public int? _8th { get; set; }

    [Column("9th", TypeName = "int(11)")]
    public int? _9th { get; set; }

    [ForeignKey("ClassId")]
    public virtual Dnd5Class Class { get; set; } = null!;
}
