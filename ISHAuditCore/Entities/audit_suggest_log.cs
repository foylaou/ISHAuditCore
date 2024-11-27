using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Entities;

[Table("audit_suggest_log")]
public partial class audit_suggest_log
{
    [Key]
    public int id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? suggest_uuid { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? col_name { get; set; }

    public string? old_value { get; set; }

    public string? new_value { get; set; }

    public DateTime? ChangeDate { get; set; }
}
