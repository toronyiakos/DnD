using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Models;

[Table("dnd5_alignment")]
public partial class Dnd5Alignment
{
    [Key]
    [Column("id", TypeName = "smallint(6)")]
    public short Id { get; set; }

    [Column("alignment")]
    [StringLength(255)]
    public string Alignment { get; set; } = null!;

    [Column("desc")]
    [StringLength(255)]
    public string Desc { get; set; } = null!;

    [InverseProperty("Alignment")]
    public virtual ICollection<Dnd5Character> Dnd5Characters { get; set; } = new List<Dnd5Character>();

    [InverseProperty("Alignment")]
    public virtual ICollection<Dnd5Monster> Dnd5Monsters { get; set; } = new List<Dnd5Monster>();
}
