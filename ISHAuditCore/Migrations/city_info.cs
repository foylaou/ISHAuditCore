using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Migrations;

[Table("city_info")]
public partial class city_info
{
    [Key]
    public int id { get; set; }

    [StringLength(20)]
    public string? city { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? update_at { get; set; }
}
