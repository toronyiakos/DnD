using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Models;

[Table("map_battlemap")]
public partial class MapBattlemap
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("tags")]
    [StringLength(255)]
    public string? Tags { get; set; }

    [Column("map_path")]
    [StringLength(256)]
    public string? MapPath { get; set; }
}
