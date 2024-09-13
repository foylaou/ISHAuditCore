using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Migrations;

[Table("audit_report_commit")]
public partial class audit_report_commit
{
    [StringLength(50)]
    public string? auditData_edit_uuid { get; set; }

    [Key]
    [StringLength(50)]
    public string filename { get; set; } = null!;

    [StringLength(10)]
    public string? filetype { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? update_at { get; set; }

    [StringLength(50)]
    public string? original_filename { get; set; }
}
