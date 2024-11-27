using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Entities;

public partial class audit_improvement_datum
{
    [Key]
    [StringLength(36)]
    public string uuid { get; set; } = null!;

    [StringLength(50)]
    public string? plan_no { get; set; }

    public int? audit_basic_id { get; set; }

    public int? improve_type { get; set; }

    public string? plan_name { get; set; }

    public string? plan_desc { get; set; }

    public DateOnly? start_time { get; set; }

    public DateOnly? end_time { get; set; }

    public string? handling_situation { get; set; }

    [StringLength(50)]
    public string? responsible_unit { get; set; }

    public int? budget_val { get; set; }

    public int? benefit_val { get; set; }

    public string? benefit_remark { get; set; }

    [StringLength(50)]
    public string? created_by { get; set; }

    [StringLength(50)]
    public string? updated_by { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? created_at { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? updated_at { get; set; }
}
