using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Migrations;

[Table("audit_data_edit_log")]
public partial class audit_data_edit_log
{
    [Key]
    public int id { get; set; }

    [StringLength(40)]
    [Unicode(false)]
    public string? audit_data_edit_uuid { get; set; }

    public DateTime? update_time { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? participate_status_val { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? improve_status_val { get; set; }
}
