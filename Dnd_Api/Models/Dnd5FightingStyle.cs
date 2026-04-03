using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Models;

[Table("dnd5_fighting_styles")]
public partial class Dnd5FightingStyle
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string? Name { get; set; }

    [Column("effect")]
    [StringLength(255)]
    public string? Effect { get; set; }

    [Column("fighter")]
    public bool Fighter { get; set; }

    [Column("paladin")]
    public bool Paladin { get; set; }

    [Column("ranger")]
    public bool Ranger { get; set; }
}
