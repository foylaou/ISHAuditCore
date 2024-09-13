using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Migrations;

[Table("ep_kpi_item")]
public partial class ep_kpi_item
{
    [Key]
    public int id { get; set; }

    [StringLength(255)]
    public string? ep_kpi_items { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? number { get; set; }
}
