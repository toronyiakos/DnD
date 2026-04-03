using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Models;

[Table("dnd5_metamagic")]
public partial class Dnd5Metamagic
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string? Name { get; set; }

    [Column("desc", TypeName = "text")]
    public string? Desc { get; set; }

    [Column("cost", TypeName = "int(11)")]
    public int? Cost { get; set; }
}
