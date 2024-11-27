using System;
using System.Collections.Generic;

namespace ISHAuditCore.Entities;

public partial class ImproveTypeTb
{
    public int Id { get; set; }

    public string? ImproveTypeName { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }
}
