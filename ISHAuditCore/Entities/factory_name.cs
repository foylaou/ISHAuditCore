using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Entities;

[Table("factory_name")]
public partial class factory_name
{
    [Key]
    public int id { get; set; }

    [StringLength(255)]
    public string? factory { get; set; }

    public int? company_id { get; set; }

    public int? industrial_area_id { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? update_at { get; set; }
}
