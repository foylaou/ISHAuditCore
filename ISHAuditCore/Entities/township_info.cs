using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Entities;

[Table("township_info")]
public partial class township_info
{
    [Key]
    public int id { get; set; }

    [StringLength(10)]
    public string? township { get; set; }

    public int? city_id { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? update_at { get; set; }
}
