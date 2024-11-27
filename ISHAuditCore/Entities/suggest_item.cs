using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Entities;

[Table("suggest_item")]
public partial class suggest_item
{
    [Key]
    public int id { get; set; }

    [Column("suggest_item")]
    [StringLength(255)]
    public string? suggest_item1 { get; set; }

    public int? suggest_type_id { get; set; }
}
