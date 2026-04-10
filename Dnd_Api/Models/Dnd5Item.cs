using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Models;

[Table("dnd5_items")]
[Index("RarityId", Name = "FK_dnd5_items_rarity_id")]
[Index("TypeId", Name = "FK_dnd5_items_type_id")]
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

    [ForeignKey("RarityId")]
    [InverseProperty("Dnd5Items")]
    public virtual Dnd5ItemRarity Rarity { get; set; } = null!;

    [ForeignKey("TypeId")]
    [InverseProperty("Dnd5Items")]
    public virtual Dnd5ItemType Type { get; set; } = null!;
}
