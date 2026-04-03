using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Models;

[Table("dnd5_profbylevel")]
public partial class Dnd5Profbylevel
{
    [Key]
    [Column("level")]
    public string Level { get; set; } = null!;

    [Column("bonus")]
    [StringLength(255)]
    public string Bonus { get; set; } = null!;
}
