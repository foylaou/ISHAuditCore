﻿using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class EcoKpiDatum
{
    public int Id { get; set; }

    public int? CompanyId { get; set; }

    public int? EcoKpiTemplateId { get; set; }

    public int? EcoKpiReportId { get; set; }

    public string? ExecutionOfKpi { get; set; }

    public string? AnnualAverage { get; set; }

    public string? AnnualAverageUnit { get; set; }

    public string? Baseline { get; set; }

    public string? BaselineUnit { get; set; }

    public string? Expressions { get; set; }

    public string? Objective { get; set; }

    public string? ObjectiveUnit { get; set; }

    public string? Deviation { get; set; }

    public string? CheckoutTime { get; set; }

    public string? CheckoutTimeUnit { get; set; }

    public int? EcoKpiSubItemId { get; set; }

    public string? Number { get; set; }

    public string? Conform { get; set; }

    public string? Remark { get; set; }
}
