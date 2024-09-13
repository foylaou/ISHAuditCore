using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Migrations.Model;

[Table("audit_detail_info")]
public partial class audit_detail_info
{
    [Key]
    public int id { get; set; }

    [StringLength(50)]
    public string? uuid { get; set; }

    public int? audit_basic_id { get; set; }

    public int? enter_type_id { get; set; }

    public int? meeting_type_id { get; set; }

    public DateOnly? audit_start_date { get; set; }

    public DateOnly? audit_end_date { get; set; }

    public string? filename { get; set; }

    [StringLength(50)]
    public string? filename_uuid { get; set; }

    public string? reportname { get; set; }

    [StringLength(50)]
    public string? reportname_uuid { get; set; }
}
