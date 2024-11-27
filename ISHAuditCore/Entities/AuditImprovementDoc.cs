using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class AuditImprovementDoc
{
    public string Uuid { get; set; } = null!;

    public string? DocUuid { get; set; }

    public int? AuditBasicId { get; set; }

    public DateOnly? ReceiveDate { get; set; }

    public string? DocText { get; set; }

    public string? DocType { get; set; }
}
