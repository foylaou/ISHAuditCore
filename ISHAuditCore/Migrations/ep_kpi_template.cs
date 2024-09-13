using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Migrations;

[Table("ep_kpi_template")]
public partial class ep_kpi_template
{
    [Key]
    public int id { get; set; }

    public int? company_name_id { get; set; }

    public int? factory_name_id { get; set; }

    public int? ep_kpi_sub_item_id { get; set; }

    [StringLength(255)]
    public string? annual_average { get; set; }

    [StringLength(255)]
    public string? annual_average_unit { get; set; }

    [StringLength(255)]
    public string? baseline { get; set; }

    [StringLength(255)]
    public string? baseline_unit { get; set; }

    [StringLength(255)]
    public string? expressions { get; set; }

    [StringLength(255)]
    public string? objective { get; set; }

    [StringLength(255)]
    public string? objective_unit { get; set; }

    [StringLength(255)]
    public string? deviation { get; set; }

    [StringLength(255)]
    public string? checkout_time { get; set; }

    [StringLength(255)]
    public string? checkout_time_unit { get; set; }
}
