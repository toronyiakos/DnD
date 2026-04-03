using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Models;

[Keyless]
[Table("dnd5_sorcery_point_conversation")]
public partial class Dnd5SorceryPointConversation
{
    [Column("spell_slot_level", TypeName = "int(11)")]
    public int SpellSlotLevel { get; set; }

    [Column("sorcery_point_cost", TypeName = "int(11)")]
    public int SorceryPointCost { get; set; }
}
