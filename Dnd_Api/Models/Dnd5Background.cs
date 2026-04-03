using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Models;

[Table("dnd5_backgrounds")]
public partial class Dnd5Background
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("Skill Proficiencies")]
    [StringLength(255)]
    public string? SkillProficiencies { get; set; }

    [Column("Tool Proficiencies")]
    [StringLength(255)]
    public string? ToolProficiencies { get; set; }

    [StringLength(255)]
    public string? Languages { get; set; }

    [StringLength(255)]
    public string? Equipment { get; set; }

    [InverseProperty("Background")]
    public virtual ICollection<Dnd5Character> Dnd5Characters { get; set; } = new List<Dnd5Character>();
}
