using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Models;

[Table("dnd5_monsters")]
[Index("AlignmentId", Name = "FK_dnd5_monsters_alignment_id")]
[Index("SizeId", Name = "FK_dnd5_monsters_size_id")]
[Index("TokenId", Name = "FK_dnd5_monsters_token_id")]
[Index("TypeId", Name = "FK_dnd5_monsters_type_id")]
public partial class Dnd5Monster
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("token_id", TypeName = "int(11)")]
    public int? TokenId { get; set; }

    [Column("armor_class", TypeName = "int(11)")]
    public int ArmorClass { get; set; }

    [Column("hp", TypeName = "int(11)")]
    public int Hp { get; set; }

    [Column("speed", TypeName = "smallint(6)")]
    public short Speed { get; set; }

    [Column("speed_fly", TypeName = "smallint(6)")]
    public short SpeedFly { get; set; }

    [Column("speed_burrow", TypeName = "smallint(6)")]
    public short SpeedBurrow { get; set; }

    [Column("str", TypeName = "smallint(6)")]
    public short Str { get; set; }

    [Column("dex", TypeName = "smallint(6)")]
    public short Dex { get; set; }

    [Column("con", TypeName = "smallint(6)")]
    public short Con { get; set; }

    [Column("int", TypeName = "smallint(6)")]
    public short Int { get; set; }

    [Column("wis", TypeName = "smallint(6)")]
    public short Wis { get; set; }

    [Column("cha", TypeName = "smallint(6)")]
    public short Cha { get; set; }

    [Column("size_id", TypeName = "smallint(6)")]
    public short SizeId { get; set; }

    [Column("type_id", TypeName = "smallint(6)")]
    public short TypeId { get; set; }

    [Column("alignment_id", TypeName = "smallint(6)")]
    public short AlignmentId { get; set; }

    [ForeignKey("AlignmentId")]
    [InverseProperty("Dnd5Monsters")]
    public virtual Dnd5Alignment Alignment { get; set; } = null!;

    [ForeignKey("SizeId")]
    [InverseProperty("Dnd5Monsters")]
    public virtual Dnd5Size Size { get; set; } = null!;

    [ForeignKey("TokenId")]
    [InverseProperty("Dnd5Monsters")]
    public virtual MapToken? Token { get; set; }

    [ForeignKey("TypeId")]
    [InverseProperty("Dnd5Monsters")]
    public virtual Dnd5MonsterType Type { get; set; } = null!;
}
