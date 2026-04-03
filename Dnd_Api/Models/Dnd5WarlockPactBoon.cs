using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Models;

[Keyless]
[Table("dnd5_warlock_pact_boons")]
public partial class Dnd5WarlockPactBoon
{
    [Column("name")]
    [StringLength(100)]
    public string? Name { get; set; }

    [Column("effect1", TypeName = "text")]
    public string? Effect1 { get; set; }

    [Column("effect2", TypeName = "text")]
    public string? Effect2 { get; set; }

    [Column("effect3", TypeName = "text")]
    public string? Effect3 { get; set; }

    [Column("effect4", TypeName = "text")]
    public string? Effect4 { get; set; }
}
