using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class EpKpiDatum
{
    public int Id { get; set; }

    public int? CompanyId { get; set; }

    public int? FactoryId { get; set; }

    public int? EpKpiTemplateId { get; set; }

    public int? EpKpiReportId { get; set; }

    public string? ExecutionOfKpi { get; set; }

    public string? ExecutionOfKpiUnit { get; set; }

    public string? AnnualAverage { get; set; }

    public string? Baseline { get; set; }

    public string? BaselineUnit { get; set; }

    public string? Expressions { get; set; }

    public string? Objective { get; set; }

    public string? ObjectiveUnit { get; set; }

    public string? Deviation { get; set; }

    public string? CheckoutTime { get; set; }

    public string? CheckoutTimeUnit { get; set; }

    public int? EpKpiSubItemId { get; set; }

    public string? AnnualAverageUnit { get; set; }

    public string? Number { get; set; }

    public string? Conform { get; set; }

    public string? Remark { get; set; }
}
