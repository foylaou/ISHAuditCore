using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Migrations.Model;

[Table("audit_response_file_log")]
public partial class audit_response_file_log
{
    [Key]
    [StringLength(50)]
    public string uuid { get; set; } = null!;

    public int? audit_detail_id { get; set; }

    [StringLength(50)]
    public string? filename { get; set; }

    [StringLength(10)]
    public string? filetype { get; set; }

    public DateTime? created_at { get; set; }
}
