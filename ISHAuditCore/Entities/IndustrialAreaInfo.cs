using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class IndustrialAreaInfo
{
    public int Id { get; set; }

    public string? IndustrialArea { get; set; }

    public int? TownshipId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }
}
