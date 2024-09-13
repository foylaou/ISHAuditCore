using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Migrations;

[Table("psm_kpi_sub_item")]
public partial class psm_kpi_sub_item
{
    [Key]
    public int id { get; set; }

    public int? kpi_items_id { get; set; }

    [StringLength(255)]
    public string? kpi_sub_items { get; set; }

    [StringLength(255)]
    public string? deadline { get; set; }
}
