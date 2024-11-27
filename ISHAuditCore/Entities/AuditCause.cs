using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class AuditCause
{
    public int Id { get; set; }

    public string? CauseName { get; set; }

    public string? CauseCode { get; set; }
}
