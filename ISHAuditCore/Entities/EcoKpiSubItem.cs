using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class EcoKpiSubItem
{
    public int Id { get; set; }

    public int? EcoKpiItemsId { get; set; }

    public string? EcoKpiSubItems { get; set; }

    public string? Deadline { get; set; }
}
