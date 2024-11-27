using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class AuditReportFileLog
{
    public string Uuid { get; set; } = null!;

    public int? AuditDetailId { get; set; }

    public string? Filename { get; set; }

    public string? Filetype { get; set; }

    public DateTime? CreatedAt { get; set; }
}
