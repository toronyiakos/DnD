using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dnd_Api.Models;

[Keyless]
[Table("map_scene_items")]
public partial class MapSceneItem
{
    [Column("scene_id", TypeName = "int(11)")]
    public int? SceneId { get; set; }

    [Column("token_id", TypeName = "int(11)")]
    public int? TokenId { get; set; }

    [Column("position_x")]
    public double? PositionX { get; set; }

    [Column("position_y")]
    public double? PositionY { get; set; }
}
