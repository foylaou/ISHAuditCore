using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Migrations.Model;

[Table("company_name")]
public partial class company_name
{
    [Key]
    public int id { get; set; }

    [StringLength(255)]
    public string? company { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? update_at { get; set; }

    public int? enterprise_id { get; set; }
}
