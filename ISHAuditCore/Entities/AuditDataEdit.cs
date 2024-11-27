using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class AuditDataEdit
{
    public string Uuid { get; set; } = null!;

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string? Status { get; set; }

    public string? AuditUuid { get; set; }

    public string? UserContacts { get; set; }
}
