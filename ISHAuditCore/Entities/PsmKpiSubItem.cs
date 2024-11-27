using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class PsmKpiSubItem
{
    public int Id { get; set; }

    public int? KpiItemsId { get; set; }

    public string? KpiSubItems { get; set; }

    public string? Deadline { get; set; }
}
