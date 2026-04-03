using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Models;

[Table("dnd5_item_types")]
public partial class Dnd5ItemType
{
    [Key]
    [Column("id", TypeName = "smallint(6)")]
    public short Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;
}
