using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Models;

[Table("dnd5_inventory")]
[Index("ItemId", Name = "FK_dnd5_inventory_item_id")]
[Index("PlayerId", Name = "FK_dnd5_inventory_player_id")]
public partial class Dnd5Inventory
{
    [Column("player_id", TypeName = "int(11)")]
    public int PlayerId { get; set; }

    [Column("item_id", TypeName = "int(11)")]
    public int ItemId { get; set; }

    public virtual Dnd5Item Item { get; set; } = null!;

    public virtual Dnd5Character Player { get; set; } = null!;
}
