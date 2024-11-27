using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class EpKpiTemplate
{
    public int Id { get; set; }

    public int? CompanyNameId { get; set; }

    public int? FactoryNameId { get; set; }

    public int? EpKpiSubItemId { get; set; }

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
}
