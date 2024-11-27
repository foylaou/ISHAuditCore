using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class AuditReportCommit
{
    public string? AuditDataEditUuid { get; set; }

    public string Filename { get; set; } = null!;

    public string? Filetype { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public string? OriginalFilename { get; set; }
}
