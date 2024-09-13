﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Migrations.Model;

[Table("psm_kpi_report")]
public partial class psm_kpi_report
{
    [Key]
    public int id { get; set; }

    public int? company_id { get; set; }

    public int? factory_id { get; set; }

    public int? kpi_report_year { get; set; }

    public DateTime? report_time { get; set; }

    public DateTime? created_time { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? status { get; set; }
}