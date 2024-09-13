using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Migrations;

[Table("audit_improvement_doc")]
public partial class audit_improvement_doc
{
    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string uuid { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? doc_uuid { get; set; }

    public int? audit_basic_id { get; set; }

    public DateOnly? receive_date { get; set; }

    [Unicode(false)]
    public string? doc_text { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? doc_type { get; set; }
}
