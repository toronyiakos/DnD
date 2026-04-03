using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Models;

[Table("dnd5_class_skills")]
[Index("ClassId", Name = "FK_céass_skills_class_id")]
public partial class Dnd5ClassSkill
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("subclass_id", TypeName = "int(11)")]
    public int? SubclassId { get; set; }

    [Column("class_id", TypeName = "int(11)")]
    public int ClassId { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("desc", TypeName = "text")]
    public string Desc { get; set; } = null!;

    [Column("unlock_level", TypeName = "smallint(6)")]
    public short UnlockLevel { get; set; }

    [ForeignKey("ClassId")]
    [InverseProperty("Dnd5ClassSkills")]
    public virtual Dnd5Class Class { get; set; } = null!;
}
