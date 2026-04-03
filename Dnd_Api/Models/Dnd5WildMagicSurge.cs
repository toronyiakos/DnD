using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Models;

[Table("dnd5_wild_magic_surge")]
public partial class Dnd5WildMagicSurge
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("d100_range")]
    [StringLength(255)]
    public string? D100Range { get; set; }

    [Column("effect")]
    [StringLength(255)]
    public string? Effect { get; set; }
}
