using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Migrations;

[Table("suggest_category")]
public partial class suggest_category
{
    [Key]
    public int id { get; set; }

    [Column("suggest_category")]
    [StringLength(255)]
    public string? suggest_category1 { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? update_at { get; set; }
}
