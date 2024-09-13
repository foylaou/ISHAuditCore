using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Migrations.Model;

[Table("audit_suggest")]
public partial class audit_suggest
{
    [Key]
    [StringLength(50)]
    public string uuid { get; set; } = null!;

    public int? audit_basic_id { get; set; }

    public int? audit_detail_id { get; set; }

    public int? suggest_item_id { get; set; }

    public int? parent_id { get; set; }

    public int? item_no { get; set; }

    public string? suggest { get; set; }

    public string? sub_suggest { get; set; }

    public string? participate { get; set; }

    public string? action { get; set; }

    public string? short_term { get; set; }

    public string? Mid_term { get; set; }

    public string? long_term { get; set; }

    public string? handling_situation { get; set; }

    public string? improve_status { get; set; }

    public string? responsible_unit { get; set; }

    public string? budget { get; set; }

    public int? budget_val { get; set; }

    public int? expert_id { get; set; }

    public string? Remarks { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? create_by { get; set; }
}
