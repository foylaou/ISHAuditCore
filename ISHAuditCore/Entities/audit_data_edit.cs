using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Entities;

[Table("audit_data_edit")]
public partial class audit_data_edit
{
    [Key]
    [StringLength(50)]
    public string uuid { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? start_time { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? end_time { get; set; }

    [StringLength(10)]
    public string? status { get; set; }

    [StringLength(50)]
    public string? audit_uuid { get; set; }

    [Unicode(false)]
    public string? user_contacts { get; set; }
}
