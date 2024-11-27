using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class PsmKpiReport
{
    public int Id { get; set; }

    public int? CompanyId { get; set; }

    public int? FactoryId { get; set; }

    public int? KpiReportYear { get; set; }

    public DateTime? ReportTime { get; set; }

    public DateTime? CreatedTime { get; set; }

    public string? Status { get; set; }
}
