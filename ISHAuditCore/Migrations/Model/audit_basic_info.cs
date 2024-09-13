using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Migrations.Model;

[Table("audit_basic_info")]
public partial class audit_basic_info
{
    [Key]
    public int id { get; set; }

    [StringLength(50)]
    public string? uuid { get; set; }

    public int? factory_id { get; set; }

    public int? audit_type_id { get; set; }

    public int? audit_cause_id { get; set; }

    public string? disater_types_id { get; set; }

    public string? disater_types { get; set; }

    public DateTime? incident_datetime { get; set; }

    public string? incident_description { get; set; }

    [StringLength(1)]
    public string? sd { get; set; }

    [StringLength(1)]
    public string? penalty { get; set; }

    public string? penalty_detail { get; set; }

    [StringLength(1)]
    public string? improve_status { get; set; }

    public string? situation { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? participate_status_val { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? improve_status_val { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? update_at { get; set; }
}
