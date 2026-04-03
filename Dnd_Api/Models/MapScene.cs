using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Models;

[Table("map_scenes")]
public partial class MapScene
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("name")]
    [MaxLength(255)]
    public byte[] Name { get; set; } = null!;

    [Column("tags")]
    [StringLength(255)]
    public string? Tags { get; set; }
}
