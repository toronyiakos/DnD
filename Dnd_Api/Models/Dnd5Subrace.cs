using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Models;

[Table("dnd5_subrace")]
[Index("RaceId", Name = "FK_subrace_race_id")]
public partial class Dnd5Subrace
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("race_id", TypeName = "int(11)")]
    public int RaceId { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("lore", TypeName = "text")]
    public string? Lore { get; set; }

    [ForeignKey("RaceId")]
    [InverseProperty("Dnd5Subraces")]
    public virtual Dnd5Race Race { get; set; } = null!;
}
