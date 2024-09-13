using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Migrations;

[Table("audit_date")]
public partial class audit_date
{
    [Key]
    public int id { get; set; }

    public int audit_detail_id { get; set; }

    [Column("audit_date")]
    public DateOnly? audit_date1 { get; set; }

    [StringLength(50)]
    public string? due { get; set; }
}
