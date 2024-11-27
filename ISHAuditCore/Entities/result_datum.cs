using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Entities;

public partial class result_datum
{
    [Key]
    public int id { get; set; }

    public int? year { get; set; }

    [StringLength(255)]
    public string? expert_name { get; set; }

    public int? expert_name_id { get; set; }

    public int? enterprise_id { get; set; }

    public int? company_id { get; set; }

    public int? factory_id { get; set; }

    public int? result_suggestion_type_id { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? indicator_category { get; set; }

    public string? suggestion_content { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? response { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? execution { get; set; }

    public DateTime? estimated_completion_date { get; set; }

    public DateTime? completion_date { get; set; }

    public int? result_report_id { get; set; }

    public int? kpi_item_id { get; set; }

    [StringLength(255)]
    public string? number { get; set; }

    public string? remark { get; set; }
}
