using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class AuditType
{
    public int Id { get; set; }

    public string? AuditType1 { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }
}
