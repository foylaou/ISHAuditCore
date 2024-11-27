using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class AuditDataEditLog
{
    public int Id { get; set; }

    public string? AuditDataEditUuid { get; set; }

    public DateTime? UpdateTime { get; set; }

    public decimal? ParticipateStatusVal { get; set; }

    public decimal? ImproveStatusVal { get; set; }
}
