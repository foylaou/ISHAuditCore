using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Migrations.Model;

[Table("KpiDocument")]
public partial class KpiDocument
{
    [Key]
    [StringLength(50)]
    public string id { get; set; } = null!;

    [StringLength(20)]
    public string? model { get; set; }

    [StringLength(20)]
    public string? model_type { get; set; }

    [StringLength(50)]
    public string? org { get; set; }

    public int? org_id { get; set; }

    public string? parent { get; set; }

    [StringLength(50)]
    public string? text { get; set; }

    [StringLength(50)]
    public string? icon { get; set; }

    public string? path { get; set; }

    [StringLength(50)]
    public string? file_type { get; set; }

    public string? classname { get; set; }

    [StringLength(3)]
    public string? status { get; set; }
}
