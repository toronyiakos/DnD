using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Models;

[Keyless]
[Table("dnd5_druid_form_limit")]
public partial class Dnd5DruidFormLimit
{
    [Column("level", TypeName = "int(11)")]
    public int? Level { get; set; }

    [Column("max_cr")]
    [StringLength(255)]
    public string? MaxCr { get; set; }

    [Column("limitation")]
    [StringLength(255)]
    public string? Limitation { get; set; }
}
