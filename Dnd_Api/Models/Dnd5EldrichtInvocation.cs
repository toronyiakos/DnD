using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Models;

[Table("dnd5_eldricht_invocations")]
public partial class Dnd5EldrichtInvocation
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("min_level", TypeName = "int(11)")]
    public int? MinLevel { get; set; }

    [StringLength(255)]
    public string? Prerequisite { get; set; }

    [Column("effect", TypeName = "text")]
    public string Effect { get; set; } = null!;
}
