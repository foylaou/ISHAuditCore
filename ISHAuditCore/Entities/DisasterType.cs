using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class DisasterType
{
    public int Id { get; set; }

    public string? Disaster { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }
}
