using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Entities;

[Table("eco_kpi_item")]
public partial class eco_kpi_item
{
    [Key]
    public int id { get; set; }

    [StringLength(255)]
    public string? eco_kpi_items { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? number { get; set; }
}
