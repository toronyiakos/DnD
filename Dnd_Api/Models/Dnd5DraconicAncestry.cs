using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Models;

[Keyless]
[Table("dnd5_draconic_ancestry")]
public partial class Dnd5DraconicAncestry
{
    [Column("color")]
    [StringLength(255)]
    public string? Color { get; set; }

    [Column("damage_type")]
    [StringLength(255)]
    public string? DamageType { get; set; }
}
