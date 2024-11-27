using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class EpKpiSubItem
{
    public int Id { get; set; }

    public int? EpKpiItemsId { get; set; }

    public string? EpKpiSubItems { get; set; }

    public string? Deadline { get; set; }
}
