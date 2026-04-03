using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Models;

[Table("dnd5_items")]
public partial class Dnd5Item
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("type_id", TypeName = "smallint(6)")]
    public short TypeId { get; set; }

    [Column("rarity_id", TypeName = "smallint(6)")]
    public short RarityId { get; set; }

    [Column("price", TypeName = "int(11)")]
    public int? Price { get; set; }

    [Column("desc")]
    [StringLength(255)]
    public string Desc { get; set; } = null!;

    [Column("attuned")]
    public bool Attuned { get; set; }

    [Column("weight", TypeName = "int(11)")]
    public int Weight { get; set; }
}
