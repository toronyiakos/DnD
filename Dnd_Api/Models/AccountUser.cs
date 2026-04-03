using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Models;

[Table("account_users")]
[Index("RoleId", Name = "FK_users_role_id")]
public partial class AccountUser
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("pw")]
    [StringLength(100)]
    public string Pw { get; set; } = null!;

    [Column("role_id", TypeName = "int(11)")]
    public int RoleId { get; set; }

    [InverseProperty("Owner")]
    public virtual ICollection<Dnd5Character> Dnd5Characters { get; set; } = new List<Dnd5Character>();

    [ForeignKey("RoleId")]
    [InverseProperty("AccountUsers")]
    public virtual AccountRole Role { get; set; } = null!;
}
