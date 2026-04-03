using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Models;

[Table("dnd5_racials")]
[Index("RaceId", Name = "FK_dnd_racials_race_id")]
public partial class Dnd5Racial
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("race_id", TypeName = "int(11)")]
    public int RaceId { get; set; }

    [Column("subrace_id", TypeName = "int(11)")]
    public int? SubraceId { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string? Name { get; set; }

    [Column("desc")]
    [StringLength(255)]
    public string? Desc { get; set; }

    [ForeignKey("RaceId")]
    [InverseProperty("Dnd5Racials")]
    public virtual Dnd5Race Race { get; set; } = null!;
}
