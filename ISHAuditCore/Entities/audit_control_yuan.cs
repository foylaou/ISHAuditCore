using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Entities;

[Table("audit_control_yuan")]
public partial class audit_control_yuan
{
    [Key]
    public int id { get; set; }

    public int? audit_basic_id { get; set; }

    public string? description { get; set; }

    public DateOnly? come_date { get; set; }

    [StringLength(50)]
    public string? come_doc_uuid { get; set; }

    public string? come_doc_text { get; set; }

    [StringLength(50)]
    public string? come_doc_type { get; set; }

    public DateOnly? back_date { get; set; }

    [StringLength(50)]
    public string? back_doc_uuid { get; set; }

    public string? back_doc_text { get; set; }

    [StringLength(50)]
    public string? back_doc_type { get; set; }

    public string? remark { get; set; }
}
