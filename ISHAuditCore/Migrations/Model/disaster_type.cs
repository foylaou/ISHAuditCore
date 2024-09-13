using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Migrations.Model;

[Table("disaster_type")]
public partial class disaster_type
{
    [Key]
    public int id { get; set; }

    [StringLength(50)]
    public string? disaster { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? update_at { get; set; }
}
