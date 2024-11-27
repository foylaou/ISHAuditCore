using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Entities;

[Table("ep_kpi_sub_item")]
public partial class ep_kpi_sub_item
{
    [Key]
    public int id { get; set; }

    public int? ep_kpi_items_id { get; set; }

    [StringLength(255)]
    public string? ep_kpi_sub_items { get; set; }

    [StringLength(255)]
    public string? deadline { get; set; }
}
