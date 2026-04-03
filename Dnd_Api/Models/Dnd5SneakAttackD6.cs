using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Models;

[Keyless]
[Table("dnd5_sneak_attack(d6)")]
public partial class Dnd5SneakAttackD6
{
    [Column("level", TypeName = "int(2)")]
    public int? Level { get; set; }

    [Column("damage", TypeName = "int(2)")]
    public int? Damage { get; set; }
}
