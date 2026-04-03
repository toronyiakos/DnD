using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Models;

[Keyless]
[Table("dnd5_land_druid_domain_spells")]
[Index("LandId", Name = "FK_land_druid_domain_spells_land_id")]
public partial class Dnd5LandDruidDomainSpell
{
    [Column("land_id", TypeName = "int(11)")]
    public int? LandId { get; set; }

    [Column("level", TypeName = "int(11)")]
    public int? Level { get; set; }

    [Column("spells")]
    [StringLength(255)]
    public string? Spells { get; set; }

    [ForeignKey("LandId")]
    public virtual Dnd5LandDruidDomain? Land { get; set; }
}
