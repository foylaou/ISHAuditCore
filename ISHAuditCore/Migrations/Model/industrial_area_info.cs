using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Migrations.Model;

[Table("industrial_area_info")]
public partial class industrial_area_info
{
    [Key]
    public int id { get; set; }

    [StringLength(50)]
    public string? industrial_area { get; set; }

    public int? township_id { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? update_at { get; set; }
}
