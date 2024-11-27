using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class EpKpiReport
{
    public int Id { get; set; }

    public int? EpKpiReportYear { get; set; }

    public DateTime? ReportTime { get; set; }

    public DateTime? CreatedTime { get; set; }

    public int? CompanyId { get; set; }

    public int? FactoryId { get; set; }

    public string? Status { get; set; }
}
