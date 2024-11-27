using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Entities;

public partial class eco_kpi_datum
{
    [Key]
    public int id { get; set; }

    public int? company_id { get; set; }

    public int? eco_kpi_template_id { get; set; }

    public int? eco_kpi_report_id { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? execution_of_kpi { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? annual_average { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? annual_average_unit { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? baseline { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? baseline_unit { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? expressions { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? objective { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? objective_unit { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? deviation { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? checkout_time { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? checkout_time_unit { get; set; }

    public int? eco_kpi_sub_item_id { get; set; }

    [StringLength(255)]
    public string? number { get; set; }

    [StringLength(5)]
    public string? conform { get; set; }

    public string? remark { get; set; }
}
