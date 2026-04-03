using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Models;

[Keyless]
[Table("dnd5_class_spells")]
[Index("ClassId", Name = "FK_dnd5_class_spells_class_id")]
public partial class Dnd5ClassSpell
{
    [Column("class_id", TypeName = "int(2)")]
    public int ClassId { get; set; }

    [Column("spell_id", TypeName = "int(4)")]
    public int SpellId { get; set; }

    [ForeignKey("ClassId")]
    public virtual Dnd5Class Class { get; set; } = null!;
}
