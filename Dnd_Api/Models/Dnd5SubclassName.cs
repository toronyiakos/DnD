using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Models;

[Table("dnd5_subclass_name")]
[Index("MainClassId", Name = "FK_dnd5_subclass_name_main_class_id")]
public partial class Dnd5SubclassName
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("main_class_id", TypeName = "int(11)")]
    public int? MainClassId { get; set; }

    [ForeignKey("MainClassId")]
    [InverseProperty("Dnd5SubclassNames")]
    public virtual Dnd5Class? MainClass { get; set; }
}
