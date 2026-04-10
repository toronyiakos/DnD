using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Models;

[Table("map_tokens")]
public partial class MapToken
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("token_path")]
    [StringLength(256)]
    public string? TokenPath { get; set; }

    [InverseProperty("Token")]
    public virtual ICollection<Dnd5Character> Dnd5Characters { get; set; } = new List<Dnd5Character>();

    [InverseProperty("Token")]
    public virtual ICollection<Dnd5Monster> Dnd5Monsters { get; set; } = new List<Dnd5Monster>();
}
