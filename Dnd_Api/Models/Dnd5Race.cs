using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Models;

[Table("dnd5_races")]
public partial class Dnd5Race
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("age")]
    [StringLength(255)]
    public string Age { get; set; } = null!;

    [Column("alignment")]
    [StringLength(255)]
    public string Alignment { get; set; } = null!;

    [Column("size")]
    [StringLength(255)]
    public string Size { get; set; } = null!;

    [Column("speed_walk", TypeName = "smallint(6)")]
    public short SpeedWalk { get; set; }

    [Column("speed_fly", TypeName = "smallint(6)")]
    public short SpeedFly { get; set; }

    [Column("speed_burrow", TypeName = "smallint(6)")]
    public short SpeedBurrow { get; set; }

    [Column("language")]
    [StringLength(255)]
    public string Language { get; set; } = null!;

    [Column("lore", TypeName = "text")]
    public string Lore { get; set; } = null!;

    [InverseProperty("Race")]
    public virtual ICollection<Dnd5Racial> Dnd5Racials { get; set; } = new List<Dnd5Racial>();

    [InverseProperty("Race")]
    public virtual ICollection<Dnd5Subrace> Dnd5Subraces { get; set; } = new List<Dnd5Subrace>();
}
