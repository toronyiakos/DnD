using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Models;

[Keyless]
[Table("dnd5_warlock_spell_level")]
public partial class Dnd5WarlockSpellLevel
{
    [Column("level", TypeName = "int(11)")]
    public int? Level { get; set; }

    [Column("cantrips_known", TypeName = "int(11)")]
    public int? CantripsKnown { get; set; }

    [Column("spells_known", TypeName = "int(11)")]
    public int? SpellsKnown { get; set; }

    [Column("spell_slots", TypeName = "int(11)")]
    public int? SpellSlots { get; set; }

    [Column("spell_slot_level", TypeName = "int(11)")]
    public int? SpellSlotLevel { get; set; }

    [Column("invocations:known", TypeName = "int(11)")]
    public int? InvocationsKnown { get; set; }
}
