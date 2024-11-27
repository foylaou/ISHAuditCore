using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Entities;

[Table("suggest_type")]
public partial class suggest_type
{
    [Key]
    public int id { get; set; }

    [Column("suggest_type")]
    [StringLength(255)]
    public string? suggest_type1 { get; set; }

    public int? suggest_category_id { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? update_at { get; set; }
}
