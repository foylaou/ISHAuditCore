using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class AuditDate
{
    public int Id { get; set; }

    public int AuditDetailId { get; set; }

    public DateOnly? AuditDate1 { get; set; }

    public string? Due { get; set; }
}
